namespace HttpWebRequestHostHeader
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        public long? ResSpan { get; set; }

        [Required]
        [StringLength(1)]
        public string CheckType { get; set; }

        [StringLength(3)]
        public string ResCode { get; set; }

        public long? ContentLength { get; set; }

        [StringLength(3000)]
        public string ErrorMessage { get; set; }

    }
}
