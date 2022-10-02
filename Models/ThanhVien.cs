namespace DACN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThanhVien")]
    public partial class ThanhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThanhVien()
        {
            BaiViets = new HashSet<BaiViet>();
            ChuXes = new HashSet<ChuXe>();
            KhachHangs = new HashSet<KhachHang>();
            TaiXes = new HashSet<TaiXe>();
        }

        [Key]
        public int MaThanhVien { get; set; }

        [StringLength(255)]
        public string TaiKhoan { get; set; }

        [StringLength(255)]
        public string MatKhau { get; set; }

        [StringLength(255)]
        public string XacNhanMatKhau { get; set; }

        [StringLength(255)]
        public string TenHienThi { get; set; }

        public int? MaLoaiThanhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiViet> BaiViets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuXe> ChuXes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang> KhachHangs { get; set; }

        public virtual LoaiThanhVien LoaiThanhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaiXe> TaiXes { get; set; }
    }
}
