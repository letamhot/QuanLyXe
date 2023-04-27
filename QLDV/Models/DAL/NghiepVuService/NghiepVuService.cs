using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.NghiepVuService
{
    public class NghiepVuService
    {
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        public List<BienDongNhapModel> GetListBienDongNhapByNhanVien(int idnhanviennhap)
        {
            List<BienDongNhapModel> rs = new List<BienDongNhapModel>();
            string sql = "select bddv.Id, bd.TenBienDong, bddv.NgayBienDong, bddv.NoiDung, bddv.DsDoanVien, bddv.TrangThaiXuLy from BienDongDoanVien bddv left join BienDong bd on bd.BienDongId = bddv.BienDongId where bddv.NhanVienNhap = " + idnhanviennhap;
            DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    List<string> lsds = new List<string>();
                    string sql_ds = "select HoTen from NguoiDung where NguoiDungId in (" + dt.Rows[i]["DsDoanVien"].ToString() + ")";
                    DataTable dt_ds = _sqlAccess.getDataFromSql(sql_ds, "").Tables[0];
                    for (int j = 0; j < dt_ds.Rows.Count; j++)
                    {
                        lsds.Add(dt_ds.Rows[j][0].ToString());
                    }
                    BienDongNhapModel _new = new BienDongNhapModel
                    {
                        BienDongDoanVienId = int.Parse(dt.Rows[i]["Id"].ToString()),
                        TenBienDong = dt.Rows[i]["TenBienDong"].ToString(),
                        NoiDung = dt.Rows[i]["NoiDung"].ToString(),
                        NgayNhap = DateTime.Parse(dt.Rows[i]["NgayBienDong"].ToString()),
                        DanhSachDoanVien = string.Join(",", lsds),
                        TrangThaiBienDong = int.Parse(dt.Rows[i]["TrangThaiXuLy"].ToString())
                    };
                    rs.Add(_new);
                }
            }
            return rs;
        }

        public List<BienDongXuLyModel> GetListBienDongXuLyByToChucNhan(int idtochucnhan)
        {
            List<BienDongXuLyModel> rs = new List<BienDongXuLyModel>();
            string sql = "select bddv.Id, bd.TenBienDong, bddv.NgayBienDong, bddv.NoiDung, bddv.TrangThaiXuLy, nd.HoTen, tc.TenToChuc from BienDongDoanVien bddv left join BienDong bd on bd.BienDongId = bddv.BienDongId left join NguoiDung nd on nd.NguoiDungId = bddv.NhanVienNhap left join DonViToChuc_Map tc on tc.Id = bddv.ToChucNhap where bddv.IdToChucNhan = " + idtochucnhan;
            DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BienDongXuLyModel _new = new BienDongXuLyModel
                    {
                        BienDongDoanVienId = int.Parse(dt.Rows[i]["Id"].ToString()),
                        TenBienDong = dt.Rows[i]["TenBienDong"].ToString(),
                        NoiDung = dt.Rows[i]["NoiDung"].ToString(),
                        NgayNhap = DateTime.Parse(dt.Rows[i]["NgayBienDong"].ToString()),
                        NguoiYeuCau = dt.Rows[i]["HoTen"].ToString(),
                        ToChucYeuCau = dt.Rows[i]["TenToChuc"].ToString(),
                        TrangThaiBienDong = int.Parse(dt.Rows[i]["TrangThaiXuLy"].ToString())
                    };
                    rs.Add(_new);
                }
            }
            return rs;
        }

        public List<BienDongNhapModel> GetDanhSachBienDongByDoanVienId(int iddoanvien)
        {
            List<BienDongNhapModel> rs = new List<BienDongNhapModel>();
            string sql = "select bdm.Id, bd.TenBienDong, bddv.NgayBienDong, bddv.TrangThaiXuLy from BienDong_DoanVien_Map bdm left join BienDongDoanVien bddv on bddv.Id = bdm.BienDongDoanVienId left join BienDong bd on bd.BienDongId = bddv.BienDongId where bdm.DoanVienId = " + iddoanvien;
            DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BienDongNhapModel _new = new BienDongNhapModel
                    {
                        BienDongDoanVienId = int.Parse(dt.Rows[i]["Id"].ToString()),
                        TenBienDong = dt.Rows[i]["TenBienDong"].ToString(),
                        NgayNhap = DateTime.Parse(dt.Rows[i]["NgayBienDong"].ToString()),
                        TrangThaiBienDong = int.Parse(dt.Rows[i]["TrangThaiXuLy"].ToString())
                    };
                    rs.Add(_new);
                }
            }
            return rs;
        }

        public List<BienDongChiTietModel> GetChiTietBienDongById(int idbiendongdoanvien)
        {
            List<BienDongChiTietModel> rs = new List<BienDongChiTietModel>();
            string sql = "select * from BienDongDoanVienChiTiet where BienDongDoanVienId = " + idbiendongdoanvien;
            DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BienDongChiTietModel _new = new BienDongChiTietModel
                    {
                        ToChucGui = GetTenToChucById(int.Parse(dt.Rows[i]["IdToChucGui"].ToString())),
                        ToChucNhan = GetTenToChucById(int.Parse(dt.Rows[i]["IdToChucNhan"].ToString())),
                        NguoiXuLy = GetTenDoanVienById(int.Parse(dt.Rows[i]["NhanVienXuLy"].ToString())),
                        NoiDung = dt.Rows[i]["NoiDung"].ToString(),
                        NgayThucHien = DateTime.Parse(dt.Rows[i]["NgayThucHien"].ToString()),
                        FileDinhKem = dt.Rows[i]["FileDinhKem"].ToString()
                    };
                    rs.Add(_new);
                }
            }
            return rs;
        }

        private string GetTenToChucById(int idtochuc)
        {
            return _entities.DonViToChuc_Map.Find(idtochuc).TenToChuc;
        }
        private string GetTenDoanVienById(int idnguoidung)
        {
            return _entities.NguoiDungs.Find(idnguoidung).HoTen;
        }
    }
}