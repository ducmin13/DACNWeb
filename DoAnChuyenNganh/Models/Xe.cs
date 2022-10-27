namespace DoAnChuyenNganh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Xe")]
    public partial class Xe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Xe()
        {
            ChiTietDatXes = new HashSet<ChiTietDatXe>();
        }

        [Key]
        public int MaXe { get; set; }

        [StringLength(255)]
        public string TenXe { get; set; }

        public int? Gia { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        public string MoTa { get; set; }

        public int? MaNhaSanXuat { get; set; }

        public int? MaLoaiXe { get; set; }

        public int? MaThanhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatXe> ChiTietDatXes { get; set; }

        public virtual LoaiXe LoaiXe { get; set; }

        public virtual NhaSanXuat NhaSanXuat { get; set; }

        public virtual ThanhVien ThanhVien { get; set; }
    }
}
