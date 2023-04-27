using System;
using QLDV.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Globalization;
using System.Text;

namespace QLDV.Models.DAL.XeVanTaiService
{
    public class XeVanTaiService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public List<XeVanTaiModel> GetListXeVantai()
        {
            List<XeVanTaiModel> _result = new List<XeVanTaiModel>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT a.id, a.maSoXe, a.idLaiXe FROM vt_XeVanTai a " +
                "left join vt_XeVanTai b on a.idLaiXe = b.id ");
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
                        XeVanTaiModel _newObj = new XeVanTaiModel
                        {
                            id = int.Parse(dt.Rows[i]["id"].ToString()),
                            maSoXe = dt.Rows[i]["maSoXe"].ToString(),
                            maLaiXe = _entities.vt_LaiXe.Find(int.Parse(dt.Rows[i]["idLaiXe"].ToString())).maLaiXe,
                            idLaiXe = _entities.vt_LaiXe.Find(int.Parse(dt.Rows[i]["idLaiXe"].ToString())).id,
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
        public XeVanTaiModel GetXeVanTaiById(int id)
        {
            var dh = _entities.vt_XeVanTai.Find(id);
            XeVanTaiModel a = new XeVanTaiModel();
            if (dh != null)
            {
                a.maSoXe = dh.maSoXe;
                a.id = dh.id;
                a.idLaiXe = dh.idLaiXe;
                a.maLaiXe = _entities.vt_LaiXe.Find(dh.idLaiXe).maLaiXe;
                
                return a;
            }
            else
            {
                return a;
            }
        }
    }
}