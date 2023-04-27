using Newtonsoft.Json;
using QLDV.Models;
using QLDV.Models.DAL;
using QLDV.Models.DAL.NghiepVuService;
using QLDV.Models.DAL.NguoiDungService;
using QLDV.Models.DAL.ProcedureService;
using QLDV.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class NghiepVuController : Controller
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        private SqlDataAccess _sqlAccess = new SqlDataAccess();
        NguoiDungService _ndsv = new NguoiDungService();
        private ProcedureService _prosv = new ProcedureService();
        NghiepVuService _nvsv = new NghiepVuService();
        // GET: NghiepVu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult kiemnhiem()
        {
            return View();
        }

        public ActionResult nhapbiendong()
        {
            return View();
        }

        public ActionResult xulybiendong()
        {
            return View();
        }

        public JsonResult LoadListChucVuKiemNhiem()
        {
            int idnguoidung = int.Parse(Request["idnguoidung"]);
            return Json(new { data = _ndsv.getListDoanVienKiemNhiem(idnguoidung) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListDsBienDongNhap()
        {
            int idnvnhap = int.Parse(Request["idnhanvien"]);
            return Json(new { data = _nvsv.GetListBienDongNhapByNhanVien(idnvnhap) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListDsBienDongXuLy()
        {
            int idtochucnhan = int.Parse(Request["idtochucnhan"]);
            return Json(new { data = _nvsv.GetListBienDongXuLyByToChucNhan(idtochucnhan) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListDsBienDongByIdDoanVien()
        {
            int iddoanvien = int.Parse(Request["iddoanvien"]);
            return Json(new { data = _nvsv.GetDanhSachBienDongByDoanVienId(iddoanvien) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListChiTietBienDong()
        {
            int idbiendongdoanvien = int.Parse(Request["idbiendongdoanvien"]);
            return Json(new { data = _nvsv.GetChiTietBienDongById(idbiendongdoanvien) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult QuyTrinh_ChiTiet_CauHinh()
        {
            List<QuyTrinhChiTiet> rs = new List<QuyTrinhChiTiet>();
            try
            {
                int idbiendong = int.Parse(Request["idbiendong"]);
                int idloaihtc = int.Parse(Request["idloaihtc"]);
                int idcaptochuc = int.Parse(Request["idcaptochuc"]);
                int quytrinhid = _entities.BienDong_QuyTrinh.FirstOrDefault(x => x.BienDongId == idbiendong && x.LoaiHinhToChucId == idloaihtc && x.CapToChucId == idcaptochuc).QuyTrinhId;
                return Json(_prosv.getListQuyTrinhChiTietById(quytrinhid), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(rs, JsonRequestBehavior.AllowGet);
                throw;
            }
            
        }

        public JsonResult GetToChucNhanBienDong()
        {
            List<DonViToChuc_Map> rs = new List<DonViToChuc_Map>();
            try
            {
                int idkttochuc = int.Parse(Request["idkttochuc"]);
                int idcaptochuc = int.Parse(Request["idcaptochuc"]);
                int idloaihtc = int.Parse(Request["idloaihtc"]);
                List<DonViToChuc_Map> donViToChuc_Maps = _entities.DonViToChuc_Map.Where(x => x.CapToChucId == idcaptochuc && x.LoaiHinhToChucId == idloaihtc).ToList();
                //rs = _ndsv.GetToChucChildByToChucId(idkttochuc, idcaptochuc, idloaihtc, donViToChuc_Maps);
                return Json(donViToChuc_Maps, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(rs, JsonRequestBehavior.AllowGet);
                throw;
            }
            
        }

        [HttpPost]
        public bool XoaKiemNhiem()
        {
            try
            {
                int idkiemnhiem = int.Parse(Request["idkiemnhiem"]);
                var _delete = _entities.DoanVien_KiemNhiem.Find(idkiemnhiem);
                _entities.DoanVien_KiemNhiem.Remove(_delete);
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
        public bool UpdateKiemNhiem(DoanVien_KiemNhiem _obj)
        {
            try
            {
                DoanVien_KiemNhiem _new = new DoanVien_KiemNhiem();
                _new.ChucVuId = _obj.ChucVuId;
                _new.LyDo = _obj.LyDo;
                _new.NguoiDungId = _obj.NguoiDungId;
                _new.ToChucId = _obj.ToChucId;
                _new.ThoiDiemApDung = _obj.ThoiDiemApDung;
                _entities.DoanVien_KiemNhiem.Add(_new);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        
        [HttpGet]
        public int GetIdBienDongByBienDongDv()
        {
            int idbiendongdoanvien = int.Parse(Request["idbiendongdoanvien"]);
            return _entities.BienDongDoanViens.Find(idbiendongdoanvien).BienDongId;
        }

        public JsonResult GetTrangThaiTiepNhan()
        {
            return Json(_entities.TrangThaiBienDongs.Where(x => x.Status == false).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTrangThaiXuLy()
        {
            return Json(_entities.TrangThaiBienDongs.Where(x => x.Status == true).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBienDongDetailById()
        {
            int idbiendongdoanvien = int.Parse(Request["idbiendongdoanvien"]);
            return Json(_entities.BienDongDoanViens.Find(idbiendongdoanvien), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadFile()
        {
            string fullpath = Request["fullpath"];
            string strfull = fullpath.Replace(",", "\\");
            string file = Request["file"];
            string type = Request["type"];
            return File(strfull, type, file);
        }

        //Hàm update biến động
        //-1: Lỗi khi upload file đính kèm
        //-2: Lỗi khi cập nhật biến động
        //0: Thêm biến động thành công
        //1: Không cho phép cập nhật do trạng thái đã xử lý
        //2: Cập nhật biến động thành công
        public int UpdateBienDong()
        {
            int idbiendongdoanvien = int.Parse(Request.Form.Get("idbiendongdoanvien"));
            int idbiendong = 0;
            if (Request.Form.Get("idbiendong") != "null")
            {
                idbiendong = int.Parse(Request.Form.Get("idbiendong"));
            }
            int idnvnhap = int.Parse(Request.Form.Get("idnvnhap"));
            int idtochucgui = int.Parse(Request.Form.Get("idtochucgui"));
            int idcaptochucnhan = 0;
            if (Request.Form.Get("idcaptochucnhan") != "null")
            {
                idcaptochucnhan = int.Parse(Request.Form.Get("idcaptochucnhan"));
            }
            int idtochucnhan = 0;
            if (Request.Form.Get("idtochucnhan") != "null")
            {
                idtochucnhan = int.Parse(Request.Form.Get("idtochucnhan"));
            }
            string ma = Request.Form.Get("ma");
            string soqd = Request.Form.Get("soqd");
            DateTime ngaybiendong = DateTime.Parse(Request.Form.Get("ngaybiendong"));
            string noidung = Request.Form.Get("noidung");
            string dsdoanvien = Request.Form.Get("dsdoanvien");
            string output_filedinhkem = "";
            
            if (idbiendongdoanvien == 0)
            {
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        string fn = _entities.BienDongs.Find(idbiendong).FolderUpload;
                        string fpath = Server.MapPath("~/Uploads/" + fn + "/");
                        if (!Directory.Exists(fpath))
                        {
                            Directory.CreateDirectory(fpath);
                        }
                        HttpFileCollectionBase files = Request.Files;
                        List<KeyValuePair<string, string>> lsFileDownload = new List<KeyValuePair<string, string>>();
                        for (int i = 0; i < files.Count; i++)
                        {
                            //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                            string filename = Path.GetFileName(Request.Files[i].FileName);

                            HttpPostedFileBase file = files[i];
                            string fname, ftype;
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }
                            fname = Path.Combine(fpath, fname);
                            ftype = file.ContentType;
                            if (!System.IO.File.Exists(fname))
                            {
                                file.SaveAs(fname);
                            }
                            lsFileDownload.Add(new KeyValuePair<string, string>(fname, ftype));
                        }
                        output_filedinhkem = JsonConvert.SerializeObject(lsFileDownload);
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
                try
                {
                    BienDongDoanVien _newBddv = new BienDongDoanVien
                    {
                        BienDongId = idbiendong,
                        NgayBienDong = ngaybiendong,
                        QuyetDinh = soqd,
                        NoiDung = noidung,
                        TrangThaiXuLy = 0,
                        IdCapToChucNhan = idcaptochucnhan,
                        IdToChucNhan = idtochucnhan,
                        NhanVienNhap = idnvnhap,
                        ToChucNhap = idtochucgui,
                        DsDoanVien = dsdoanvien,
                        FileDinhKem = output_filedinhkem
                    };
                    _entities.BienDongDoanViens.Add(_newBddv);
                    _entities.SaveChanges();
                    List<string> lsDv = dsdoanvien.Split(',').ToList();
                    for (int i = 0; i < lsDv.Count; i++)
                    {
                        BienDong_DoanVien_Map _map = new BienDong_DoanVien_Map
                        {
                            BienDongDoanVienId = _newBddv.Id,
                            DoanVienId = int.Parse(lsDv[i])
                        };
                        _entities.BienDong_DoanVien_Map.Add(_map);
                        _entities.SaveChanges();
                        BienDongDoanVienChiTiet _newChitiet = new BienDongDoanVienChiTiet
                        {
                            BienDongDoanVienId = _map.Id,
                            IdToChucGui = idtochucgui,
                            IdToChucNhan = idtochucnhan,
                            NgayThucHien = ngaybiendong,
                            NoiDung = noidung,
                            NhanVienXuLy = idnvnhap,
                            TrangThai = 0,
                            FileDinhKem = output_filedinhkem
                        };
                        _entities.BienDongDoanVienChiTiets.Add(_newChitiet);
                        _entities.SaveChanges();
                    }
                    return 0;
                }
                catch (Exception)
                {
                    return -2;
                    throw;
                }

            }
            else
            {
                BienDongDoanVien _update = _entities.BienDongDoanViens.Find(idbiendongdoanvien);
                if (_update.TrangThaiXuLy != 0)
                {
                    return 1;
                }
                else
                {
                    string dulieudinhkem = _update.FileDinhKem;
                    if (dulieudinhkem == "")
                    {
                        if (Request.Files.Count > 0)
                        {
                            try
                            {
                                string fn = _entities.BienDongs.Find(idbiendong).FolderUpload;
                                string fpath = Server.MapPath("~/Uploads/" + fn + "/");
                                if (!Directory.Exists(fpath))
                                {
                                    Directory.CreateDirectory(fpath);
                                }
                                HttpFileCollectionBase files = Request.Files;
                                List<KeyValuePair<string, string>> lsFileDownload = new List<KeyValuePair<string, string>>();
                                for (int i = 0; i < files.Count; i++)
                                {
                                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                                    string filename = Path.GetFileName(Request.Files[i].FileName);

                                    HttpPostedFileBase file = files[i];
                                    string fname, ftype;
                                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                                    {
                                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                        fname = testfiles[testfiles.Length - 1];
                                    }
                                    else
                                    {
                                        fname = file.FileName;
                                    }
                                    fname = Path.Combine(fpath, fname);
                                    ftype = file.ContentType;
                                    if (!System.IO.File.Exists(fname))
                                    {
                                        file.SaveAs(fname);
                                    }
                                    lsFileDownload.Add(new KeyValuePair<string, string>(fname, ftype));
                                }
                                output_filedinhkem = JsonConvert.SerializeObject(lsFileDownload);
                            }
                            catch (Exception ex)
                            {
                                return -1;
                            }
                        }
                    }
                    else
                    {
                        if (Request.Files.Count > 0)
                        {
                            try
                            {
                                var ls = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(dulieudinhkem);
                                string fn = _entities.BienDongs.Find(idbiendong).FolderUpload;
                                string fpath = Server.MapPath("~/Uploads/" + fn + "/");
                                if (!Directory.Exists(fpath))
                                {
                                    Directory.CreateDirectory(fpath);
                                }
                                HttpFileCollectionBase files = Request.Files;
                                for (int i = 0; i < files.Count; i++)
                                {
                                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                                    string filename = Path.GetFileName(Request.Files[i].FileName);

                                    HttpPostedFileBase file = files[i];
                                    string fname, ftype;
                                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                                    {
                                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                        fname = testfiles[testfiles.Length - 1];
                                    }
                                    else
                                    {
                                        fname = file.FileName;
                                    }
                                    fname = Path.Combine(fpath, fname);
                                    ftype = file.ContentType;
                                    if (!System.IO.File.Exists(fname))
                                    {
                                        file.SaveAs(fname);
                                    }
                                    bool ischeck = true;
                                    for (int j = 0; j < ls.Count; j++)
                                    {
                                        if (ls[j].Key == fname && ls[j].Value == ftype)
                                        {
                                            ischeck = false;
                                            break;
                                        }
                                    }
                                    if (ischeck)
                                    {
                                        ls.Add(new KeyValuePair<string, string>(fname, ftype));
                                    }
                                }
                                output_filedinhkem = JsonConvert.SerializeObject(ls);
                            }
                            catch (Exception ex)
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            output_filedinhkem = dulieudinhkem;
                        }
                    }
                    //_update.BienDongId = idbiendong;
                    _update.FileDinhKem = output_filedinhkem;
                    //_update.IdToChucNhan = idtochucnhan;
                    //_update.IdCapToChucNhan = idcaptochucnhan;
                    _update.NgayBienDong = ngaybiendong;
                    _update.QuyetDinh = soqd;
                    _update.NoiDung = noidung;
                    _entities.SaveChanges();
                    return 2;
                }
            }
        }

        public int ProcessBienDong()
        {
            int idbiendongdoanvien = int.Parse(Request.Form.Get("idbiendongdoanvien"));
            int idbiendong = 0;
            if (Request.Form.Get("idbiendong") != "null")
            {
                idbiendong = int.Parse(Request.Form.Get("idbiendong"));
            }
            int idnvnhap = int.Parse(Request.Form.Get("idnvnhap"));
            int idtochucgui = int.Parse(Request.Form.Get("idtochucgui"));
            int idcaptochucnhan = 0;
            if (Request.Form.Get("idcaptochucnhan") != "null")
            {
                idcaptochucnhan = int.Parse(Request.Form.Get("idcaptochucnhan"));
            }
            int idtochucnhan = 0;
            if (Request.Form.Get("idtochucnhan") != "null")
            {
                idtochucnhan = int.Parse(Request.Form.Get("idtochucnhan"));
            }
            string ma = Request.Form.Get("ma");
            string soqd = Request.Form.Get("soqd");
            DateTime ngaybiendong = DateTime.Parse(Request.Form.Get("ngaybiendong"));
            string noidung = Request.Form.Get("noidung");
            string dsdoanvien = Request.Form.Get("dsdoanvien");
            try
            {
                if (ma == "chuyen_buoc")
                {
                    BienDongDoanVien _update = _entities.BienDongDoanViens.Find(idbiendongdoanvien);
                    string output_filedinhkem = get_fileDinhKem(_update, Request, idbiendong);
                    string output_filedinhkemstep = get_fileDinhKemStep(Request, idbiendong);
                    //_update.BienDongId = idbiendong;
                    _update.FileDinhKem = output_filedinhkem;
                    _update.TrangThaiXuLy = 1;
                    _update.IdToChucNhan = idtochucnhan;
                    _update.IdCapToChucNhan = idcaptochucnhan;
                    _update.NgayBienDong = ngaybiendong;
                    //_update.QuyetDinh = soqd;
                    _update.NoiDung = noidung;
                    _entities.SaveChanges();
                    List<string> lsDv = dsdoanvien.Split(',').ToList();
                    for (int i = 0; i < lsDv.Count; i++)
                    {
                        string sql = "select Id from BienDong_DoanVien_Map where BienDongDoanVienId = " + idbiendongdoanvien + " and DoanVienId = " + int.Parse(lsDv[i]);
                        int id = int.Parse(_sqlAccess.getDataFromSql(sql, "").Tables[0].Rows[0][0].ToString());
                        BienDongDoanVienChiTiet _newChitiet = new BienDongDoanVienChiTiet
                        {
                            BienDongDoanVienId = id,
                            IdToChucGui = idtochucgui,
                            IdToChucNhan = idtochucnhan,
                            NgayThucHien = ngaybiendong,
                            NoiDung = noidung,
                            NhanVienXuLy = idnvnhap,
                            TrangThai = 1,
                            FileDinhKem = output_filedinhkemstep
                        };
                        _entities.BienDongDoanVienChiTiets.Add(_newChitiet);
                        _entities.SaveChanges();
                    }
                }
                else
                {
                    //Nếu mã là ket_thuc
                    //TODO
                    var _biendong = _entities.BienDongs.Find(idbiendong);
                    BienDongDoanVien _update = _entities.BienDongDoanViens.Find(idbiendongdoanvien);
                    string output_filedinhkem = get_fileDinhKem(_update, Request, idbiendong);
                    string output_filedinhkemstep = get_fileDinhKemStep(Request, idbiendong);
                    //_update.BienDongId = idbiendong;
                    _update.FileDinhKem = output_filedinhkem;
                    _update.TrangThaiXuLy = 2;
                    _update.IdToChucNhan = idtochucnhan;
                    _update.IdCapToChucNhan = idcaptochucnhan;
                    _update.NgayBienDong = ngaybiendong;
                    //_update.QuyetDinh = soqd;
                    _update.NoiDung = noidung;
                    _entities.SaveChanges();
                    int biendong_map = _entities.BienDong_DoanVien_Map.FirstOrDefault(x => x.BienDongDoanVienId == idbiendongdoanvien).Id;
                    int idtochucgui_first = _entities.BienDongDoanVienChiTiets.FirstOrDefault(x => x.BienDongDoanVienId == biendong_map && x.TrangThai == 0).IdToChucGui;
                    List<string> lsDv = dsdoanvien.Split(',').ToList();
                    for (int i = 0; i < lsDv.Count; i++)
                    {
                        string sql = "select Id from BienDong_DoanVien_Map where BienDongDoanVienId = " + idbiendongdoanvien + " and DoanVienId = " + int.Parse(lsDv[i]);
                        int id = int.Parse(_sqlAccess.getDataFromSql(sql, "").Tables[0].Rows[0][0].ToString());
                        BienDongDoanVienChiTiet _newChitiet = new BienDongDoanVienChiTiet
                        {
                            BienDongDoanVienId = id,
                            IdToChucGui = idtochucgui,
                            IdToChucNhan = idtochucnhan,
                            NgayThucHien = ngaybiendong,
                            NoiDung = noidung,
                            NhanVienXuLy = idnvnhap,
                            TrangThai = 2,
                            FileDinhKem = output_filedinhkemstep
                        };
                        _entities.BienDongDoanVienChiTiets.Add(_newChitiet);
                        _entities.SaveChanges();
                        if (_biendong.IsChanged == true)
                        {
                            int idnguoidungdel = int.Parse(lsDv[i]);
                            var _deletedoanvientc = _entities.NguoiDung_ToChuc.FirstOrDefault(x => x.ToChucId == idtochucgui_first && x.NguoiDungId == idnguoidungdel);
                            var _deletechucvu = _entities.DoanVien_KiemNhiem.FirstOrDefault(x => x.ToChucId == idtochucgui_first && x.NguoiDungId == idnguoidungdel);
                            if (_deletedoanvientc != null)
                            {
                                _entities.NguoiDung_ToChuc.Remove(_deletedoanvientc);
                                _entities.SaveChanges();
                            }
                            if (_deletechucvu != null)
                            {
                                _entities.DoanVien_KiemNhiem.Remove(_deletechucvu);
                                _entities.SaveChanges();
                            }
                            if (_biendong.FolderUpload == "chuyen_di")
                            {
                                NguoiDung_ToChuc _new = new NguoiDung_ToChuc
                                {
                                    ToChucId = idtochucnhan,
                                    NguoiDungId = idnguoidungdel,
                                    ChucVuId = 5
                                };
                                DoanVien_KiemNhiem _kiemnhiem = new DoanVien_KiemNhiem
                                {
                                    ToChucId = idtochucnhan,
                                    NguoiDungId = idnguoidungdel,
                                    ChucVuId = 5
                                };
                                _entities.NguoiDung_ToChuc.Add(_new);
                                _entities.DoanVien_KiemNhiem.Add(_kiemnhiem);
                                _entities.SaveChanges();
                            }
                        }
                    }
                    
                }
                return 2;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private string get_fileDinhKemStep(HttpRequestBase request, int idbiendong)
        {
            string output_filedinhkem = "";
            if (request.Files.Count > 0)
            {
                try
                {
                    string fn = _entities.BienDongs.Find(idbiendong).FolderUpload;
                    string fpath = Server.MapPath("~/Uploads/" + fn + "/");
                    if (!Directory.Exists(fpath))
                    {
                        Directory.CreateDirectory(fpath);
                    }
                    HttpFileCollectionBase files = request.Files;
                    List<KeyValuePair<string, string>> lsFileDownload = new List<KeyValuePair<string, string>>();
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                        string filename = Path.GetFileName(request.Files[i].FileName);

                        HttpPostedFileBase file = files[i];
                        string fname, ftype;
                        if (request.Browser.Browser.ToUpper() == "IE" || request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        fname = Path.Combine(fpath, fname);
                        ftype = file.ContentType;
                        if (!System.IO.File.Exists(fname))
                        {
                            file.SaveAs(fname);
                        }
                        lsFileDownload.Add(new KeyValuePair<string, string>(fname, ftype));
                    }
                    output_filedinhkem = JsonConvert.SerializeObject(lsFileDownload);
                    return output_filedinhkem;
                }
                catch (Exception ex)
                {
                    return output_filedinhkem;
                }
            }
            return output_filedinhkem;
        }

        private string get_fileDinhKem(BienDongDoanVien _update, HttpRequestBase request, int idbiendong)
        {
            string output_filedinhkem = "";
            
            string dulieudinhkem = _update.FileDinhKem;
            if (dulieudinhkem == "")
            {
                if (request.Files.Count > 0)
                {
                    try
                    {
                        string fn = _entities.BienDongs.Find(idbiendong).FolderUpload;
                        string fpath = Server.MapPath("~/Uploads/" + fn + "/");
                        if (!Directory.Exists(fpath))
                        {
                            Directory.CreateDirectory(fpath);
                        }
                        HttpFileCollectionBase files = request.Files;
                        List<KeyValuePair<string, string>> lsFileDownload = new List<KeyValuePair<string, string>>();
                        for (int i = 0; i < files.Count; i++)
                        {
                            //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                            string filename = Path.GetFileName(request.Files[i].FileName);

                            HttpPostedFileBase file = files[i];
                            string fname, ftype;
                            if (request.Browser.Browser.ToUpper() == "IE" || request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }
                            fname = Path.Combine(fpath, fname);
                            ftype = file.ContentType;
                            if (!System.IO.File.Exists(fname))
                            {
                                file.SaveAs(fname);
                            }
                            lsFileDownload.Add(new KeyValuePair<string, string>(fname, ftype));
                        }
                        output_filedinhkem = JsonConvert.SerializeObject(lsFileDownload);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else
            {
                if (request.Files.Count > 0)
                {
                    try
                    {
                        var ls = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(dulieudinhkem);
                        string fn = _entities.BienDongs.Find(idbiendong).FolderUpload;
                        string fpath = Server.MapPath("~/Uploads/" + fn + "/");
                        if (!Directory.Exists(fpath))
                        {
                            Directory.CreateDirectory(fpath);
                        }
                        HttpFileCollectionBase files = request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                            string filename = Path.GetFileName(Request.Files[i].FileName);

                            HttpPostedFileBase file = files[i];
                            string fname, ftype;
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }
                            fname = Path.Combine(fpath, fname);
                            ftype = file.ContentType;
                            if (!System.IO.File.Exists(fname))
                            {
                                file.SaveAs(fname);
                            }
                            bool ischeck = true;
                            for (int j = 0; j < ls.Count; j++)
                            {
                                if (ls[j].Key == fname && ls[j].Value == ftype)
                                {
                                    ischeck = false;
                                    break;
                                }
                            }
                            if (ischeck)
                            {
                                ls.Add(new KeyValuePair<string, string>(fname, ftype));
                            }
                        }
                        output_filedinhkem = JsonConvert.SerializeObject(ls);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    output_filedinhkem = dulieudinhkem;
                }
            }
            return output_filedinhkem;
        }
    }
}