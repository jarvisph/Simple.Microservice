using Simple.Core.Data.Schema;
using Simple.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple.Translate.Entity
{
    /// <summary>
    /// 频道表
    /// </summary>
    [Table("tran_Channel")]
    public class TranslateChannel : IEntity
    {
        ///<summary>
        ///密钥
        ///</summary>
        [Key]
        public Guid Channel { get; set; } = Guid.Empty;

        ///<summary>
        ///频道名称
        ///</summary>
        [Column("ChannelName")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }
    }
}
