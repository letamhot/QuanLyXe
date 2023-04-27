using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace QLDV.Models.DAL.DangKiemGiayToXe
{
    public class DangKiemGiayToXeService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public List<DangKiemGiayToXeModel> GetDangKiem()
        {
            List<DangKiemGiayToXeModel> _result = new List<DangKiemGiayToXeModel>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT a.id, a.idXeVanTai, a.maSoXe, a.kiemDinhSo, a.ngayKiemDinh, a.loaiKiemDinh, a.noiDung, a.ngayHetHan, a.ghiChu FROM vt_DangKiemGiayToXe a " +
                "left join vt_XeVanTai b on a.idXeVanTai = b.id ");
            sql.Append("WHERE 1=1 ");

            sql.Append("AND a.daXoa = '0' ");
            sql.Append("ORDER BY a.maSoXe DESC");
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
                        DangKiemGiayToXeModel _newObj = new DangKiemGiayToXeModel
                        {
                            id = int.Parse(dt.Rows[i]["id"].ToString()),
                            maSoXe = _entities.vt_XeVanTai.Find(int.Parse(dt.Rows[i]["idXeVanTai"].ToString())).maSoXe,
                            idXeVanTai = _entities.vt_XeVanTai.Find(int.Parse(dt.Rows[i]["idXeVanTai"].ToString())).id,
                            kiemDinhSo = dt.Rows[i]["kiemDinhSo"].ToString(),
                            ngayKiemDinh = (DateTime.Parse(dt.Rows[i]["ngayKiemDinh"].ToString())).ToString("dd/MM/yyyy"),
                            loaiKiemDinh = dt.Rows[i]["loaiKiemDinh"].ToString(),
                            noiDung = dt.Rows[i]["noiDung"].ToString(),
                            ngayHetHan = (DateTime.Parse(dt.Rows[i]["ngayHetHan"].ToString())).ToString("dd/MM/yyyy"),
                            ghiChu = dt.Rows[i]["ghiChu"].ToString(),
                            daXoa = "0"

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
        public DangKiemGiayToXeModel GetDangKiemById(int id)
        {
            var dh = _entities.vt_DangKiemGiayToXe.Find(id);
            DangKiemGiayToXeModel a = new DangKiemGiayToXeModel();
            if (dh != null)
            {
                a.id = dh.id;
                a.idXeVanTai = dh.idXeVanTai;
                a.kiemDinhSo = dh.kiemDinhSo;
                a.maSoXe = _entities.vt_XeVanTai.Find(dh.idXeVanTai).maSoXe;
                a.ngayKiemDinh = ((DateTime)dh.ngayKiemDinh).ToString("dd/MM/yyyy");
                a.ngayHetHan = ((DateTime)dh.ngayHetHan).ToString("dd/MM/yyyy");
                a.loaiKiemDinh = dh.loaiKiemDinh;

                a.noiDung = dh.noiDung;
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