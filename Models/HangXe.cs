namespace DoAnChuyenNganh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HangXe")]
    public partial class HangXe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HangXe()
        {
            Xes = new HashSet<Xe>();
        }

        [Key]
        public int MaHangXe { get; set; }

        [StringLength(255)]
        public string TenHangXe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xe> Xes { get; set; }
    }
}
