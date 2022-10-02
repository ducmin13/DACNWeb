namespace DACN.Models
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

        public int? GiaThue { get; set; }

        public int? SoLuong { get; set; }

        public int? MaHangXe { get; set; }

        public int? MaLoai { get; set; }

        public int? MaTaiXe { get; set; }

        public int? MaHinhThucDatXe { get; set; }

        public int? MaChuXe { get; set; }

        public int? MaKhuVuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatXe> ChiTietDatXes { get; set; }

        public virtual ChuXe ChuXe { get; set; }

        public virtual HangXe HangXe { get; set; }

        public virtual HinhThucDatXe HinhThucDatXe { get; set; }

        public virtual KhuVucChoThue KhuVucChoThue { get; set; }

        public virtual LoaiXe LoaiXe { get; set; }

        public virtual TaiXe TaiXe { get; set; }
    }
}
