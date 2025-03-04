namespace HttpWebRequestHostHeader
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// DTO ��� ������� SiteCDB-A.ICheckResponse.Params 
    /// </summary>
    public partial class Params
    {
        /// <summary>
        /// Id ���������
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        /// <summary>
        /// ��� ���������
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// �������� ���������
        /// </summary>
        [StringLength(100)]
        public string Value { get; set; }
    }
}
