using QLDV.Models;
using QLDV.Models.DAL;
using QLDV.Models.DAL.NguoiDungService;
using QLDV.Models.DAL.ToChucService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class NguoiDungController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private SqlDataAccess _sqlAccess = new SqlDataAccess();
        NguoiDungService _ndsv = new NguoiDungService();
        ToChucService _tcsv = new ToChucService();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListNguoiDung()
        {
            int iddonvi = int.Parse(Request["iddonvi"]);
            List<int> rs = _ndsv.getDonViChildByDonViId(iddonvi);
            _entities.Configuration.ProxyCreationEnabled = false;
            return Json(new { data = (from item in _entities.NguoiDungs where rs.Contains(item.DonViId) select item).ToList() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListDoanVienChucVu()
        {
            int idtochuc = int.Parse(Request["idtochuc"]);
            return Json(new { data = _ndsv.getListDoanVienChucVu(idtochuc) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListDoanVienBienDong()
        {
            int idtochuc = int.Parse(Request["idtochuc"]);
            string currentid = Request["currentid"];
            return Json(new { data = _ndsv.GetListDoanVienBienDong(idtochuc, currentid) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListDetailDoanVienById()
        {
            string idnguoidung = Request["idnguoidung"];
            return Json(_ndsv.GetListDetailByIdNguoiDung(idnguoidung), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListNguoiDungServerSide()
        {
            UserData result = new UserData();
            _entities.Configuration.ProxyCreationEnabled = false;
            string search = Request["search[value]"];
            string draw = Request["draw"];
            //string order = Request["order[0][column]"];
            //string orderDir = Request["order[0][dir]"];
            int startRec = Convert.ToInt32(Request["start"]);
            int pageSize = Convert.ToInt32(Request["length"]);
            List<NguoiDung> rs = _entities.NguoiDungs.ToList();

            int total_record = rs.Count;
            if (!string.IsNullOrEmpty(search) &&
                !string.IsNullOrWhiteSpace(search))
            {
                // Apply search
                rs = rs.Where(p => p.TaiKhoan.ToString().ToLower().Contains(search.ToLower()) ||
                                       p.HoTen.ToLower().Contains(search.ToLower())).ToList();
            }
            int recFilter = rs.Count;
            rs = rs.Skip(startRec).Take(pageSize).ToList();
            result.draw = Convert.ToInt32(draw);
            result.recordsTotal = total_record;
            result.recordsFiltered = recFilter;
            result.data = rs;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNguoiDungById()
        {
            _entities.Configuration.ProxyCreationEnabled = false;
            int idnguoidung = int.Parse(Request["idnguoidung"]);
            return Json(_entities.NguoiDungs.Find(idnguoidung), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListDoanVienThuocToChuc()
        {
            int iddonvi = int.Parse(Request["iddonvi"]);
            int idtochuc = int.Parse(Request["idtochuc"]);
            return Json(new { data = _tcsv.getListDoanVienToChuc(iddonvi, idtochuc) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string UpdateNguoiDung(NguoiDung _objNguoiDung)
        {
            if (_objNguoiDung.NguoiDungId == 0)
            {
                try
                {
                    NguoiDung _new = new NguoiDung();
                    _new.DonViId = _objNguoiDung.DonViId;
                    _new.HoTen = _objNguoiDung.HoTen;
                    _new.HasAccount = false;
                    _new.QuyenKhaiThacDonVi = 0;
                    _new.QuyenKhaiThacToChuc = 0;
                    _new.TaiKhoan = _objNguoiDung.TaiKhoan;
                    _new.GioiTinh = _objNguoiDung.GioiTinh;
                    _new.NgaySinh = _objNguoiDung.NgaySinh;
                    _new.GioiTinh = _objNguoiDung.GioiTinh;
                    _new.NgayTao = DateTime.Now;
                    _entities.NguoiDungs.Add(_new);
                    _entities.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    return "fail";
                }
                return "addsuccess";
            }
            else
            {
                NguoiDung _update = _entities.NguoiDungs.Find(_objNguoiDung.NguoiDungId);
                _update.DonViId = _objNguoiDung.DonViId;
                _update.HoTen = _objNguoiDung.HoTen;
                _update.TaiKhoan = _objNguoiDung.TaiKhoan;
                _entities.SaveChanges();
                return "updatesuccess";
            }
        }

        [HttpPost]
        public bool CapTaiKhoan()
        {
            int idnguoidung = int.Parse(Request["idnguoidung"]);
            try
            {
                var nguoidung = _entities.NguoiDungs.Find(idnguoidung);
                nguoidung.HasAccount = true;
                nguoidung.MatKhau = "Vnpt@123";
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        }

        [HttpPost]
        public bool XoaTaiKhoan()
        {
            int idnguoidung = int.Parse(Request["idnguoidung"]);
            try
            {
                var nguoidung = _entities.NguoiDungs.Find(idnguoidung);
                nguoidung.HasAccount = false;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        [HttpPost]
        public bool SetRoleForId()
        {
            try
            {
                int idnguoidung = int.Parse(Request["idnguoidung"]);
                int idnhomquyen = int.Parse(Request["idnhomquyen"]);
                int id_role_donvi = int.Parse(Request["id_role_donvi"]);
                int id_role_tochuc = int.Parse(Request["id_role_tochuc"]);
                var _delete = _entities.NguoiDungNhomQuyen_Map.FirstOrDefault(x => x.NguoiDungId == idnguoidung);
                if (_delete != null)
                {
                    _entities.NguoiDungNhomQuyen_Map.Remove(_delete);
                    _entities.SaveChanges();
                }
                if (idnhomquyen != 0)
                {
                    NguoiDungNhomQuyen_Map _new = new NguoiDungNhomQuyen_Map();
                    _new.NguoiDungId = idnguoidung;
                    _new.NhomQuyenId = idnhomquyen;
                    _entities.NguoiDungNhomQuyen_Map.Add(_new);
                    _entities.SaveChanges();
                }
                NguoiDung _update = _entities.NguoiDungs.Find(idnguoidung);
                _update.QuyenKhaiThacDonVi = id_role_donvi;
                _update.QuyenKhaiThacToChuc = id_role_tochuc;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public JsonResult GetNguoiDungInfoById()
        {
            UserRole rs = new UserRole();
            rs.NhomQuyenId = 0;
            int idnguoidung = int.Parse(Request["idnguoidung"]);
            var _objnq = _entities.NguoiDungNhomQuyen_Map.FirstOrDefault(x => x.NguoiDungId == idnguoidung);
            if (_objnq != null)
            {
                rs.NhomQuyenId = _objnq.NhomQuyenId;
            }
            string sql = "select nd.QuyenKhaiThacToChuc, nd.QuyenKhaiThacDonVi, tc.TenToChuc, dv.TenDonVi from NguoiDung nd left join DonViToChuc_Map tc on tc.Id = nd.QuyenKhaiThacToChuc left join DonVi dv on dv.DonViId = nd.QuyenKhaiThacDonVi where nd.NguoiDungId = " + idnguoidung;
            DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                rs.Role_DonVi = int.Parse(dt.Rows[0]["QuyenKhaiThacDonVi"].ToString());
                rs.Role_ToChuc = int.Parse(dt.Rows[0]["QuyenKhaiThacToChuc"].ToString());
                rs.ToChuc = dt.Rows[0]["TenToChuc"].ToString();
                rs.DonVi = dt.Rows[0]["TenDonVi"].ToString();
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string GetTenToChucById()
        {
            try
            {
                int idtochuc = int.Parse(Request["idtochuc"]);
                var _objtc = _entities.DonViToChuc_Map.Find(idtochuc);
                return _objtc.TenToChuc;
            }
            catch (Exception)
            {
                return "";
                throw;
            }
            
        }

        [HttpGet]
        public string getDonViIdByToChucId()
        {
            try
            {
                int idtochuc = int.Parse(Request["idtochuc"]);
                var obj = _entities.DonViToChuc_Map.Find(idtochuc);
                return obj.DonViId.ToString();
            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }

        [HttpGet]
        public string getPreDonViIdByToChucId()
        {
            try
            {
                int idloaihtc = int.Parse(Request["idloaihtc"]);
                int iddonvi = int.Parse(Request["iddonvi"]);
                var _obj = _entities.DonViToChuc_Map.FirstOrDefault(x => x.DonViId == iddonvi && x.LoaiHinhToChucId == idloaihtc);
                return _obj.Id.ToString();
            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }
        [HttpGet]
        public string GetNameById()
        {
            int idnguoidung = int.Parse(Request["idnguoidung"]);
            return _entities.NguoiDungs.Find(idnguoidung).HoTen;
        }
        [HttpGet]
        public string GetCapToChucIdByNguoiDungId()
        {
            try
            {
                int idnguoidung = int.Parse(Request["idnguoidung"]);
                int idtochuc = 0;
                var objNguoiDungToChuc = _entities.NguoiDung_ToChuc.FirstOrDefault(x => x.NguoiDungId == idnguoidung);
                if (objNguoiDungToChuc == null)
                {
                    return "not exist";
                }
                else
                {
                    idtochuc = objNguoiDungToChuc.ToChucId;
                    var _obj = _entities.DonViToChuc_Map.Find(idtochuc);
                    return _obj.CapToChucId.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public bool UpdateDoanVienToChuc()
        {
            try
            {
                int idtochuc = int.Parse(Request["idtochuc"]);
                string idnguoidung = Request["idnguoidung"];
                string idchucvu = Request["idchucvu"];
                string[] arraynguoidung = idnguoidung.Split(' ');
                string[] arraychucvu = idchucvu.Split(' ');
                string sql_delete = "delete from NguoiDung_ToChuc where ToChucId = " + idtochuc;
                string sql_deletecv = "delete from DoanVien_KiemNhiem where ToChucId = " + idtochuc;
                if (_sqlAccess.DbCommand(sql_delete) && _sqlAccess.DbCommand(sql_deletecv))
                {
                    if (arraynguoidung.Length > 0)
                    {
                        for (int i = 0; i < arraynguoidung.Length; i++)
                        {
                            NguoiDung_ToChuc _new = new NguoiDung_ToChuc
                            {
                                NguoiDungId = int.Parse(arraynguoidung[i]),
                                ToChucId = idtochuc,
                                ChucVuId = int.Parse(arraychucvu[i])
                            };
                            DoanVien_KiemNhiem _dvkn = new DoanVien_KiemNhiem
                            {
                                NguoiDungId = int.Parse(arraynguoidung[i]),
                                ChucVuId = int.Parse(arraychucvu[i]),
                                ToChucId = idtochuc
                            };
                            _entities.NguoiDung_ToChuc.Add(_new);
                            _entities.DoanVien_KiemNhiem.Add(_dvkn);
                            _entities.SaveChanges();
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        }

        public class UserData
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<NguoiDung> data { get; set; }
        }

        public class UserRole
        {
            public int NhomQuyenId { get; set; }
            public int Role_DonVi { get; set; }
            public int Role_ToChuc { get; set; }
            public string DonVi { get; set; }
            public string ToChuc { get; set; }
        }
    }
}