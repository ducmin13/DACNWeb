namespace DoAnChuyenNganh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XaPhuong")]
    public partial class XaPhuong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaXaPhuong { get; set; }

        [StringLength(255)]
        public string TenXaPhuong { get; set; }

        public int? IdQuanHuyen { get; set; }
    }
}
