using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.CapToChucService
{
    public class CapToChucService
    {
        QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public List<ToChucSelectedModel> getListCapToChucSelected(int nhomquyenid)
        {
            List<ToChucSelectedModel> result = new List<ToChucSelectedModel>();
            List<CapToChuc> lsCapToChuc = _entities.CapToChucs.ToList();
            DataTable dt = new DataTable();
            string query = "select CapToChucId from NhomQuyenCapTc_Map where NhomQuyenId = " + nhomquyenid;
            dt = _sqlAccess.getDataFromSql(query, "").Tables[0];
            for (int i = 0; i < lsCapToChuc.Count; i++)
            {
                bool isChecked = false;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j][0].ToString() == lsCapToChuc[i].CapToChucId.ToString())
                    {
                        isChecked = true;
                        break;
                    }
                }
                result.Add(new ToChucSelectedModel
                {
                    ID = lsCapToChuc[i].CapToChucId,
                    TenCapToChuc = lsCapToChuc[i].TenCapToChuc,
                    Selected = isChecked
                });
            }
            return result;
        }

        public List<ToChucSelectedModel> getListCapToChucByLoaiHtc(int loaihtcid)
        {
            List<ToChucSelectedModel> result = new List<ToChucSelectedModel>();
            List<CapToChuc> lsCapToChuc = _entities.CapToChucs.ToList();
            DataTable dt = new DataTable();
            string query = "select CapToChucId from CapToChucLoaiHinh_Map where LoaiHinhToChucId = " + loaihtcid;
            dt = _sqlAccess.getDataFromSql(query, "").Tables[0];
            for (int i = 0; i < lsCapToChuc.Count; i++)
            {
                bool isChecked = false;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j][0].ToString() == lsCapToChuc[i].CapToChucId.ToString())
                    {
                        isChecked = true;
                        break;
                    }
                }
                result.Add(new ToChucSelectedModel
                {
                    ID = lsCapToChuc[i].CapToChucId,
                    TenCapToChuc = lsCapToChuc[i].TenCapToChuc,
                    Selected = isChecked
                });
            }
            return result;
        }

        public List<LoaiHinhToChuc> getListLoaiHTCByDonVi(int iddonvi, int idtochuc)
        {
            List<LoaiHinhToChuc> rs = new List<LoaiHinhToChuc>();
            DataTable dt = new DataTable();
            string sql = "select LoaiHinhToChucId from DonViToChuc_Map where DonViId = " + iddonvi + " and id != " + idtochuc;
            dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            string sql_lhtc = "select * from LoaiHinhToChuc";
            if (dt.Rows.Count > 0)
            {
                string[] except = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    except[i] = dt.Rows[i][0].ToString();
                }
                sql_lhtc += " where LoaiHinhToChucId not in (" + string.Join(",", except) + ")";
            }
            DataTable dt_lhtc = _sqlAccess.getDataFromSql(sql_lhtc, "").Tables[0];
            if (dt_lhtc.Rows.Count > 0)
            {
                for (int i = 0; i < dt_lhtc.Rows.Count; i++)
                {
                    LoaiHinhToChuc item = new LoaiHinhToChuc
                    {
                        LoaiHinhToChucId = int.Parse(dt_lhtc.Rows[i]["LoaiHinhToChucId"].ToString()),
                        TenLoaiHinhToChuc = dt_lhtc.Rows[i]["TenLoaiHinhToChuc"].ToString()
                    };
                    rs.Add(item);
                }
            }
            return rs;
        }

        public List<CapToChuc> getCapToChucByLoaiHTCExceptDonVi(int idloaihtc, int iddonvi, int idtochuc)
        {
            List<CapToChuc> rs = new List<CapToChuc>();
            DataTable dt = new DataTable();
            string sql = "select CapToChucId from DonViToChuc_Map where DonViId = " + iddonvi + " and Id != " + idtochuc;
            dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            string sql_ctc = "select ctc.CapToChucId, ctc.TenCapToChuc from CapToChuc ctc left join CapToChucLoaiHinh_Map ctclh on ctc.CapToChucId = ctclh.CapToChucId where ctclh.LoaiHinhToChucId = " + idloaihtc;
            if (dt.Rows.Count > 0)
            {
                string[] except = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    except[i] = dt.Rows[i][0].ToString();
                }
                sql_ctc += " and ctc.CapToChucId not in (" + string.Join(",", except) + ")";
            }
            DataTable dt_ctc = _sqlAccess.getDataFromSql(sql_ctc, "").Tables[0];
            if (dt_ctc.Rows.Count > 0)
            {
                for (int i = 0; i < dt_ctc.Rows.Count; i++)
                {
                    CapToChuc item = new CapToChuc
                    {
                        CapToChucId = int.Parse(dt_ctc.Rows[i]["CapToChucId"].ToString()),
                        TenCapToChuc = dt_ctc.Rows[i]["TenCapToChuc"].ToString()
                    };
                    rs.Add(item);
                }
            }
            return rs;
        }

        public List<CapToChuc> getCapToChucByLoaiHTCId(int loaihtcid)
        {
            List<CapToChuc> rs = new List<CapToChuc>();
            string sql = "select ctc.CapToChucId, ctc.TenCapToChuc from CapToChuc ctc left join CapToChucLoaiHinh_Map ctclh on ctc.CapToChucId = ctclh.CapToChucId where ctclh.LoaiHinhToChucId = " + loaihtcid;
            DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CapToChuc item = new CapToChuc
                    {
                        CapToChucId = int.Parse(dt.Rows[i]["CapToChucId"].ToString()),
                        TenCapToChuc = dt.Rows[i]["TenCapToChuc"].ToString()
                    };
                    rs.Add(item);
                }
            }
            return rs;
        }
    }
}