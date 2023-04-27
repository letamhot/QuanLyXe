using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace QLDV.Models.DAL.ChiPhiSuaXe
{
    public class ChiPhiSuaXeService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public List<ChiPhiSuaXeModel> GetListChiPhiSuaXe()
        {
            List<ChiPhiSuaXeModel> _result = new List<ChiPhiSuaXeModel>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT a.id, a.ngayNhap, a.vatTu, a.soLuong, a.donViTinh, a.donGia, a.thanhTien, a.ghiChu FROM vt_ChiPhiSuaXe a ");
            sql.Append("WHERE 1=1 ");
            sql.Append("ORDER BY a.vatTu DESC");
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
                        ChiPhiSuaXeModel _newObj = new ChiPhiSuaXeModel
                        {
                            id = int.Parse(dt.Rows[i]["id"].ToString()),
                            ngayNhap = (DateTime.Parse(dt.Rows[i]["ngayNhap"].ToString())).ToString("dd/MM/yyyy"),
                            vatTu = dt.Rows[i]["vatTu"].ToString(),
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
        public ChiPhiSuaXeModel GetChiPhiSuaXeById(int id)
        {
            var dh = _entities.vt_ChiPhiSuaXe.Find(id);
            ChiPhiSuaXeModel a = new ChiPhiSuaXeModel();
            if (dh != null)
            {
                a.ngayNhap = ((DateTime)dh.ngayNhap).ToString("dd/MM/yyyy");
                a.id = dh.id;
                a.vatTu = dh.vatTu;
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