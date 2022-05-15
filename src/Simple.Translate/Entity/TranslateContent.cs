using Newtonsoft.Json;
using Simple.Core.Data.Schema;
using Simple.Core.Languages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Translate.Entity
{
    [Table("tran_Content")]
    public class TranslateContent : IEntity
    {
        /// <summary>
        /// 词汇
        /// </summary>
        [Key]
        public long Word { get; set; }
        /// <summary>
        /// 频道ID
        /// </summary>
        public Guid Channel { get; set; }
        /// <summary>
        /// 多语种Json格式
        /// </summary>
        public string Content { get; set; } = string.Empty;

        [NotMapped]
        public Dictionary<LanguageType, string> Translate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.Content))
                {
                    return new Dictionary<LanguageType, string>();
                }
                else
                {
                    return JsonConvert.DeserializeObject<Dictionary<LanguageType, string>>(this.Content) ?? new Dictionary<LanguageType, string>();
                }
            }
        }
    }
}
