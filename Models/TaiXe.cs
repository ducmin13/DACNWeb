namespace DACN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiXe")]
    public partial class TaiXe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiXe()
        {
            Xes = new HashSet<Xe>();
        }

        [Key]
        public int MaTaiXe { get; set; }

        [StringLength(255)]
        public string TenTaiXe { get; set; }

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
