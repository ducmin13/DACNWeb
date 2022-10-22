namespace DoAnChuyenNganh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuXe")]
    public partial class ChuXe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuXe()
        {
            Xes = new HashSet<Xe>();
        }

        [Key]
        public int MaChuXe { get; set; }

        [StringLength(255)]
        public string TenChuXe { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NamSinh { get; set; }

        public string DiaChi { get; set; }

        [StringLength(255)]
        public string SoDienThoai { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public int? MaThanhVien { get; set; }

        public virtual ThanhVien ThanhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xe> Xes { get; set; }
    }
}
