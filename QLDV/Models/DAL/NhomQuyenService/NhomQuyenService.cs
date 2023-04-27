using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace QLDV.Models.DAL.NhomQuyenService
{
    public class NhomQuyenService
    {
        SqlDataAccess _sqlAccess = new SqlDataAccess();


        public DataTable GetQuyenMain()
        {
            DataTable dt = new DataTable();
            string sql = "select * from QuyenMenu where QuyenCha = 0";
            dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            return dt;
        }

        public DataTable GetRoleChild(int parentid)
        {
            DataTable dt = new DataTable();
            string sql = "select * from QuyenMenu where QuyenCha = " + parentid;
            dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            return dt;
        }

        public DataTable GetRoleSelectedByNhomQuyenID(int nhomquyenid)
        {
            DataTable dt = new DataTable();
            string query = "select QuyenMenuId from NhomQuyenMenu_Map where NhomQuyenId = " + nhomquyenid;
            dt = _sqlAccess.getDataFromSql(query, "").Tables[0];
            return dt;
        }

        public int GetParentIdByQuyenId(int quyenmenuid)
        {
            DataTable dt = new DataTable();
            string sql = "select QuyenCha from QuyenMenu where QuyenMenuId = " + quyenmenuid;
            dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0]["QuyenCha"].ToString());
            }
            return 0;
        }

        public bool CheckExistParentNode(int nhomquyenid, int quyenmenuid)
        {
            string sql = "select * from NhomQuyenMenu_Map where NhomQuyenId = " + nhomquyenid + " and QuyenMenuId = " + quyenmenuid;
            DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}