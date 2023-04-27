using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.ProcedureService
{
    public class ProcedureService
    {
        QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();

        public List<ProcedureCustomModel> getListQuyTrinhByBienDongId(int idbiendong)
        {
            List<ProcedureCustomModel> rs = new List<ProcedureCustomModel>();
            DataTable dt = new DataTable();
            string sql = "select qt.QuyTrinhId, qt.TenQuyTrinh, CONCAT('[', lhtc.TenLoaiHinhToChuc, '] ',ctc.TenCapToChuc) as 'CapToChuc' from BienDong_QuyTrinh qt left join LoaiHinhToChuc lhtc on lhtc.LoaiHinhToChucId = qt.LoaiHinhToChucId left join CapToChuc ctc on ctc.CapToChucId = qt.CapToChucId where qt.BienDongId = " + idbiendong;
            dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProcedureCustomModel _new = new ProcedureCustomModel
                    {
                        QuyTrinhId = int.Parse(dt.Rows[i]["QuyTrinhId"].ToString()),
                        TenQuyTrinh = dt.Rows[i]["TenQuyTrinh"].ToString(),
                        CapToChuc = dt.Rows[i]["CapToChuc"].ToString()
                    };
                    rs.Add(_new);
                }
            }
            return rs;
        }

        public List<QuyTrinhChiTiet> getListQuyTrinhChiTietById(int idquytrinh)
        {
            List<QuyTrinhChiTiet> rs = new List<QuyTrinhChiTiet>();
            var bdqtct = _entities.BienDong_QuyTrinh_ChiTiet.Where(x => x.QuyTrinhId == idquytrinh).ToList();
            if (bdqtct != null)
            {
                for (int i = 0; i < bdqtct.Count; i++)
                {
                    string sql = "select CONCAT('[', lhtc.TenLoaiHinhToChuc, '] ',ctc.TenCapToChuc) as 'CapToChuc' from BienDong_QuyTrinh_ChiTiet bdqtct left join LoaiHinhToChuc lhtc on lhtc.LoaiHinhToChucId = bdqtct.LoaiHinhToChucId left join CapToChuc ctc on ctc.CapToChucId = bdqtct.CapToChucId where bdqtct.Id = " + bdqtct[i].Id;
                    string ctc = _sqlAccess.getDataFromSql(sql, "").Tables[0].Rows[0][0].ToString();
                    string email = "";
                    string sms = "";
                    if (bdqtct[i].Email != "")
                    {
                        string sql_email = "select TenChucVu from ChucVu where ChucVuId in (" + bdqtct[i].Email + ")";
                        DataTable dt = _sqlAccess.getDataFromSql(sql_email, "").Tables[0];
                        List<string> lschucvu = new List<string>();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            lschucvu.Add(dt.Rows[j][0].ToString());
                        }
                        email = string.Join(",", lschucvu);
                    }
                    if (bdqtct[i].Sms != "")
                    {
                        string sql_sms = "select TenChucVu from ChucVu where ChucVuId in (" + bdqtct[i].Sms + ")";
                        DataTable dt = _sqlAccess.getDataFromSql(sql_sms, "").Tables[0];
                        List<string> lschucvu = new List<string>();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            lschucvu.Add(dt.Rows[j][0].ToString());
                        }
                        sms = string.Join(",", lschucvu);
                    }
                    QuyTrinhChiTiet _new = new QuyTrinhChiTiet
                    {
                        QuyTrinhChiTietId = bdqtct[i].Id,
                        CapToChucId = bdqtct[i].CapToChucId,
                        CapToChuc = ctc,
                        Email = email,
                        Sms = sms
                    };
                    rs.Add(_new);
                }
            }
            return rs;
        }
    }
}