using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.ChiPhiKhac
{
    public class ChiPhiKhacModel
    {
        public int id { get; set; }
        public string tenChiPhi { get; set; }
        public string ngayThanhToan { get; set; }
        public string noiDung { get; set; }
        public int soLuong { get; set; }
        public string donViTinh { get; set; }
        public float donGia { get; set; }
        public float thanhTien { get; set; }
        public string ghiChu { get; set; }
    }
}