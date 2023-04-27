using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace QLDV.Models.DAL.ChiPhiKhac
{
    public class ChiPhiKhacService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public List<ChiPhiKhacModel> GetListChiPhiKhac()
        {
            List<ChiPhiKhacModel> _result = new List<ChiPhiKhacModel>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT a.id, a.tenChiPhi, a.ngayThanhToan, a.noiDung, a.soLuong, a.donViTinh, a.donGia, a.thanhTien, a.ghiChu FROM vt_ChiPhiKhac a ");
            sql.Append("WHERE 1=1 ");
            sql.Append("ORDER BY a.tenChiPhi DESC");
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
                        ChiPhiKhacModel _newObj = new ChiPhiKhacModel
                        {
                            id = int.Parse(dt.Rows[i]["id"].ToString()),
                            tenChiPhi = dt.Rows[i]["tenChiPhi"].ToString(),
                            ngayThanhToan = (DateTime.Parse(dt.Rows[i]["ngayThanhToan"].ToString())).ToString("dd/MM/yyyy"),
                            noiDung = dt.Rows[i]["noiDung"].ToString(),
                            soLuong = int.Parse(dt.Rows[i]["soLuong"].ToString()),
                            donViTinh = dt.Rows[i]["donViTinh"].ToString(),
                            donGia = float.Parse(dt.Rows[i]["donGia"].ToString()),
                            thanhTien = float.Parse(dt.Rows[i]["thanhTien"].ToString()),
                            ghiChu = dt.Rows[i]["ghiChu"].ToString(),

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
        public ChiPhiKhacModel GetChiPhiKhacById(int id)
        {
            var dh = _entities.vt_ChiPhiKhac.Find(id);
            ChiPhiKhacModel a = new ChiPhiKhacModel();
            if (dh != null)
            {
                a.ngayThanhToan = ((DateTime)dh.ngayThanhToan).ToString("dd/MM/yyyy");
                a.id = dh.id;
                a.tenChiPhi = dh.tenChiPhi;
                a.noiDung = dh.noiDung;
                a.soLuong = (int)dh.soLuong;
                a.donGia = (float)dh.donGia;
                a.thanhTien = (float)dh.thanhTien;
                a.donViTinh = dh.donViTinh;
                a.ghiChu = dh.ghiChu;

                return a;
            }
            else
            {
                return a;
            }
        }
    }
}