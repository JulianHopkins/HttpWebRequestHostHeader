namespace HttpWebRequestHostHeader
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// DTO для таблицы SiteCDB-A.ICheckResponse.IpCheck
    /// </summary>

    [Table("IpCheck")]
    public partial class IpCheck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public DateTime ReqTime { get; set; }
        [Required]
        [StringLength(15)]
        public string IpAddr { get; set; }
        /// <summary>
        /// Время отклика в милисекундах
        /// </summary>
        public long? ResSpan { get; set; }
        /// <summary>
        /// Тип проверки
        /// </summary>
        [Required]
        [StringLength(1)]
        public string CheckType { get; set; }
        /// <summary>
        /// Код ответа
        /// </summary>
        [StringLength(3)]
        public string ResCode { get; set; }
        /// <summary>
        /// Значение HTTP-Заголовка Content-Length
        /// </summary>
        public long? ContentLength { get; set; }
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        [StringLength(3000)]
        public string ErrorMessage { get; set; }

    }
}
