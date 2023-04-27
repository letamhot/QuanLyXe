using System;
using QLDV.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Globalization;
using System.Text;

namespace QLDV.Models.DAL.ChiPhiLichTrinhXe
{
    public class ChiPhiLTXService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public List<ChiPhiLTXModel> getDanhSachChiPhiLichTrinh()
        {
            List<ChiPhiLTXModel> _result = new List<ChiPhiLTXModel>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT a.id, a.idLichTrinhXe, a.loaiChiPhi, a.tenChiPhi, a.giaTien FROM vt_ChiPhiLichTrinhXe a " +
                "left join vt_LichTrinhXe b on a.idLichTrinhXe = b.id ");
            sql.Append("WHERE 1=1 ");

            sql.Append("AND a.daXoa = '0' ");
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
                        ChiPhiLTXModel _newObj = new ChiPhiLTXModel
                        {
                            id = int.Parse(dt.Rows[i]["id"].ToString()),
                            maChuyen = _entities.vt_LichTrinhXe.Find(int.Parse(dt.Rows[i]["idLichTrinhXe"].ToString())).maChuyen,
                            idLichTrinhXe = _entities.vt_LichTrinhXe.Find(int.Parse(dt.Rows[i]["idLichTrinhXe"].ToString())).id,
                            loaiChiPhi = int.Parse(dt.Rows[i]["loaiChiPhi"].ToString()),
                            tenChiPhi = dt.Rows[i]["tenChiPhi"].ToString(),
                            giaTien = float.Parse(dt.Rows[i]["giaTien"].ToString()),
                            daXoa = "0",

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
    }
}