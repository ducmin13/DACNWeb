using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnChuyenNganh.Models
{
    public class GioHang
    {
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();

        public int iMaXe { get; set; }

        public string iTenXe { get; set; }

        public string iHinhAnh { get; set; }

        public double iGia { get; set; }

        public int iSL { get; set; }

        public DateTime iNgayDat { get; set; }

        public DateTime iNgayKetThuc { get; set; }

        public int? iMaHinhThucDatXe { get; set; }

        public String iViTriBatDau { get; set; }

        public String iViTriKetThuc { get; set; }

        public int? iMaTaiXe { get; set; }

        public Double iTHANHTIEN
        {
            get { return iSL * iGia; }
        }
        public GioHang(int MaXe)
        {
            iMaXe = MaXe;
            Xe xe = db.Xes.Single(n => n.MaXe == iMaXe);
            iTenXe = xe.TenXe;
            iHinhAnh = xe.HinhAnh;
            iGia = double.Parse(xe.Gia.ToString());
            iMaHinhThucDatXe = xe.MaHinhThucDatXe;
            iMaTaiXe = xe.MaThanhVien;
            iNgayDat = DateTime.Parse(xe.NgayBatDau.ToString());
            iNgayKetThuc = DateTime.Parse(xe.NgayKetThuc.ToString());
            iViTriBatDau = xe.ViTriBatDau;
            iViTriKetThuc = xe.ViTriKetThuc;
        }
    }
}