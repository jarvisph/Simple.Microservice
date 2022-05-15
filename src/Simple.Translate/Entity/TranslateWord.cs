using Simple.Core.Data.Schema;
using Simple.Core.Languages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple.Translate.Entity
{
    [Table("tran_Word")]
    public class TranslateWord : IEntity
    {
        ///<summary>
        ///[ID]词汇ID
        ///</summary>
        [Key]
        public long Word { get; set; }
        /// <summary>
        /// 语种
        /// </summary>
        [Key]
        public LanguageType Language { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }
    }
}
