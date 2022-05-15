using Newtonsoft.Json;
using Simple.Core.Dapper;
using Simple.Core.Domain;
using Simple.Core.Domain.Enums;
using Simple.Core.Encryption;
using Simple.Core.Helper;
using Simple.Core.Languages;
using Simple.Core.Logger;
using Simple.Translate.Domain.Services;
using Simple.Translate.Entity;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Simple.Translate.Application
{
    public class TranslateAppService : AppServiceBase, ITranslateAppService
    {
        private long GetKey(string word)
        {
            word = word.Replace("~", "");
            return SHA256Encryption.GetLongHashCode(word);
        }

        public string Online(LanguageType source, LanguageType target, string content)
        {
            Regex pattern = new Regex("<span class=\"jsx-1885187966\" href=\"(.+?)\">(.+?)</span>");
            string url = "http://www.iciba.com/word?w=" + content;
            string text = NetHelper.Get(url);
            if (!string.IsNullOrWhiteSpace(text))
            {
                Match match = pattern.Match(text);
                if (match.Groups.Count == 3)
                {
                    content = match.Groups[2].Value;
                }
            }
            return content;
        }

        public string Translate(string word, Guid channel, LanguageType language)
        {
            if (!CheckChannel(channel)) throw new MessageException("频道错误");
            long key = this.GetKey(word);
            using (IDapperDatabase db = CreateDatabase(IsolationLevel.ReadUncommitted))
            {
                TranslateContent text = db.FirstOrDefault<TranslateContent>(c => c.Channel == channel && c.Word == key);
                if (text == null || !text.Translate.ContainsKey(language))
                {
                    text ??= new TranslateContent();
                    string content = db.FirstOrDefault<TranslateWord, string>(c => c.Word == key && c.Language == language, c => c.Content);
                    content = word.Replace("~", "");
                    text.Translate[LanguageType.CHN] = content;
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        if (!db.Any<TranslateWord>(c => c.Word == key && c.Language == LanguageType.CHN))
                        {
                            db.Insert(new TranslateWord
                            {
                                Content = content,
                                Language = LanguageType.CHN,
                                CreateAt = DateTime.Now,
                                Word = key
                            });
                        }
                        content = Online(LanguageType.CHN, language, content);
                        text.Translate[language] = content;
                        if (db.Any<TranslateWord>(c => c.Word == key && c.Language == language))
                        {
                            db.Update<TranslateWord, string>(c => c.Word == key && c.Language == language, c => c.Content, content);
                        }
                        else
                        {
                            db.Insert(new TranslateWord()
                            {
                                Content = content,
                                Language = language,
                                CreateAt = DateTime.Now,
                                Word = key
                            });
                        }

                    }
                    text.Content = JsonConvert.SerializeObject(text.Translate);
                    if (db.Any<TranslateContent>(c => c.Word == key && c.Channel == channel))
                    {
                        db.Update(text, c => c.Word == key && c.Channel == channel, c => c.Content);
                    }
                    else
                    {
                        text.Word = key;
                        text.Channel = channel;
                        db.Insert(text);
                    }
                }
                db.Commit();
                return text.Translate.GetValueOrDefault(language, string.Empty);
            }
        }

        public byte[] Translate(byte[] data, Guid channel, LanguageType language)
        {
            if (!CheckChannel(channel)) throw new MessageException("频道错误");
            string content = Encoding.UTF8.GetString(data);
            Regex regex = new Regex("~(.+?)~");
            if (regex.IsMatch(content))
            {
                MatchCollection matchs = regex.Matches(content);
                foreach (Match match in matchs)
                {
                    string text = Translate(match.Value, channel, language);
                    content.Replace(match.Value, text);
                }
            }
            return Encoding.UTF8.GetBytes(content);
        }

        public bool CheckChannel(Guid channel)
        {
            if (channel == Guid.Empty) return false;
            return MemoryHelper.GetOrCreate(channel.ToString("N"), TimeSpan.MaxValue, () =>
            {
                using (IDapperDatabase db = CreateDatabase())
                {
                    return db.Any<TranslateChannel>(c => c.Channel == channel);
                }
            });
        }

        public bool CreateChannel(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 16) throw new MessageException("频道名称错误");
            using (IDapperDatabase db = CreateDatabase())
            {
                db.Insert(new TranslateChannel
                {
                    Channel = Guid.NewGuid(),
                    Name = name,
                    CreateAt = DateTime.Now,
                    Status = UserStatus.Normal
                });
            }
            return Logger.Log("创建频道");
        }

        public bool SaveContent(Guid channel, long word, LanguageType language, string content)
        {
            if (!CheckChannel(channel)) throw new MessageException("频道错误");
            using (IDapperDatabase db = CreateDatabase(IsolationLevel.ReadUncommitted))
            {
                if (!db.Any<TranslateWord>(c => c.Word == word && c.Language == language))
                {
                    db.Insert(new TranslateWord
                    {
                        Word = word,
                        Language = language,
                        Content = content,
                        CreateAt = DateTime.Now,
                    });
                }
                TranslateContent text = db.FirstOrDefault<TranslateContent>(c => c.Channel == channel && c.Word == word);
                if (text == null)
                {
                    text ??= new TranslateContent();
                    text.Word = word;
                    text.Channel = channel;
                    text.Translate[language] = content;
                    text.Content = JsonConvert.SerializeObject(text.Translate);
                    db.Insert(text);
                }
                else
                {
                    text.Translate[language] = content;
                    text.Content = JsonConvert.SerializeObject(text.Translate);
                    db.Update(text, c => c.Channel == channel && c.Word == word, t => t.Content);
                }
                db.Commit();
            }
            return Logger.Log($"保存内容/{channel}/{word}");
        }
    }
}
