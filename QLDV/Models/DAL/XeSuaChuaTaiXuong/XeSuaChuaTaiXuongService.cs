using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace QLDV.Models.DAL.XeSuaChuaTaiXuong
{
    public class XeSuaChuaTaiXuongService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public List<XeSuaChuaTaiXuongModel> getDanhSachXeSuaChuaTaiXuong()
        {
            List<XeSuaChuaTaiXuongModel> _result = new List<XeSuaChuaTaiXuongModel>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT a.id, a.ngayThang, a.idXeVanTai, a.idKhachHang, a.noiDungSuaChua, a.soLuong, a.donViTinh, a.donGia, a.thanhTienChuaThue, a.thueVat, a.tongThanhToan, a.no, a.canTru, a.ghiChu FROM vt_XeSuaChuaTaiXuong a " +
                "left join vt_XeVanTai b on a.idXeVanTai = b.id " + "left join vt_DMKhachHang c on a.idKhachHang = c.id ");
            sql.Append("WHERE 1=1 ");

            sql.Append("ORDER BY a.id DESC");
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
                        XeSuaChuaTaiXuongModel _newObj = new XeSuaChuaTaiXuongModel
                        {
                            id = int.Parse(dt.Rows[i]["id"].ToString()),
                            ngayThang = (DateTime.Parse(dt.Rows[i]["ngayThang"].ToString())).ToString("dd/MM/yyyy"),
                            idXeVanTai = _entities.vt_XeVanTai.Find(int.Parse(dt.Rows[i]["idXeVanTai"].ToString())).id,
                            maXe = _entities.vt_XeVanTai.Find(int.Parse(dt.Rows[i]["idXeVanTai"].ToString())).maSoXe,
                            idKhachHang = _entities.vt_DMKhachHang.Find(int.Parse(dt.Rows[i]["idKhachHang"].ToString())).id,
                            tenKhachHang = _entities.vt_DMKhachHang.Find(int.Parse(dt.Rows[i]["idKhachHang"].ToString())).tenKhachHang,
                            noiDungSuaChua = dt.Rows[i]["noiDungSuaChua"].ToString(),
                            soLuong = int.Parse(dt.Rows[i]["soLuong"].ToString()),
                            donViTinh = dt.Rows[i]["donViTinh"].ToString(),
                            donGia = double.Parse(dt.Rows[i]["donGia"].ToString()),
                            thanhTienChuaThue = double.Parse(dt.Rows[i]["thanhTienChuaThue"].ToString()),
                            thueVat = double.Parse(dt.Rows[i]["thueVat"].ToString()),
                            tongThanhToan = double.Parse(dt.Rows[i]["tongThanhToan"].ToString()),
                            no = double.Parse(dt.Rows[i]["no"].ToString() == null ? "0" : dt.Rows[i]["no"].ToString()),
                            canTru = double.Parse(dt.Rows[i]["canTru"].ToString() == null ? "0" : dt.Rows[i]["canTru"].ToString()),
                            ghiChu = dt.Rows[i]["ghiChu"].ToString() == "" ? "" : dt.Rows[i]["ghiChu"].ToString()

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
        public XeSuaChuaTaiXuongModel GetXeSuaChuaById(int id)
        {
            var dh = _entities.vt_XeSuaChuaTaiXuong.Find(id);
            XeSuaChuaTaiXuongModel a = new XeSuaChuaTaiXuongModel();
            if (dh != null)
            {
                a.ngayThang = ((DateTime)dh.ngayThang).ToString("dd/MM/yyyy");
                a.id = dh.id;
                a.idXeVanTai = dh.idXeVanTai;
                a.idKhachHang = dh.idKhachHang;
                a.maXe = _entities.vt_XeVanTai.Find(dh.idXeVanTai).maSoXe;
                a.tenKhachHang = _entities.vt_DMKhachHang.Find(dh.idKhachHang).tenKhachHang;
                a.noiDungSuaChua = dh.noiDungSuaChua;

                a.soLuong = (int)dh.soLuong;
                a.donViTinh = dh.donViTinh;
                a.donGia = dh.donGia;
                a.thanhTienChuaThue = dh.thanhTienChuaThue;
                a.thueVat = dh.thueVat;
                a.tongThanhToan = dh.tongThanhToan;
                a.no = dh.no;
                a.canTru = dh.canTru;
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