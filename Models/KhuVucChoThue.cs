namespace DACN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhuVucChoThue")]
    public partial class KhuVucChoThue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhuVucChoThue()
        {
            Xes = new HashSet<Xe>();
        }

        [Key]
        public int MaKhuVuc { get; set; }

        [StringLength(255)]
        public string TenKhuVuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xe> Xes { get; set; }
    }
}
