using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.RoleService
{
    public class RoleService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();

        public List<QuyenMenuFull> ListQuyenCha(int nguoidungid)
        {
            List<QuyenMenuFull> lsQuyenCha = new List<QuyenMenuFull>();
            DataTable dt = new DataTable();
            string sql = "select * from QuyenMenu where QuyenCha = 0 and QuyenMenuId in (select QuyenMenuId from NhomQuyenMenu_Map where NhomQuyenId in (select NhomQuyenId from NguoiDungNhomQuyen_Map where NguoiDungId = " + nguoidungid + ")) order by ThuTu";
            dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    List<QuyenMenuFull> lsChild = new List<QuyenMenuFull>();
                    DataTable dt_child = new DataTable();
                    string sql_child = "select * from QuyenMenu where QuyenCha = " + int.Parse(dt.Rows[i]["QuyenMenuId"].ToString()) + " and QuyenMenuId in (select QuyenMenuId from NhomQuyenMenu_Map where NhomQuyenId in (select NhomQuyenId from NguoiDungNhomQuyen_Map where NguoiDungId = " + nguoidungid + ")) order by ThuTu";
                    dt_child = _sqlAccess.getDataFromSql(sql_child, "").Tables[0];
                    if (dt_child.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt_child.Rows.Count; j++)
                        {
                            QuyenMenuFull qmf = new QuyenMenuFull
                            {
                                QuyenMenuId = int.Parse(dt_child.Rows[j]["QuyenMenuId"].ToString()),
                                TenQuyen = dt_child.Rows[j]["TenQuyen"].ToString(),
                                QuyenCha = int.Parse(dt_child.Rows[j]["QuyenCha"].ToString()),
                                Class = dt_child.Rows[j]["Class"].ToString(),
                                Controller = dt_child.Rows[j]["Controller"].ToString(),
                                Action = dt_child.Rows[j]["Action"].ToString(),
                                Styles = dt_child.Rows[j]["Styles"].ToString()
                            };
                            lsChild.Add(qmf);
                        }
                        
                    }
                    QuyenMenuFull qm = new QuyenMenuFull
                    {
                        QuyenMenuId = int.Parse(dt.Rows[i]["QuyenMenuId"].ToString()),
                        TenQuyen = dt.Rows[i]["TenQuyen"].ToString(),
                        Class = dt.Rows[i]["Class"].ToString(),
                        Styles = dt.Rows[i]["Styles"].ToString(),
                        Children = lsChild
                    };
                    lsQuyenCha.Add(qm);
                }
            }
            return lsQuyenCha;
        }
        
    }
}