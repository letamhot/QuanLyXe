using DAL;
using QB_Pro_DataAccess;
using QBWaco.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;

public partial class GhiChiSoDongHo_HoaDon_SoGhiNhanh : System.Web.UI.Page
{
    public static QBWacoEntities entities = new QBWacoEntities();
    string tendangnhap = "", nguoidungid;
    DataSet ds;
    clGhiChiso objGhichiso;
    clKyCuoc objDmKycuoc;
    protected void Page_Load(object sender, EventArgs e)
    {
        nguoidungid = Session["ID"].ToString();
        ds = new DataSet();
        objDmKycuoc = new clKyCuoc();
        ds = objDmKycuoc.GetKyCuocAll();
        if (!IsPostBack)
        {
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                loadCommbox(this.drChuky, ds.Tables[0], "Id", "Kycuoc");
                loadCommbox(this.KySoSanh, ds.Tables[0], "Id", "Kycuoc");
                loadCommbox(this.kycuoccanchuyen, ds.Tables[0], "Id", "Kycuoc");
                loadCommbox(this.kycuocduocchuyen, ds.Tables[0], "Id", "Kycuoc");
                drChuky.SelectedValue = objDmKycuoc.GetMaxKyCuoc();
            }
        }
        ds = new DataSet();
        objGhichiso = new clGhiChiso();
        ds = objGhichiso.GetAllLotrinh(int.Parse(nguoidungid));
        if (ds.Tables[0].Rows.Count > 0)
        {
            loadCommbox(drLotrinh, ds.Tables[0], "Id", "Ten");
            loadCommbox(lotrinhcanchuyen, ds.Tables[0], "Id", "Ten");
        }


        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object GetListKHChuaGhiSo()
    {
        UserData rs = new UserData();
        try
        {
            //Initialization
            int lotrinhid = int.Parse(HttpContext.Current.Request.Params["lotrinhid"]);
            int kycuocid = int.Parse(HttpContext.Current.Request.Params["kycuocid"]);
            bool isCompare = bool.Parse(HttpContext.Current.Request.Params["isCompare"]);
            int kysosanh = int.Parse(HttpContext.Current.Request.Params["kysosanh"]);
            string search = HttpContext.Current.Request.Params["search[value]"];
            string draw = HttpContext.Current.Request.Params["draw"];
            string order = HttpContext.Current.Request.Params["order[0][column]"];
            string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);

            List<SoGhi_KhachHang> result = new List<SoGhi_KhachHang>();
            if (isCompare == true)
            {
                result = GetDataKyCuocByLoTrinh(kysosanh, lotrinhid, isCompare);
            }
            else
            {
                result = GetDataKyCuocByLoTrinh(kycuocid, lotrinhid, isCompare);
            }
            int total_record = result.Count;
            // Verification.
            if (!string.IsNullOrEmpty(search) &&
                !string.IsNullOrWhiteSpace(search))
            {
                // Apply search
                result = result.Where(p => p.MaKH.ToString().ToLower().Contains(search.ToLower()) ||
                                       p.MaHopDong.ToLower().Contains(search.ToLower()) ||
                                       p.Ten.ToString().ToLower().Contains(search.ToLower()) ||
                                       p.DiaChi.ToLower().Contains(search.ToLower()) ||
                                       p.LoaiKhachHang.ToLower().Contains(search.ToLower()) ||
                                       p.TienTruocThue.ToString().ToLower().Contains(search.ToLower()) ||
                                       p.TienSauThue.ToString().ToLower().Contains(search.ToLower())).ToList();
            }

            // Sorting.
            result = SortByColumnWithOrder(order, orderDir, result);

            // Filter record count.
            int recFilter = result.Count;

            // Apply pagination.
            result = result.Skip(startRec).Take(pageSize).ToList();

            rs.draw = Convert.ToInt32(draw);
            rs.recordsTotal = total_record;
            rs.recordsFiltered = recFilter;
            rs.data = result;
        }
        catch (Exception)
        {

            throw;
        }
        //object json = new { data = result };
        return rs;
    }

    [WebMethod]
    public static object GetListKHSoGhiChiSo(int lotrinhid, int kycuocid)
    {
        DataTable dt = new DataTable();
        //baidd start sửa b.LoaiKhachHangID thành a.LoaiKhachHangID
        //string sql = "select c.HoTen as TenKhachHang, a.ID, a.HopDongID,b.MaHopDong, b.TrangThaiHopDong, a.ChiSoKyTruoc, a.ChiSoGhiDuoc, a.NgayGhiThucTe, a.TrangThai,b.LoaiKhachHangID from [CSDH_SoGhiChiSo] a join [KH_HopDong] b on a.HopDongID = b.ID join [KH_KhachHang] c on b.KhachHangID = c.ID where a.KyCuocID = " + kycuocid + " and b.LoTrinhID = " + lotrinhid + " order by a.TaoLuc desc";
        string sql = "select a.TangGiam, c.HoTen as TenKhachHang, a.ID, a.HopDongID,b.MaHopDong, b.TrangThaiHopDong, a.ChiSoKyTruoc, a.ChiSoGhiDuoc, a.NgayGhiThucTe, a.TrangThai,a.LoaiKhachHangID from [CSDH_SoGhiChiSo] a join [KH_HopDong] b on a.HopDongID = b.ID join [KH_KhachHang] c on b.KhachHangID = c.ID where a.KyCuocID = " + kycuocid + " and b.LoTrinhID = " + lotrinhid + " order by a.TaoLuc desc";
        //baidd end sửa b.LoaiKhachHangID thành a.LoaiKhachHangID
        dt = SqlDataAccess.getDataFromSql(sql, "").Tables[0];
        List<SoGhi_ChiSo> result = new List<SoGhi_ChiSo>();
        foreach (DataRow item in dt.Rows)
        {
            SoGhi_ChiSo pr = new SoGhi_ChiSo
            {
                ID = int.Parse(item["ID"].ToString()),
                TenKhachHang = item["TenKhachHang"].ToString(),
                HopDongID = item["HopDongID"].ToString(),
                MaHopDong = item["MaHopDong"].ToString(),
                LoaiKhachHangID = int.Parse(item["LoaiKhachHangID"].ToString()),
                ChiSoKyTruoc = int.Parse(item["ChiSoKyTruoc"].ToString()),
                ChiSoGhiDuoc = int.Parse(item["ChiSoGhiDuoc"].ToString()),
                TrangThaiHD = int.Parse(item["TrangThaiHopDong"].ToString()),
                TrangThai = int.Parse(item["TrangThai"].ToString()),
                NgayGhiThucTe = DateTime.Parse(item["NgayGhiThucTe"].ToString()),
                TangGiam = int.Parse(item["TangGiam"].ToString())
            };
            result.Add(pr);
        }
        object json = new { data = result };
        return json;
    }

    [WebMethod]
    public static bool CapNhatGia(string name, int value, string pk)
    {
        string[] spl = pk.Split(',');
        CSDH_ChiSoTemp _obj = entities.CSDH_ChiSoTemp.Find(spl[0], int.Parse(spl[1]));
        if (_obj != null)
        {
            if (!(_obj.LoTrinhID == int.Parse(spl[2]) && _obj.ChuKyID == int.Parse(spl[1])))
            {
                return false;
            }
            else
            {
                if (name == "GiaTruocThue")
                {
                    _obj.GiaTruocThue = value;
                }
                else
                {
                    _obj.GiaSauThue = value;
                }
                entities.SaveChanges();
                return true;
            }
            
        }
        else
        {
            CSDH_ChiSoTemp _new = new CSDH_ChiSoTemp();
            _new.MaHopDong = spl[0];
            if (name == "GiaTruocThue")
            {
                _new.GiaTruocThue = value;
            }
            else
            {
                _new.GiaSauThue = value;
            }
            _new.ChuKyID = int.Parse(spl[1]);
            _new.LoTrinhID = int.Parse(spl[2]);
            entities.CSDH_ChiSoTemp.Add(_new);
            entities.SaveChanges();
            return true;
        }
    }

    [WebMethod]
    public static List<SoGhi_ChiSo> CapNhatSoGhiChiSo(List<string> ListMaHD, int LoTrinhID, int ChuKyID, string NgayGhi, List<string> LoaiKhachHang, List<string> TienTruocThue, List<string> TienSauThue, List<string> MaKH, bool IsCompare, int ChuKyChuyenID)
    {
        int soghichisoID = int.Parse(SqlDataAccess.getDataFromSql("select ID from DM_SoGhiChiSo where LoTrinhID = " + LoTrinhID, "").Tables[0].Rows[0][0].ToString());
        int chuky = ChuKyID;
        if (IsCompare)
        {
            chuky = ChuKyChuyenID;
        }
        List<SoGhi_ChiSo> result = new List<SoGhi_ChiSo>();
        //DataTable dt_export = new DataTable();
        //dt_export.Columns.Add("MaHopDong", typeof(string));
        //dt_export.Columns.Add("TenKhachHang", typeof(string));
        //dt_export.Columns.Add("TienTruocThue", typeof(int));
        //dt_export.Columns.Add("TienSauThue", typeof(int));
        for (int i = 0; i < ListMaHD.Count; i++)
        {
            CSDH_ChiSoTemp _chiso = entities.CSDH_ChiSoTemp.Find(MaKH[i], chuky);
            if (IsCompare)
            {
                bool check = true;
                DataTable dt = new DataTable();
                string sql = "select c.HoTen as TenKhachHang, a.ID, a.HopDongID, b.TrangThaiHopDong, a.ChiSoKyTruoc, a.ChiSoGhiDuoc, a.NgayGhiThucTe, a.TrangThai from [CSDH_SoGhiChiSo] a join [KH_HopDong] b on a.HopDongID = b.ID join [KH_KhachHang] c on b.KhachHangID = c.ID where a.KyCuocID = " + ChuKyID + " and b.LoTrinhID = " + LoTrinhID + " order by a.TaoLuc desc";
                dt = SqlDataAccess.getDataFromSql(sql, "").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (ListMaHD[i] != dt.Rows[j]["HopDongID"].ToString())
                        {
                            continue;
                        }
                        else
                        {
                            SoGhi_ChiSo pr = new SoGhi_ChiSo
                            {
                                HopDongID = ListMaHD[i],
                                TenKhachHang = dt.Rows[j]["TenKhachHang"].ToString(),
                                ChiSoKyTruoc = int.Parse(dt.Rows[j]["ChiSoKyTruoc"].ToString()),
                                ChiSoGhiDuoc = int.Parse(dt.Rows[j]["ChiSoGhiDuoc"].ToString()),
                                TangGiam = 0
                            };
                            result.Add(pr);
                            //dt_export.Rows.Add(new Object[]
                            //{
                            //    ListMaHD[i],
                            //    dt.Rows[j]["TenKhachHang"].ToString(),
                            //    dt.Rows[j]["ChiSoKyTruoc"].ToString(),
                            //    dt.Rows[j]["ChiSoGhiDuoc"].ToString()
                            //});
                            check = false;
                            break;
                        }
                    }
                    if (check == true)
                    {
                        //baidd start
                        int LoaiKhachHangID = 0;
                        if (LoaiKhachHang[i] == "Hộ gia đình ở các xã")
                        {
                            LoaiKhachHangID = 16;
                        }
                        else if (LoaiKhachHang[i] == "Hộ gia đình ở các phường")
                        {
                            LoaiKhachHangID = 17;
                        }
                        else
                        {
                            LoaiKhachHangID = 30;
                        }
                        //baidd end
                        CSDH_SoGhiChiSo _addObj = new CSDH_SoGhiChiSo
                        {
                            HopDongID = int.Parse(ListMaHD[i]),
                            SoGhiChiSoID = soghichisoID,
                            KyCuocID = ChuKyID,
                            //baidd start
                            LoaiKhachHangID = LoaiKhachHangID,
                            TangGiam = 0
                            //baidd end
                        };
                        if (_chiso != null)
                        {
                            if (_chiso.GiaTruocThue.ToString() != "")
                            {
                                _addObj.ChiSoKyTruoc = int.Parse(_chiso.GiaTruocThue.ToString());
                            }
                            else
                            {
                                _addObj.ChiSoKyTruoc = int.Parse(TienTruocThue[i]);
                            }
                            if (_chiso.GiaSauThue.ToString() != "")
                            {
                                _addObj.ChiSoGhiDuoc = int.Parse(_chiso.GiaSauThue.ToString());
                            }
                            else
                            {
                                _addObj.ChiSoGhiDuoc = int.Parse(TienSauThue[i]);
                            }
                            //entities.CSDH_ChiSoTemp.Remove(_chiso);
                        }
                        else
                        {
                            _addObj.ChiSoKyTruoc = int.Parse(TienTruocThue[i]);
                            _addObj.ChiSoGhiDuoc = int.Parse(TienSauThue[i]);
                        }
                        _addObj.TrangThai = 0;
                        if (NgayGhi != "")
                        {
                            _addObj.NgayGhiThucTe = DateTime.ParseExact(NgayGhi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            _addObj.NgayGhiThucTe = DateTime.Now;
                        }
                        _addObj.TaoLuc = DateTime.Now;
                        entities.CSDH_SoGhiChiSo.Add(_addObj);
                    }
                }
            }
            else
            {
                //baidd start
                int LoaiKhachHangID = 0;
                if (LoaiKhachHang[i] == "Hộ gia đình ở các xã")
                {
                    LoaiKhachHangID = 16;
                }
                else if (LoaiKhachHang[i] == "Hộ gia đình ở các phường")
                {
                    LoaiKhachHangID = 17;
                }
                else
                {
                    LoaiKhachHangID = 30;
                }
                //baidd end
                CSDH_SoGhiChiSo _addObj = new CSDH_SoGhiChiSo
                {
                    HopDongID = int.Parse(ListMaHD[i]),
                    SoGhiChiSoID = soghichisoID,
                    KyCuocID = ChuKyID,
                    //baidd start
                    LoaiKhachHangID = LoaiKhachHangID,
                    TangGiam = 0
                    //baidd end
                };
                if (_chiso != null)
                {
                    if (_chiso.GiaTruocThue.ToString() != "")
                    {
                        _addObj.ChiSoKyTruoc = int.Parse(_chiso.GiaTruocThue.ToString());
                    }
                    else
                    {
                        _addObj.ChiSoKyTruoc = int.Parse(TienTruocThue[i]);
                    }
                    if (_chiso.GiaSauThue.ToString() != "")
                    {
                        _addObj.ChiSoGhiDuoc = int.Parse(_chiso.GiaSauThue.ToString());
                    }
                    else
                    {
                        _addObj.ChiSoGhiDuoc = int.Parse(TienSauThue[i]);
                    }
                    //entities.CSDH_ChiSoTemp.Remove(_chiso);
                }
                else
                {
                    _addObj.ChiSoKyTruoc = int.Parse(TienTruocThue[i]);
                    _addObj.ChiSoGhiDuoc = int.Parse(TienSauThue[i]);
                }
                _addObj.TrangThai = 0;
                if (NgayGhi != "")
                {
                    _addObj.NgayGhiThucTe = DateTime.ParseExact(NgayGhi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    _addObj.NgayGhiThucTe = DateTime.Now;
                }
                _addObj.TaoLuc = DateTime.Now;
                entities.CSDH_SoGhiChiSo.Add(_addObj);
            }
            entities.SaveChanges();
        }

        return result;
    }

    [WebMethod]
    public static bool CheckStatusTinhCuoc(int HopDongID, int KyCuocID, int LoTrinhID)
    {
        int soghichisoID = int.Parse(SqlDataAccess.getDataFromSql("select ID from DM_SoGhiChiSo where LoTrinhID = " + LoTrinhID, "").Tables[0].Rows[0][0].ToString());
        int trangthai = int.Parse(SqlDataAccess.getDataFromSql("select TrangThai from CSDH_SoGhiChiSo where HopDongID = " + HopDongID + " and KyCuocID = " + KyCuocID + " and SoGhiChiSoID = " + soghichisoID, "").Tables[0].Rows[0][0].ToString());
        if ((trangthai == 0)||(trangthai == 1))
        {
            return false;
        }
        return true;
    }

    [WebMethod]
    public static SoGhi_ChiSo GetSoGhiChiSo(int HopDongID, int KyCuocID, int LoTrinhID)
    {
        int soghichisoID = int.Parse(SqlDataAccess.getDataFromSql("select ID from DM_SoGhiChiSo where LoTrinhID = " + LoTrinhID, "").Tables[0].Rows[0][0].ToString());
        CSDH_SoGhiChiSo _obj = new CSDH_SoGhiChiSo();
        SoGhi_ChiSo _soghi = new SoGhi_ChiSo();
        _obj = entities.CSDH_SoGhiChiSo.FirstOrDefault(x => x.HopDongID == HopDongID && x.KyCuocID == KyCuocID && x.SoGhiChiSoID == soghichisoID );
        if (_obj != null)
        {
            _soghi.HopDongID = _obj.HopDongID.ToString();
            _soghi.ChiSoGhiDuoc = _obj.ChiSoGhiDuoc;
            _soghi.ChiSoKyTruoc = _obj.ChiSoKyTruoc;
            _soghi.TangGiam = int.Parse(_obj.TangGiam.ToString());
            _soghi.LoaiKhachHangID = int.Parse(_obj.LoaiKhachHangID.ToString());
        }
        return _soghi;
    }

    [WebMethod]
    public static int SetSoGhiChiSoKH(string HopDongID, int KyCuocID, int LoTrinhID, int CSTT, int CSST, bool IsCompare, int ChuKyChuyenID)
    {
        int chuky = KyCuocID;
        if (IsCompare)
        {
            chuky = ChuKyChuyenID;
        }
        CSDH_ChiSoTemp obj = new CSDH_ChiSoTemp();
        obj = entities.CSDH_ChiSoTemp.Find(HopDongID, chuky);
        if (obj != null)
        {
            obj.GiaTruocThue = CSTT;
            obj.GiaSauThue = CSST;
            entities.SaveChanges();
        }
        else
        {
            CSDH_ChiSoTemp _new = new CSDH_ChiSoTemp();
            _new.MaHopDong = HopDongID;
            _new.ChuKyID = chuky;
            _new.LoTrinhID = LoTrinhID;
            _new.GiaTruocThue = CSTT;
            _new.GiaSauThue = CSST;
            entities.CSDH_ChiSoTemp.Add(_new);
            entities.SaveChanges();
        }
        return 0;
    }

    [WebMethod]
    public static int CapNhatGiaSauKhiGhi(int HopDongID, int KyCuocID, int LoTrinhID, int GiaTruocThue, int GiaSauThue,int ID, int TangGiam)
    {
        int soghichisoID = int.Parse(SqlDataAccess.getDataFromSql("select ID from DM_SoGhiChiSo where LoTrinhID = " + LoTrinhID, "").Tables[0].Rows[0][0].ToString());
        CSDH_SoGhiChiSo _obj = entities.CSDH_SoGhiChiSo.FirstOrDefault(x => x.HopDongID == HopDongID && x.KyCuocID == KyCuocID && x.SoGhiChiSoID == soghichisoID);
        //baidd start cap nhat
        KH_HopDong _hd = entities.KH_HopDong.FirstOrDefault(y =>y.ID == HopDongID);
        //baidd End cap nhat
        if (_obj != null)
        {
            if ((_obj.TrangThai == 0)  || (_obj.TrangThai == 1))
            {
                _obj.ChiSoKyTruoc = GiaTruocThue;
                _obj.ChiSoGhiDuoc = GiaSauThue;
                //baidd start cap nhat
                _obj.LoaiKhachHangID = ID;
                _obj.TangGiam = TangGiam;
                _hd.LoaiKhachHangID = ID;
                //baidd End cap nhat
                entities.SaveChanges();
                return 0;
            }
            else
            {
                return 1;
            }
        }
        return 2;
    }

    [WebMethod]
    public static int ChuyenCuoc(int LoTrinhID, int KyCuocCanChuyen, int KyCuocDuocChuyen, string NgayGhi)
    {
        if (KyCuocCanChuyen >= KyCuocDuocChuyen)
        {
            return 2;
        }
        List<SoGhi_KhachHang> lsKHKyCuocCanChuyen = new List<SoGhi_KhachHang>();
        string sql = "select a.LoaiKhachHangID, c.HoTen as TenKhachHang, a.ID, a.HopDongID, a.ChiSoKyTruoc, a.ChiSoGhiDuoc, a.NgayGhiThucTe, a.TrangThai from [CSDH_SoGhiChiSo] a join [KH_HopDong] b on a.HopDongID = b.ID join [KH_KhachHang] c on b.KhachHangID = c.ID where a.KyCuocID = " + KyCuocDuocChuyen + " and b.LoTrinhID = " + LoTrinhID + " order by a.TaoLuc desc";
        DataTable dt = SqlDataAccess.getDataFromSql(sql, "").Tables[0];
        int soghichisoID = int.Parse(SqlDataAccess.getDataFromSql("select ID from DM_SoGhiChiSo where LoTrinhID = " + LoTrinhID, "").Tables[0].Rows[0][0].ToString());
        try
        {
            DataTable dt_1 = new DataTable();
            string sql_1 = "select a.LoaiKhachHangID, c.HoTen as TenKhachHang, a.ID, a.HopDongID, a.ChiSoKyTruoc, a.ChiSoGhiDuoc, a.NgayGhiThucTe, a.TrangThai from [CSDH_SoGhiChiSo] a join [KH_HopDong] b on a.HopDongID = b.ID join [KH_KhachHang] c on b.KhachHangID = c.ID where a.KyCuocID = " + KyCuocCanChuyen + " and b.LoTrinhID = " + LoTrinhID + " order by a.TaoLuc desc";
            dt_1 = SqlDataAccess.getDataFromSql(sql_1, "").Tables[0];
            if (dt_1.Rows.Count > 0)
            {
                for (int i = 0; i < dt_1.Rows.Count; i++)
                {
                    bool check = true;
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt_1.Rows[i]["HopDongID"].ToString() != dt.Rows[j]["HopDongID"].ToString())
                            {
                                continue;
                            }
                            else
                            {
                                check = false;
                                break;
                            }
                        }
                        if (check == true)
                        {
                            CSDH_SoGhiChiSo _addObj = new CSDH_SoGhiChiSo
                            {
                                HopDongID = int.Parse(dt_1.Rows[i]["HopDongID"].ToString()),
                                SoGhiChiSoID = soghichisoID,
                                KyCuocID = KyCuocDuocChuyen,
                                ChiSoKyTruoc = int.Parse(dt_1.Rows[i]["ChiSoKyTruoc"].ToString()),
                                ChiSoGhiDuoc = int.Parse(dt_1.Rows[i]["ChiSoGhiDuoc"].ToString()),
                                LoaiKhachHangID = int.Parse(dt_1.Rows[i]["LoaiKhachHangID"].ToString()),
                                TangGiam = 0,
                                TrangThai = 0
                            };
                            if (NgayGhi != "")
                            {
                                _addObj.NgayGhiThucTe = DateTime.ParseExact(NgayGhi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                _addObj.NgayGhiThucTe = DateTime.Now;
                            }
                            _addObj.TaoLuc = DateTime.Now;
                            entities.CSDH_SoGhiChiSo.Add(_addObj);
                        }
                    }
                    else
                    {
                        CSDH_SoGhiChiSo _addObj = new CSDH_SoGhiChiSo
                        {
                            HopDongID = int.Parse(dt_1.Rows[i]["HopDongID"].ToString()),
                            SoGhiChiSoID = soghichisoID,
                            KyCuocID = KyCuocDuocChuyen,
                            ChiSoKyTruoc = int.Parse(dt_1.Rows[i]["ChiSoKyTruoc"].ToString()),
                            ChiSoGhiDuoc = int.Parse(dt_1.Rows[i]["ChiSoGhiDuoc"].ToString()),
                            LoaiKhachHangID = int.Parse(dt_1.Rows[i]["LoaiKhachHangID"].ToString()),
                            TangGiam = 0,
                            TrangThai = 0
                        };
                        if (NgayGhi != "")
                        {
                            _addObj.NgayGhiThucTe = DateTime.ParseExact(NgayGhi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            _addObj.NgayGhiThucTe = DateTime.Now;
                        }
                        _addObj.TaoLuc = DateTime.Now;
                        entities.CSDH_SoGhiChiSo.Add(_addObj);
                    }
                }
                entities.SaveChanges();
                return 0;
            }
        }
        catch (Exception)
        {
            return 2;
            throw;
        }
        
        return 1;
    }

    [WebMethod]
    public static int XoaDuLieuKy(int KyCuocID, int LoTrinhID)
    {
        int soghichisoID = int.Parse(SqlDataAccess.getDataFromSql("select ID from DM_SoGhiChiSo where LoTrinhID = " + LoTrinhID, "").Tables[0].Rows[0][0].ToString());
        string sql = "delete from [CSDH_SoGhiChiSo] where SoGhiChiSoID = " + soghichisoID + " and KyCuocID = " + KyCuocID;
        return SqlDataAccess.excuteSql(sql);
    }

    [WebMethod]
    public static int RemoveKHFromSoGhi(int HopDongID, int KyCuocID, int LoTrinhID)
    {
        int soghichisoID = int.Parse(SqlDataAccess.getDataFromSql("select ID from DM_SoGhiChiSo where LoTrinhID = " + LoTrinhID, "").Tables[0].Rows[0][0].ToString());
        CSDH_SoGhiChiSo _obj = entities.CSDH_SoGhiChiSo.FirstOrDefault(x => x.HopDongID == HopDongID && x.KyCuocID == KyCuocID && x.SoGhiChiSoID == soghichisoID);
        if (_obj != null)
        {
            if (_obj.TrangThai == 0 || _obj.TrangThai == 1)
            {
                entities.CSDH_SoGhiChiSo.Remove(_obj);
                entities.SaveChanges();
                return 0;
            }
            else
            {
                return 1;
            }
        }
        return 2;
    }

    [WebMethod]
    public static ClassValueCSHD GetValueKHCoChiSo(int LoTrinhID, int ChuKyID)
    {
        ClassValueCSHD _obj = new ClassValueCSHD();
        DataTable dt = new DataTable();
        string sql = "select c.HoTen as TenKhachHang, a.ID, a.HopDongID, b.TrangThaiHopDong, a.ChiSoKyTruoc, a.ChiSoGhiDuoc, a.NgayGhiThucTe, a.TrangThai from [CSDH_SoGhiChiSo] a join [KH_HopDong] b on a.HopDongID = b.ID join [KH_KhachHang] c on b.KhachHangID = c.ID where a.KyCuocID = " + ChuKyID + " and b.LoTrinhID = " + LoTrinhID + " order by a.TaoLuc desc";
        dt = SqlDataAccess.getDataFromSql(sql, "").Tables[0];
        int TongKH = dt.Rows.Count;
        int TongTT = 0;
        int TongST = 0;
        if (TongKH > 0)
        {
            for (int i = 0; i < TongKH; i++)
            {
                TongTT += int.Parse(dt.Rows[i]["ChiSoKyTruoc"].ToString());
                TongST += int.Parse(dt.Rows[i]["ChiSoGhiDuoc"].ToString());
            }
            _obj.TongKH = TongKH;
            _obj.TongTruocThue = TongTT;
            _obj.TongSauThue = TongST;
        }
        return _obj;
    }
    
    [WebMethod]
    public static int CapNhatGiaMacDinh(int LoTrinhID, int ChuKyID)
    {
        DataTable dtSoGhiChiSo = new DataTable();
        string sql_compare = "select a.HopDongID, b.MaHopDong, a.ChiSoKyTruoc, a.ChiSoGhiDuoc from [CSDH_SoGhiChiSo] a join [KH_HopDong] b on a.HopDongID = b.ID where a.KyCuocID = " + ChuKyID + " and b.LotrinhID = " + LoTrinhID + " and b.LoaiKhachHangID <> 17 and b.LoaiKhachHangID <> 16";
        dtSoGhiChiSo = SqlDataAccess.getDataFromSql(sql_compare, "").Tables[0];
        string sql_delete = "delete from [CSDH_ChiSoTemp] where ChuKyID = " + ChuKyID + " and LoTrinhID = " + LoTrinhID;
        int rt = SqlDataAccess.excuteSql(sql_delete);

        if (dtSoGhiChiSo.Rows.Count > 0)
        {
            try
            {
                for (int i = 0; i < dtSoGhiChiSo.Rows.Count; i++)
                {
                    CSDH_ChiSoTemp _obj = new CSDH_ChiSoTemp
                    {
                        MaHopDong = dtSoGhiChiSo.Rows[i]["MaHopDong"].ToString(),
                        LoTrinhID = LoTrinhID,
                        ChuKyID = ChuKyID,
                        GiaTruocThue = int.Parse(dtSoGhiChiSo.Rows[i]["ChiSoKyTruoc"].ToString()),
                        GiaSauThue = int.Parse(dtSoGhiChiSo.Rows[i]["ChiSoGhiDuoc"].ToString())
                    };
                    entities.CSDH_ChiSoTemp.Add(_obj);
                }
                entities.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 2;
                throw;
            }
            
        }
        return 1;
    }

    public class UserData
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<SoGhi_KhachHang> data { get; set; }
    }

    public class ClassValueCSHD
    {
        public int TongKH { get; set; }
        public int TongTruocThue { get; set; }
        public int TongSauThue { get; set; }
    }

    public class SoGhi_KhachHang
    {
        public string MaKH { get; set; }
        public string MaHopDong { get; set; }
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public string LoaiKhachHang { get; set; }
        public double TienTruocThue { get; set; }
        public double TienSauThue { get; set; }
    }

    public class SoGhi_ChiSo
    {
        public int ID { get; set; }
        public string TenKhachHang { get; set; }
        public string MaHopDong { get; set; }
        public string HopDongID { get; set; }
        public int LoaiKhachHangID { get; set; }
        public int ChiSoKyTruoc { get; set; }
        public int ChiSoGhiDuoc { get; set; }
        public int TrangThaiHD { get; set; }
        public int TrangThai { get; set; }  
        public int TangGiam { get; set; }
        public DateTime NgayGhiThucTe { get; set; }
    }

    public static void loadCommbox(DropDownList objCombo, DataTable tblDataTable, string value, string display)
    {
        if (tblDataTable.Rows.Count > 0)
        {
            objCombo.DataSource = tblDataTable;
            objCombo.DataTextField = display;
            objCombo.DataValueField = value;
            objCombo.DataBind();
        }
        else
        {
            objCombo.DataSource = null;
            objCombo.DataBind();
        }
    }

    public static List<SoGhi_KhachHang> GetDataKyCuocByLoTrinh(int kycuocid, int lotrinhid, bool isCompare)
    {
        string year_apply = SqlDataAccess.getDataFromSql("Select KyCuoc from DM_KyCuoc where ID = " + kycuocid, "").Tables[0].Rows[0]["KyCuoc"].ToString();
        string[] spl = year_apply.Split('/');

        DataTable dtSoGhiChiSo = new DataTable();
        string sql_compare = "select a.HopDongID from [CSDH_SoGhiChiSo] a join [KH_HopDong] b on a.HopDongID = b.ID where a.KyCuocID = " + kycuocid + " and b.LotrinhID = " + lotrinhid;
        dtSoGhiChiSo = SqlDataAccess.getDataFromSql(sql_compare, "").Tables[0];
        DataTable dt = new DataTable();
        //string sql = "select hd.ID, hd.SoHopDong,kh.HoTen,kh.DiaChi,kh.MaKhachHang, lkh.Ten from kh_hopdong hd, KH_KhachHang kh, DM_LoaiKhachHang lkh where hd.TrangThaiHopDong=1 and  kh.ID=hd.KhachHangID and hd.LoaiKhachHangID = lkh.ID and hd.lotrinhid=" + lotrinhid + " order by hd.SoHopDong";
        string sql = "select hd.ID, hd.LoaiKhachHangID, hd.MaHopDong, hd.SoHopDong,kh.HoTen,kh.DiaChi,kh.MaKhachHang, lkh.Ten, cst.GiaTruocThue, cst.GiaSauThue from KH_HopDong hd left join KH_KhachHang kh on hd.KhachHangID = kh.ID left join DM_LoaiKhachHang lkh on hd.LoaiKhachHangID = lkh.ID left join CSDH_ChiSoTemp cst on hd.MaHopDong = cst.MaHopDong and cst.ChuKyID = " + kycuocid + " where hd.TrangThaiHopDong = 1 and hd.LoTrinhID = " + lotrinhid + " order by hd.SoHopDong  desc";
        dt = SqlDataAccess.getDataFromSql(sql, "KhachHang_HopDong").Tables[0];
        List<SoGhi_KhachHang> result = new List<SoGhi_KhachHang>();
        foreach (DataRow item in dt.Rows)
        {
            bool check = true;
            if (dtSoGhiChiSo.Rows.Count > 0)
            {
                if (!isCompare)
                {
                    for (int i = 0; i < dtSoGhiChiSo.Rows.Count; i++)
                    {
                        if (int.Parse(item["ID"].ToString()) != int.Parse(dtSoGhiChiSo.Rows[i][0].ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check == true)
                    {
                        int ttt = 1, tst = 1;
                        DataTable dt_1 = new DataTable();
                        string sql_1 = "";
                        if (item["LoaiKhachHangID"].ToString() == "17")
                        {
                            sql_1 = "select GiaTruocThue, GiaSauThue from DM_GiaMacDinh where LoaiKhachHangID = 1 and YearApply = '" + spl[1] + "'";
                        }
                        else if (item["LoaiKhachHangID"].ToString() == "16")
                        {
                            sql_1 = "select GiaTruocThue, GiaSauThue from DM_GiaMacDinh where LoaiKhachHangID = 2 and YearApply = '" + spl[1] + "'";
                        }
                        else
                        {
                            sql_1 = "select GiaTruocThue, GiaSauThue from DM_GiaMacDinh where LoaiKhachHangID = 3 and YearApply = '" + spl[1] + "'";
                        }
                        dt_1 = SqlDataAccess.getDataFromSql(sql_1, "").Tables[0];
                        if (dt_1.Rows.Count > 0)
                        {
                            ttt = int.Parse(dt_1.Rows[0]["GiaTruocThue"].ToString());
                            tst = int.Parse(dt_1.Rows[0]["GiaSauThue"].ToString());
                        }

                        if (item["GiaTruocThue"].ToString() != "")
                        {
                            ttt = int.Parse(item["GiaTruocThue"].ToString());
                        }
                        if (item["GiaSauThue"].ToString() != "")
                        {
                            tst = int.Parse(item["GiaSauThue"].ToString());
                        }
                        SoGhi_KhachHang pr = new SoGhi_KhachHang
                        {
                            Ten = item["HoTen"].ToString(),
                            DiaChi = item["DiaChi"].ToString(),
                            MaHopDong = item["ID"].ToString(),
                            MaKH = item["MaKhachHang"].ToString(),
                            LoaiKhachHang = item["Ten"].ToString(),
                            TienTruocThue = ttt,
                            TienSauThue = tst
                        };
                        result.Add(pr);
                    }
                }
                else
                {
                    for (int i = 0; i < dtSoGhiChiSo.Rows.Count; i++)
                    {
                        if (int.Parse(item["ID"].ToString()) == int.Parse(dtSoGhiChiSo.Rows[i][0].ToString()))
                        {
                            int ttt = 1, tst = 1;
                            DataTable dt_1 = new DataTable();
                            string sql_1 = "";
                            if (item["LoaiKhachHangID"].ToString() == "17")
                            {
                                sql_1 = "select GiaTruocThue, GiaSauThue from DM_GiaMacDinh where LoaiKhachHangID = 1 and YearApply = '" + spl[1] + "'";
                            }
                            else if (item["LoaiKhachHangID"].ToString() == "16")
                            {
                                sql_1 = "select GiaTruocThue, GiaSauThue from DM_GiaMacDinh where LoaiKhachHangID = 2 and YearApply = '" + spl[1] + "'";
                            }
                            else
                            {
                                sql_1 = "select GiaTruocThue, GiaSauThue from DM_GiaMacDinh where LoaiKhachHangID = 3 and YearApply = '" + spl[1] + "'";
                            }
                            dt_1 = SqlDataAccess.getDataFromSql(sql_1, "").Tables[0];
                            if (dt_1.Rows.Count > 0)
                            {
                                ttt = int.Parse(dt_1.Rows[0]["GiaTruocThue"].ToString());
                                tst = int.Parse(dt_1.Rows[0]["GiaSauThue"].ToString());
                            }

                            if (item["GiaTruocThue"].ToString() != "")
                            {
                                ttt = int.Parse(item["GiaTruocThue"].ToString());
                            }
                            if (item["GiaSauThue"].ToString() != "")
                            {
                                tst = int.Parse(item["GiaSauThue"].ToString());
                            }
                            SoGhi_KhachHang pr = new SoGhi_KhachHang
                            {
                                Ten = item["HoTen"].ToString(),
                                DiaChi = item["DiaChi"].ToString(),
                                MaHopDong = item["ID"].ToString(),
                                MaKH = item["MaKhachHang"].ToString(),
                                LoaiKhachHang = item["Ten"].ToString(),
                                TienTruocThue = ttt,
                                TienSauThue = tst
                            };
                            result.Add(pr);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    
                }
            }
            else
            {
                int ttt = 1, tst = 1;
                DataTable dt_1 = new DataTable();
                string sql_1 = "";
                if (item["LoaiKhachHangID"].ToString() == "17")
                {
                    sql_1 = "select GiaTruocThue, GiaSauThue from DM_GiaMacDinh where LoaiKhachHangID = 1 and YearApply = '" + spl[1] + "'";
                }
                else if (item["LoaiKhachHangID"].ToString() == "16")
                {
                    sql_1 = "select GiaTruocThue, GiaSauThue from DM_GiaMacDinh where LoaiKhachHangID = 2 and YearApply = '" + spl[1] + "'";
                }
                else
                {
                    sql_1 = "select GiaTruocThue, GiaSauThue from DM_GiaMacDinh where LoaiKhachHangID = 3 and YearApply = '" + spl[1] + "'";
                }
                dt_1 = SqlDataAccess.getDataFromSql(sql_1, "").Tables[0];
                if (dt_1.Rows.Count > 0)
                {
                    ttt = int.Parse(dt_1.Rows[0]["GiaTruocThue"].ToString());
                    tst = int.Parse(dt_1.Rows[0]["GiaSauThue"].ToString());
                }
                if (item["GiaTruocThue"].ToString() != "")
                {
                    ttt = int.Parse(item["GiaTruocThue"].ToString());
                }
                if (item["GiaSauThue"].ToString() != "")
                {
                    tst = int.Parse(item["GiaSauThue"].ToString());
                }
                SoGhi_KhachHang pr = new SoGhi_KhachHang
                {
                    Ten = item["HoTen"].ToString(),
                    DiaChi = item["DiaChi"].ToString(),
                    MaHopDong = item["ID"].ToString(),
                    MaKH = item["MaKhachHang"].ToString(),
                    LoaiKhachHang = item["Ten"].ToString(),
                    TienTruocThue = ttt,
                    TienSauThue = tst
                };
                result.Add(pr);
            }

        }
        return result;
    }
    
    private static void XuatExcel(DataTable dt)
    {

    }

    private static List<SoGhi_KhachHang> SortByColumnWithOrder(string order, string orderDir, List<SoGhi_KhachHang> data)
    {
        // Initialization.
        List<SoGhi_KhachHang> lst = new List<SoGhi_KhachHang>();

        try
        {
            // Sorting
            switch (order)
            {
                case "0":
                    // Setting.
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.MaKH).ToList()
                                                                                             : data.OrderBy(p => p.MaKH).ToList();
                    break;

                case "1":
                    // Setting.
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.MaHopDong).ToList()
                                                                                             : data.OrderBy(p => p.MaHopDong).ToList();
                    break;

                case "2":
                    // Setting.
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Ten).ToList()
                                                                                             : data.OrderBy(p => p.Ten).ToList();
                    break;

                case "3":
                    // Setting.
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.DiaChi).ToList()
                                                                                             : data.OrderBy(p => p.DiaChi).ToList();
                    break;

                case "4":
                    // Setting.
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.TienTruocThue).ToList()
                                                                                               : data.OrderBy(p => p.TienTruocThue).ToList();
                    break;

                case "5":
                    // Setting.
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.TienSauThue).ToList()
                                                                                             : data.OrderBy(p => p.TienSauThue).ToList();
                    break;

                case "6":
                    // Setting.
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.LoaiKhachHang).ToList()
                                                                                             : data.OrderBy(p => p.LoaiKhachHang).ToList();
                    break;

                default:

                    // Setting.
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.MaKH).ToList()
                                                                                             : data.OrderBy(p => p.MaKH).ToList();
                    break;
            }
        }
        catch (Exception ex)
        {
            // info.
            Console.Write(ex);
        }

        // info.
        return lst;
    }


    private void DumpExcel(DataTable tbl)
    {
        using (ExcelPackage pck = new ExcelPackage())
        {
            //Create the worksheet
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");

            //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells["A1"].LoadFromDataTable(tbl, true);

            //Format the header for column 1-3
            using (ExcelRange rng = ws.Cells["A1:C1"])
            {
                rng.Style.Font.Bold = true;
                //rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                //rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                //rng.Style.Font.Color.SetColor(Color.White);
            }

            //Example how to Format Column 1 as numeric 
            using (ExcelRange col = ws.Cells[2, 1, 2 + tbl.Rows.Count, 1])
            {
                //col.Style.Numberformat.Format = "#,##0.00";
                //col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }

            //Write it back to the client
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;  filename=" + drChuky.SelectedItem.ToString() + "_" + RemoveSign4VietnameseString(drLotrinh.SelectedItem.ToString()) + ".xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
        }
    }
    public static string RemoveSign4VietnameseString(string str)
    {

        //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

        for (int i = 1; i < VietnameseSigns.Length; i++)
        {

            for (int j = 0; j < VietnameseSigns[i].Length; j++)

                str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            str = str.Replace(" ", "");
        }

        return str;

    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ds = new DataSet();
        objGhichiso = new clGhiChiso();
        ds = objGhichiso.GetdsSoghiByKycuoc_Lotrinh(drChuky.SelectedValue.ToString(), drLotrinh.SelectedValue.ToString());
        if (ds != null)
        {
            //ExportTableData(ds.Tables[0]);
            DumpExcel(ds.Tables[0]);
        }
    }
    private static readonly string[] VietnameseSigns = new string[]

    {

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵ",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễ",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"

    };

    //baidd start doi loai khach hang
    [WebMethod]

    public static List<DM_LoaiKhachHang> GetCbxLoaiKhachHang()
    {
        List<DM_LoaiKhachHang> loaikhachhang_id = new List<DM_LoaiKhachHang>();
        DataTable lkh = new DataTable();
        string sql = "SELECT  ID,Ten FROM [dbo].[DM_LoaiKhachHang]";
        lkh = SqlDataAccess.getDataFromSql(sql, "").Tables[0];
        if (lkh.Rows.Count > 0)
        {

            for (int i = 0; i < lkh.Rows.Count; i++)
            {
                loaikhachhang_id.Add(new DM_LoaiKhachHang
                {
                    ID = int.Parse(lkh.Rows[i]["ID"].ToString()),
                    Ten = lkh.Rows[i]["Ten"].ToString()
                });
            }
        }
        return loaikhachhang_id;
    }
    //baidd End doi loai khach hang

    //baidd start cap nhat gia tien loai khach hang khac
    [WebMethod]
    public static int capnhat_tanggiam(int kycuoc_id_truoc, int kycuoc_id_sau, int lotrinh_id)//baidd cap nhat TangGiam=1
    {
        string sql = "update[dbo].[CSDH_SoGhiChiSo]  set TangGiam = 1 where kycuocid = " + kycuoc_id_sau + " and id in(select sg.ID from[dbo].[CSDH_SoGhiChiSo] sg, [dbo].[KH_HopDong] hd where sg.HopDongID=hd.id and sg.KyCuocID=" + kycuoc_id_sau + " and hd.LoTrinhID=" + lotrinh_id + " and sg.HopDongID not in (select sg.HopDongID from [dbo].[CSDH_SoGhiChiSo] sg, [dbo].[KH_HopDong] hd where sg.HopDongID= hd.id and sg.KyCuocID= " + kycuoc_id_truoc + " and hd.LoTrinhID= " + lotrinh_id + "))";
        return SqlDataAccess.excuteSql(sql);
    }
    //baidd end cap nhat gia tien loai khach hang khac

    //baidd start ghi chi so nhieu ky
    [WebMethod]

    public static List<DM_KyCuoc> GetCbxNhieuKy()
    {
        List<DM_KyCuoc> lsKyCuoc = new List<DM_KyCuoc>();
        DataTable Kicuocchonnhieu = new DataTable();
        string sql_IDKYCUOC = "SELECT  ID,KyCuoc FROM [dbo].[DM_KyCuoc]";
        Kicuocchonnhieu = SqlDataAccess.getDataFromSql(sql_IDKYCUOC, "").Tables[0];
        if (Kicuocchonnhieu.Rows.Count > 0)
        {

            for (int i = 0; i < Kicuocchonnhieu.Rows.Count; i++)
            {
                lsKyCuoc.Add(new DM_KyCuoc
                {
                    ID = int.Parse(Kicuocchonnhieu.Rows[i]["ID"].ToString()),
                    KyCuoc = Kicuocchonnhieu.Rows[i]["KyCuoc"].ToString()
                });
            }
        }
        return lsKyCuoc;
    }
    [WebMethod]
    public static List<SoGhi_ChiSo> CapNhatSoGhiChiSo_nhieuky(List<string> ListMaHD, int LoTrinhID, List<int> lstChuKyID, string NgayGhi, List<string> LoaiKhachHang, List<string> TienTruocThue, List<string> TienSauThue, List<string> MaKH)
    {
        int soghichisoID = int.Parse(SqlDataAccess.getDataFromSql("select ID from DM_SoGhiChiSo where LoTrinhID = " + LoTrinhID, "").Tables[0].Rows[0][0].ToString());
        List<SoGhi_ChiSo> result = new List<SoGhi_ChiSo>();
        for (int k = 0; k < lstChuKyID.Count; k++)
        {
            int chukyID = int.Parse(lstChuKyID[k].ToString());
            for (int i = 0; i < ListMaHD.Count; i++)
            {
                CSDH_ChiSoTemp _chiso = entities.CSDH_ChiSoTemp.Find(MaKH[i], chukyID);
                bool check = true;
                DataTable dt = new DataTable();
                string sql = "select c.HoTen as TenKhachHang, a.ID, a.HopDongID, b.TrangThaiHopDong, a.ChiSoKyTruoc, a.ChiSoGhiDuoc, a.NgayGhiThucTe, a.TrangThai from [CSDH_SoGhiChiSo] a join [KH_HopDong] b on a.HopDongID = b.ID join [KH_KhachHang] c on b.KhachHangID = c.ID where a.KyCuocID = " + chukyID + " and b.LoTrinhID = " + LoTrinhID + " order by a.TaoLuc desc";
                dt = SqlDataAccess.getDataFromSql(sql, "").Tables[0];

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (ListMaHD[i] != dt.Rows[j]["HopDongID"].ToString())
                    {
                        continue;
                    }
                    else
                    {
                        SoGhi_ChiSo pr = new SoGhi_ChiSo
                        {
                            HopDongID = ListMaHD[i],
                            TenKhachHang = dt.Rows[j]["TenKhachHang"].ToString(),
                            ChiSoKyTruoc = int.Parse(dt.Rows[j]["ChiSoKyTruoc"].ToString()),
                            ChiSoGhiDuoc = int.Parse(dt.Rows[j]["ChiSoGhiDuoc"].ToString()),
                            TangGiam =0
                        };
                        result.Add(pr);
                        check = false;
                        break;
                    }
                }
                if (check == true)
                {
                    int LoaiKhachHangID = 0;
                   
                    
                    if (LoaiKhachHang[i] == "Hộ gia đình ở các xã")
                    {
                        LoaiKhachHangID = 16;
                    }
                    else if (LoaiKhachHang[i] == "Hộ gia đình ở các phường")
                    {
                        LoaiKhachHangID = 17;
                    }
                    else
                    {
                        LoaiKhachHangID = 30;
                    }
                    CSDH_SoGhiChiSo _addObj = new CSDH_SoGhiChiSo
                    {
                        HopDongID = int.Parse(ListMaHD[i]),
                        SoGhiChiSoID = soghichisoID,
                        KyCuocID = chukyID,
                        LoaiKhachHangID = LoaiKhachHangID,
                        TangGiam = 0
                    };
                    if (_chiso != null)
                    {
                        if (_chiso.GiaTruocThue.ToString() != "")
                        {
                            _addObj.ChiSoKyTruoc = int.Parse(_chiso.GiaTruocThue.ToString());
                        }
                        else
                        {
                            _addObj.ChiSoKyTruoc = int.Parse(TienTruocThue[i]);
                        }
                        if (_chiso.GiaSauThue.ToString() != "")
                        {
                            _addObj.ChiSoGhiDuoc = int.Parse(_chiso.GiaSauThue.ToString());
                        }
                        else
                        {
                            _addObj.ChiSoGhiDuoc = int.Parse(TienSauThue[i]);
                        }
                    }
                    else
                    {
                        _addObj.ChiSoKyTruoc = int.Parse(TienTruocThue[i]);
                        _addObj.ChiSoGhiDuoc = int.Parse(TienSauThue[i]);
                    }
                    _addObj.TrangThai = 0;
                    if (NgayGhi != "")
                    {
                        _addObj.NgayGhiThucTe = DateTime.ParseExact(NgayGhi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        _addObj.NgayGhiThucTe = DateTime.Now;
                    }
                    _addObj.TaoLuc = DateTime.Now;
                    entities.CSDH_SoGhiChiSo.Add(_addObj);
                    entities.SaveChanges();
                }
            }
        }
        return result;
    }
    //baidd start ghi chi so nhieu ky
    //baidd xoa cuoc , xoa so ghi start
    [WebMethod]//xoa cuoc
    public static int Xoa_Cuoc(int kycuoc, int lotrinh)
    {
        string xoacuoc = "delete from CSDH_TinhCuoc where SoGhiChiSoID in(select sg.id from[dbo].[CSDH_SoGhiChiSo] sg ,[dbo].[KH_HopDong] hd where sg.HopDongID=hd.id and sg.KyCuocID=" + kycuoc + " and hd.LoTrinhID="+ lotrinh + ")";
        return SqlDataAccess.excuteSql(xoacuoc);
    }


    [WebMethod]//xoa so ghi
    public static int Xoa_So_ghi(int kycuoc, int lotrinh)
    {
        string xoasoghi = "delete from [dbo].[CSDH_SoGhiChiSo] where ID in (select sg.id from[dbo].[CSDH_SoGhiChiSo] sg ,[dbo].[KH_HopDong] hd where sg.HopDongID=hd.id and sg.KyCuocID=" + kycuoc + " and hd.LoTrinhID="+ lotrinh + ")";
        return SqlDataAccess.excuteSql(xoasoghi);
    }
    //baidd xoa cuoc, xoa so ghi end


}