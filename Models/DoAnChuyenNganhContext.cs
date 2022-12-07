using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoAnChuyenNganh.Models
{
    public partial class DoAnChuyenNganhContext : DbContext
    {
        public DoAnChuyenNganhContext()
            : base("name=DoAnChuyenNganhContext")
        {
        }

        public virtual DbSet<BaiViet> BaiViets { get; set; }
        public virtual DbSet<BinhLuanBaiViet> BinhLuanBaiViets { get; set; }
        public virtual DbSet<ChiTietDatXe> ChiTietDatXes { get; set; }
        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<ChuXe> ChuXes { get; set; }
        public virtual DbSet<DatXe> DatXes { get; set; }
        public virtual DbSet<DiaDiem> DiaDiems { get; set; }
        public virtual DbSet<HinhThucDatXe> HinhThucDatXes { get; set; }
        public virtual DbSet<HinhThucThanhToan> HinhThucThanhToans { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LienHe> LienHes { get; set; }
        public virtual DbSet<LoaiThanhVien> LoaiThanhViens { get; set; }
        public virtual DbSet<LoaiXe> LoaiXes { get; set; }
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<QuanHuyen> QuanHuyens { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiXe> TaiXes { get; set; }
        public virtual DbSet<ThanhPho> ThanhPhoes { get; set; }
        public virtual DbSet<ThanhVien> ThanhViens { get; set; }
        public virtual DbSet<XaPhuong> XaPhuongs { get; set; }
        public virtual DbSet<Xe> Xes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaiViet>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<ChuXe>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<DiaDiem>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<TaiXe>()
                .Property(e => e.SoDienThoai)
                .IsFixedLength();

            modelBuilder.Entity<TaiXe>()
                .Property(e => e.HinhAnhTaiXe)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhPho>()
                .HasMany(e => e.QuanHuyens)
                .WithOptional(e => e.ThanhPho)
                .HasForeignKey(e => e.IdThanhPho);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.XacNhanMatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<Xe>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);
        }
    }
}
