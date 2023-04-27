using System;
using QLDV.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Globalization;
using System.Text;

namespace QLDV.Models.DAL.LichTrinhService
{
    public class LichTrinhService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public List<LichTrinhModel> getDanhSachLichTrinh()
        {
            List<LichTrinhModel> _result = new List<LichTrinhModel>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT a.id, a.maChuyen, a.idLaiXe, a.idKhachHang, a.thoiGianLapPhieu, a.noiDungLamViec, a.noiDi, a.noiDen, a.giaChuaThue, a.thueVat, a.tongThanhToan, a.tinhTrangThanhToan, a.checkPhieu FROM vt_LichTrinhXe a " +
                "left join vt_LaiXe b on a.idLaiXe = b.id "+ "left join vt_DMKhachHang c on a.idKhachHang = c.id ");
            sql.Append("WHERE 1=1 ");
            
            sql.Append("AND a.daXoa = 0 ");
            sql.Append("ORDER BY a.maChuyen DESC");
            Console.WriteLine("==============SQL: " + sql.ToString());
            DataTable dt = new DataTable();
            try
            {
                dt = _sqlAccess.getDataFromSql(sql.ToString(), "").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    int stt = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        stt++;
                        LichTrinhModel _newObj = new LichTrinhModel
                        {
                            id = int.Parse(dt.Rows[i]["id"].ToString()),
                            maChuyen = dt.Rows[i]["maChuyen"].ToString(),
                            maLaiXe = _entities.vt_LaiXe.Find(int.Parse(dt.Rows[i]["idLaiXe"].ToString())).maLaiXe,
                            idLaiXe = _entities.vt_LaiXe.Find(int.Parse(dt.Rows[i]["idLaiXe"].ToString())).id,
                            idKhachHang = _entities.vt_DMKhachHang.Find(int.Parse(dt.Rows[i]["idKhachHang"].ToString())).id,
                            maKhachHang = _entities.vt_DMKhachHang.Find(int.Parse(dt.Rows[i]["idKhachHang"].ToString())).maKhachHang,
                            thoiGianLapPhieu = (DateTime.Parse(dt.Rows[i]["thoiGianLapPhieu"].ToString())).ToString("dd/MM/yyyy"),
                            noiDungLamViec = dt.Rows[i]["noiDungLamViec"].ToString(),
                            noiDi = dt.Rows[i]["noiDi"].ToString(),
                            noiDen = dt.Rows[i]["noiDen"].ToString(),
                            giaChuaThue = double.Parse(dt.Rows[i]["giaChuaThue"].ToString()),
                            thueVat = double.Parse(dt.Rows[i]["thueVat"].ToString()),
                            tongThanhToan = double.Parse(dt.Rows[i]["tongThanhToan"].ToString()),
                            tinhTrangThanhToan = int.Parse(dt.Rows[i]["tinhTrangThanhToan"].ToString()),
                            checkPhieu = bool.Parse(dt.Rows[i]["checkPhieu"].ToString()),
                            daXoa = 0

                        };
                        _result.Add(_newObj);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return _result;
        }
        public LichTrinhModel GetLichTrinhXeById(int id)
        {
            var dh = _entities.vt_LichTrinhXe.Find(id);
            LichTrinhModel a = new LichTrinhModel();
            if (dh != null)
            {
                a.maChuyen = dh.maChuyen;
                a.id = dh.id;
                a.idLaiXe = dh.idLaiXe;
                a.idKhachHang = dh.idKhachHang;
                a.maLaiXe = _entities.vt_LaiXe.Find(dh.idLaiXe).maLaiXe;
                a.maKhachHang = _entities.vt_DMKhachHang.Find(dh.idKhachHang).maKhachHang;
                a.thoiGianLapPhieu = ((DateTime)dh.thoiGianLapPhieu).ToString("dd/MM/yyyy");
                a.noiDungLamViec = dh.noiDungLamViec;
                
                a.noiDi = dh.noiDi;
                a.noiDen = dh.noiDen;
                a.giaChuaThue = dh.giaChuaThue;
                a.thueVat = dh.thueVat;
                a.tongThanhToan = dh.tongThanhToan;
                a.tinhTrangThanhToan = dh.tinhTrangThanhToan;
                a.checkPhieu = dh.checkPhieu;
                return a;
            }
            else
            {
                return a;
            }
        }
        
    }
}