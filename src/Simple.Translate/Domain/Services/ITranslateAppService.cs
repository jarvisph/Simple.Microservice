using Microsoft.AspNetCore.Http;
using Simple.Core.Dependency;
using Simple.Core.Languages;

namespace Simple.Translate.Domain.Services
{
    public interface ITranslateAppService : ISingletonDependency
    {
        /// <summary>
        /// 检查频道
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        bool CheckChannel(Guid channel);
        /// <summary>
        /// 创建频道
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool CreateChannel(string name);
        /// <summary>
        /// 保存语种内容
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="language"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        bool SaveContent(Guid channel, long word, LanguageType language, string content);
        /// <summary>
        /// 在线翻译
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        string Online(LanguageType source, LanguageType target, string content);
        /// <summary>
        /// 本地在线翻译
        /// </summary>
        /// <param name="word"></param>
        /// <param name="channel"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        string Translate(string word, Guid channel, LanguageType language);

        /// <summary>
        /// 文件翻译
        /// </summary>
        /// <param name="data"></param>
        /// <param name="channel"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        byte[] Translate(byte[] data, Guid channel, LanguageType language);
    }
}
