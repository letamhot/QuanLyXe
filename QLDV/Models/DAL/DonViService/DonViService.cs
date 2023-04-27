using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.DonViService
{
    public class DonViService
    {
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public List<DvToChucModel> GetListDvTc(int iddonvi)
        {
            List<DvToChucModel> rs = new List<DvToChucModel>();
            string sql = "select dvtc.Id, lhtc.TenLoaiHinhToChuc as 'LoaiToChuc', ctc.TenCapToChuc as 'CapToChuc', dvtc.TenToChuc, dvtc.NgayThanhLap from DonViToChuc_Map dvtc left join LoaiHinhToChuc lhtc on dvtc.LoaiHinhToChucId = lhtc.LoaiHinhToChucId left join CapToChuc ctc on dvtc.CapToChucId = ctc.CapToChucId where dvtc.DonViId = " + iddonvi;
            DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DvToChucModel it = new DvToChucModel
                        {
                            Id = int.Parse(dt.Rows[i]["Id"].ToString()),
                            LoaiToChuc = dt.Rows[i]["LoaiToChuc"].ToString(),
                            CapToChuc = dt.Rows[i]["CapToChuc"].ToString(),
                            TenToChuc = dt.Rows[i]["TenToChuc"].ToString(),
                            NgayThanhLap = DateTime.Parse(dt.Rows[i]["NgayThanhLap"].ToString())
                        };
                        rs.Add(it);
                    }
                }
                return rs;
            }
            catch (Exception ex)
            {
                return rs;
                throw;
            }
            
            
        }
    }
}