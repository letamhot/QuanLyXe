using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.LichTrinhService
{
    public class LichTrinhModel
    {
        public int id { get; set; }
        public string maChuyen { get; set; }
        public int idLaiXe { get; set; }
        public string tenLaiXe { get; set; }
        public int idKhachHang { get; set; }
        public string tenKhachHang { get; set; }
        public string thoiGianLapPhieu { get; set; }
        public string noiDungLamViec { get; set; }
        public string noiDi { get; set; }
        public string noiDen { get; set; }
        public Nullable<double> giaChuaThue { get; set; }
        public Nullable<double> thueVat { get; set; }
        public Nullable<double> tongThanhToan { get; set; }
        public Nullable<int> tinhTrangThanhToan { get; set; }
        public Nullable<bool> checkPhieu { get; set; }
        public Nullable<int> daXoa { get; set; }
    }
}