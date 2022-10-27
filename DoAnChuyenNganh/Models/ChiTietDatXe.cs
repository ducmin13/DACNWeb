namespace DoAnChuyenNganh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDatXe")]
    public partial class ChiTietDatXe
    {
        [Key]
        public int MaChiTietDatXe { get; set; }

        public int? MaDatXe { get; set; }

        public int? MaXe { get; set; }

        public int? TongThanhToan { get; set; }

        public virtual DatXe DatXe { get; set; }

        public virtual Xe Xe { get; set; }
    }
}
