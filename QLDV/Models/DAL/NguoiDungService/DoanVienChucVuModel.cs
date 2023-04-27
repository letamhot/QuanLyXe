using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.NguoiDungService
{
    public class DoanVienChucVuModel
    {
        public int NguoiDungId { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public List<chucvu_tochuc> ChucVu { get; set; }
        public string ToChuc { get; set; }
    }
    public class chucvu_tochuc
    {
        public string tencv { get; set; }
        public string tentochuc { get; set; }
    }

    public class DoanVienKiemNhiemModel
    {
        public int KiemNhiemId { get; set; }
        public string ToChuc { get; set; }
        public string ChucVu { get; set; }
        public DateTime NgayApDung { get; set; }
    }

    public class DoanVienBienDong
    {
        public int DoanVienId { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public bool Selected { get; set; }
    }
}