using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.DichVuKinhDoanhKhac
{
    public class DichVuKinhDoanhKhacModel
    {
        public int id { get; set; }
        public int loaiDichVuKhac { get; set; }
        public string ngayHoanThanh { get; set; }
        public string tenCongTrinh { get; set; }
        public string doiTruong { get; set; }
        public Nullable<double> doanhThu { get; set; }
        public Nullable<double> chiPhi { get; set; }
        public Nullable<double> loiNhuan { get; set; }
        public Nullable<double> thanhToanLan1 { get; set; }
        public Nullable<double> thanhToanLan2 { get; set; }
        public Nullable<double> thanhToanLan3 { get; set; }
        public Nullable<double> no { get; set; }
        public string ghiChu { get; set; }
    }
}