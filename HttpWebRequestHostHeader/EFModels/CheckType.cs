namespace HttpWebRequestHostHeader
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CheckType")]
    public partial class CheckType
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column("CheckType", Order = 1)]
        [StringLength(1)]
        public string CheckType1 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string CheckName { get; set; }

        [StringLength(3000)]
        public string CheckDesc { get; set; }
    }
}
