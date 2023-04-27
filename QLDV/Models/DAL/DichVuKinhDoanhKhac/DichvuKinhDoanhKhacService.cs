using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace QLDV.Models.DAL.DichVuKinhDoanhKhac
{
    public class DichvuKinhDoanhKhacService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public List<DichVuKinhDoanhKhacModel> GetListDichVuKDK()
        {
            List<DichVuKinhDoanhKhacModel> _result = new List<DichVuKinhDoanhKhacModel>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT a.id, a.loaiDichVuKhac, a.ngayHoanThanh, a.tenCongTrinh, a.doiTruong, a.doanhThu, a.chiPhi, a.loiNhuan, a.thanhToanLan1, a.thanhToanLan2, a.thanhToanLan3, a.no, a.ghiChu FROM vt_DichVuKinhDoanhKhac a ");
            sql.Append("WHERE 1=1 ");

            sql.Append("ORDER BY a.tenCongTrinh DESC");
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
                        DichVuKinhDoanhKhacModel _newObj = new DichVuKinhDoanhKhacModel
                        {
                            id = int.Parse(dt.Rows[i]["id"].ToString()),
                            loaiDichVuKhac = int.Parse(dt.Rows[i]["loaiDichVuKhac"].ToString()),
                            ngayHoanThanh = (DateTime.Parse(dt.Rows[i]["ngayHoanThanh"].ToString())).ToString("dd/MM/yyyy"),
                            tenCongTrinh = dt.Rows[i]["tenCongTrinh"].ToString(),
                            doiTruong = dt.Rows[i]["doiTruong"].ToString(),
                            doanhThu = Double.Parse(dt.Rows[i]["doanhThu"].ToString() != null ? dt.Rows[i]["doanhThu"].ToString(): "0"),
                            chiPhi = Double.Parse(dt.Rows[i]["chiPhi"].ToString() != null ? dt.Rows[i]["chiPhi"].ToString() : "0"),
                            loiNhuan = Double.Parse(dt.Rows[i]["loiNhuan"].ToString() != "" ? dt.Rows[i]["loiNhuan"].ToString() : "0"),
                            thanhToanLan1 = Double.Parse(dt.Rows[i]["thanhToanLan1"].ToString() != null ? dt.Rows[i]["thanhToanLan1"].ToString() : "0"),
                            thanhToanLan2 = Double.Parse(dt.Rows[i]["thanhToanLan2"].ToString() != null ? dt.Rows[i]["thanhToanLan2"].ToString() : "0"),
                            thanhToanLan3 = Double.Parse(dt.Rows[i]["thanhToanLan3"].ToString() != null ? dt.Rows[i]["thanhToanLan3"].ToString() : "0"),
                            no = Double.Parse(dt.Rows[i]["no"].ToString() != null ? dt.Rows[i]["no"].ToString() : "0"),
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
        public DichVuKinhDoanhKhacModel GetDichVuKhacById(int id)
        {
            var dh = _entities.vt_DichVuKinhDoanhKhac.Find(id);
            DichVuKinhDoanhKhacModel a = new DichVuKinhDoanhKhacModel();
            if (dh != null)
            {
                a.loaiDichVuKhac = dh.loaiDichVuKhac;
                a.id = dh.id;
                a.ngayHoanThanh = ((DateTime)dh.ngayHoanThanh).ToString("dd/MM/yyyy"); ;
                a.tenCongTrinh = dh.tenCongTrinh;
                a.doiTruong = dh.doiTruong;
                a.doanhThu = dh.doanhThu;
                a.chiPhi = dh.chiPhi;
                a.loiNhuan = dh.loiNhuan;

                a.thanhToanLan1 = dh.thanhToanLan1;
                a.thanhToanLan2 = dh.thanhToanLan2;
                a.thanhToanLan3 = dh.thanhToanLan3;
                a.no = dh.no;
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