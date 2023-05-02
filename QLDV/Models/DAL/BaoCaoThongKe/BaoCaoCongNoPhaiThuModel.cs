using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.BaoCaoThongKe
{
    public class BaoCaoCongNoPhaiThuModel
    {
        public int stt { get; set; }
        public string maCongNo { get; set; }
        public string maChuyen { get; set; }
        public string laiXe { get; set; }
        public string maXe { get; set; }
        public string maKhachHang { get; set; }
        public string thoiGianLapPhieu { get; set; }
        public string noiDungLamViec { get; set; }
        public string noiDen { get; set; }
        public string noiDi { get; set; }
        public double giaChuaThue { get; set; }
        public string vat { get; set; }
        public double tongThanhToan { get; set; }
    }
}