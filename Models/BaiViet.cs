namespace DACN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiViet")]
    public partial class BaiViet
    {
        [Key]
        public int MaBaiViet { get; set; }

        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        public DateTime? NgayDang { get; set; }

        public int? SoLuotThich { get; set; }

        public int? MaThanhVien { get; set; }

        public virtual ThanhVien ThanhVien { get; set; }
    }
}
