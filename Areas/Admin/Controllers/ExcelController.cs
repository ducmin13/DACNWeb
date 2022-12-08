using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;

namespace DoAnChuyenNganh.Areas.Admin.Controllers
{
    public class ExcelController : Controller
    {
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();
        public ActionResult ImportExcel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImportExcel(HttpPostedFileBase postedFile)
        {

            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                string conString = string.Empty;
                switch (extension)
                {
                    case ".xls":
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx":
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;


                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();


                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

                conString = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.Xe";

                        sqlBulkCopy.ColumnMappings.Add("MaXe", "MaXe");
                        sqlBulkCopy.ColumnMappings.Add("TenXe", "TenXe");
                        sqlBulkCopy.ColumnMappings.Add("Gia", "Gia");
                        sqlBulkCopy.ColumnMappings.Add("HinhAnh", "HinhAnh");
                        sqlBulkCopy.ColumnMappings.Add("MoTa", "MoTa");
                        sqlBulkCopy.ColumnMappings.Add("MaNhaSanXuat", "MaNhaSanXuat");
                        sqlBulkCopy.ColumnMappings.Add("MaLoaiXe", "MaLoaiXe");
                        sqlBulkCopy.ColumnMappings.Add("MaThanhVien", "MaThanhVien");
                        sqlBulkCopy.ColumnMappings.Add("MaHinhThuc", "MaHinhThuc");
                        sqlBulkCopy.ColumnMappings.Add("Status", "Status");
                        sqlBulkCopy.ColumnMappings.Add("ViTri", "ViTri");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();


                    }
                }
            }

            return View();
        }
    }
}