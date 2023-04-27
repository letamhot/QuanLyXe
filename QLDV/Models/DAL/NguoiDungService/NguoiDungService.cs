using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.NguoiDungService
{
    public class NguoiDungService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public List<int> getDonViChildByDonViId(int iddonvi)
        {
            List<int> rs = new List<int>();
            rs.Add(iddonvi);

            
            List<DonVi> lsDonVi = _entities.DonVis.ToList();
            List<DonVi> lsCha = new List<DonVi>();
            foreach (var item in lsDonVi)
            {
                if (item.DonViCha == iddonvi)
                {
                    lsCha.Add(item);
                }
            }
            foreach (var item in lsCha)
            {
                rs.Add(item.DonViId);
                AddChild(lsDonVi, rs, item.DonViId);
            }
            return rs;
        }
        public void AddChild(List<DonVi> lsDonvi, List<int> rs, int parentId)
        {
            List<DonVi> result = new List<DonVi>();
            foreach (var item in lsDonvi)
            {
                if (item.DonViCha == parentId)
                {
                    result.Add(item);
                }
            }
            try
            {
                foreach (var item in result)
                {
                    rs.Add(item.DonViId);
                    AddChild(lsDonvi, rs, item.DonViId);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public List<DonViToChuc_Map> GetToChucChildByToChucId(int idtochuc, int idcaptochuc, int idloaihtc, List<DonViToChuc_Map> lsToChuc)
        {
            List<DonViToChuc_Map> rs = new List<DonViToChuc_Map>();

            var _first = _entities.DonViToChuc_Map.FirstOrDefault(x => x.Id == idtochuc && x.LoaiHinhToChucId == idloaihtc && x.CapToChucId == idcaptochuc);
            if (_first != null)
            {
                rs.Add(_first);
            }
            //if (lsToChuc.Count == 1)
            //{
            //    rs.Add(lsToChuc[0]);
            //}
            //List<DonViToChuc_Map> lsToChuc = _entities.DonViToChuc_Map.ToList();
            List<DonViToChuc_Map> lsCha = new List<DonViToChuc_Map>();
            foreach (var item in lsToChuc)
            {
                if (item.ToChucChaId == idtochuc)
                {
                    lsCha.Add(item);
                }
            }
            foreach (var item in lsCha)
            {
                rs.Add(new DonViToChuc_Map {
                    Id = item.Id,
                    TenToChuc = item.TenToChuc
                });
                AddChildToChuc(lsToChuc, rs, item.Id);
            }
            return rs;
        }

        public void AddChildToChuc(List<DonViToChuc_Map> lsToChuc, List<DonViToChuc_Map> rs, int parentId)
        {
            List<DonViToChuc_Map> result = new List<DonViToChuc_Map>();
            foreach (var item in lsToChuc)
            {
                if (item.ToChucChaId == parentId)
                {
                    result.Add(item);
                }
            }
            try
            {
                foreach (var item in result)
                {
                    rs.Add(new DonViToChuc_Map {
                        Id = item.Id,
                        TenToChuc = item.TenToChuc
                    });
                    AddChildToChuc(lsToChuc, rs, item.Id);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<DoanVienChucVuModel> getListDoanVienChucVu(int idtochuc)
        {
            List<DoanVienChucVuModel> result = new List<DoanVienChucVuModel>();
            try
            {
                string tentc = _entities.DonViToChuc_Map.Find(idtochuc).TenToChuc;
                string sql = "select * from NguoiDung where NguoiDungId in (select NguoiDungId from NguoiDung_ToChuc where ToChucId = " + idtochuc + ")";
                DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        List<chucvu_tochuc> _chucvu = new List<chucvu_tochuc>();
                        string sql_cv = "select cv.TenChucVu, tc.TenToChuc from DoanVien_KiemNhiem dv left join ChucVu cv on cv.ChucVuId = dv.ChucVuId left join DonViToChuc_Map tc on tc.Id = dv.ToChucId where dv.NguoiDungId = " + dt.Rows[i]["NguoiDungId"].ToString();
                        DataTable dt_cv = _sqlAccess.getDataFromSql(sql_cv, "").Tables[0];
                        if (dt_cv.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt_cv.Rows.Count; j++)
                            {
                                _chucvu.Add(new chucvu_tochuc
                                {
                                    tencv = dt_cv.Rows[j][0].ToString(),
                                    tentochuc = dt_cv.Rows[j][1].ToString()
                                });
                            }
                        }
                        DoanVienChucVuModel _new = new DoanVienChucVuModel
                        {
                            NguoiDungId = int.Parse(dt.Rows[i]["NguoiDungId"].ToString()),
                            ChucVu = _chucvu,
                            HoTen = dt.Rows[i]["HoTen"].ToString(),
                            NgaySinh = DateTime.Parse(dt.Rows[i]["NgaySinh"].ToString()),
                            ToChuc = tentc,
                            GioiTinh = "Nam"
                        };
                        result.Add(_new);
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return result;
                throw;
            }
        }

        public List<DoanVienBienDong> GetListDoanVienBienDong(int idtochuc, string currentid)
        {
            List<DoanVienBienDong> rs = new List<DoanVienBienDong>();
            List<string> str = new List<string>();
            if (currentid != "")
            {
                str = currentid.Split(',').ToList();
            }
            string sql = "select * from NguoiDung where NguoiDungId in (select NguoiDungId from NguoiDung_ToChuc where ToChucId = " + idtochuc + ")";
            DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool isCheck = false;
                    if (str.Count > 0)
                    {
                        for (int j = 0; j < str.Count; j++)
                        {
                            if (str[j] == dt.Rows[i]["NguoiDungId"].ToString())
                            {
                                isCheck = true;
                                break;
                            }
                        }
                    }
                    DoanVienBienDong _new = new DoanVienBienDong
                    {
                        DoanVienId = int.Parse(dt.Rows[i]["NguoiDungId"].ToString()),
                        HoTen = dt.Rows[i]["HoTen"].ToString(),
                        NgaySinh = DateTime.Parse(dt.Rows[i]["NgaySinh"].ToString()),
                        GioiTinh = bool.Parse(dt.Rows[i]["GioiTinh"].ToString()),
                        Selected = isCheck
                    };
                    rs.Add(_new);
                }
            }
            return rs;
        }

        public List<NguoiDung> GetListDetailByIdNguoiDung(string idnguoidung)
        {
            List<NguoiDung> rs = new List<NguoiDung>();
            try
            {
                string sql = "select * from NguoiDung where NguoiDungId in (" + idnguoidung + ")";
                DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
                List<string> ls = idnguoidung.Split(',').ToList();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string tochuc = "";
                        string sql_tc = "select ToChucId from NguoiDung_ToChuc where NguoiDungId = " + ls[i];
                        DataTable dt_tc = _sqlAccess.getDataFromSql(sql_tc, "").Tables[0];
                        if (dt_tc.Rows.Count > 0)
                        {
                            tochuc = _entities.DonViToChuc_Map.Find(dt_tc.Rows[0][0]).TenToChuc;
                        }
                        NguoiDung _new = new NguoiDung
                        {
                            NguoiDungId = int.Parse(dt.Rows[i]["NguoiDungId"].ToString()),
                            HoTen = dt.Rows[i]["HoTen"].ToString(),
                            NoiKetNapDoan = tochuc
                        };
                        rs.Add(_new);
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

        public List<DoanVienKiemNhiemModel> getListDoanVienKiemNhiem(int idnguoidung)
        {
            List<DoanVienKiemNhiemModel> rs = new List<DoanVienKiemNhiemModel>();
            string sql = "select kn.Id, tc.TenToChuc, cv.TenChucVu, kn.ThoiDiemApDung from DoanVien_KiemNhiem kn left join DonViToChuc_Map tc on tc.Id = kn.ToChucId left join ChucVu cv on cv.ChucVuId = kn.ChucVuId where kn.NguoiDungId = " + idnguoidung + " and kn.ThoiDiemApDung is not NULL";
            DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DoanVienKiemNhiemModel _new = new DoanVienKiemNhiemModel
                    {
                        KiemNhiemId = int.Parse(dt.Rows[i]["Id"].ToString()),
                        ToChuc = dt.Rows[i]["TenToChuc"].ToString(),
                        ChucVu = dt.Rows[i]["TenChucVu"].ToString(),
                        NgayApDung = DateTime.Parse(dt.Rows[i]["ThoiDiemApDung"].ToString())
                    };
                    rs.Add(_new);
                }
            }
            return rs;
        }
    }
}