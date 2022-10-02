using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DACN.Models
{
    public partial class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext()
            : base("name=CarRentalDbContext")
        {
        }

        public virtual DbSet<BaiViet> BaiViets { get; set; }
        public virtual DbSet<ChiTietDatXe> ChiTietDatXes { get; set; }
        public virtual DbSet<ChuXe> ChuXes { get; set; }
        public virtual DbSet<DatXe> DatXes { get; set; }
        public virtual DbSet<HangXe> HangXes { get; set; }
        public virtual DbSet<HinhThucDatXe> HinhThucDatXes { get; set; }
        public virtual DbSet<HinhThucThanhToan> HinhThucThanhToans { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KhuVucChoThue> KhuVucChoThues { get; set; }
        public virtual DbSet<LoaiThanhVien> LoaiThanhViens { get; set; }
        public virtual DbSet<LoaiXe> LoaiXes { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<TaiXe> TaiXes { get; set; }
        public virtual DbSet<ThanhVien> ThanhViens { get; set; }
        public virtual DbSet<Xe> Xes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaiViet>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<ChuXe>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<ChuXe>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<PhanQuyen>()
                .Property(e => e.MaQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<Quyen>()
                .Property(e => e.MaQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<TaiXe>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<TaiXe>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.XacNhanMatKhau)
                .IsUnicode(false);
        }
    }
}
