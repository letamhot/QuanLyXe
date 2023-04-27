using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.ChiPhiSuaXe
{
    public class ChiPhiSuaXeModel
    {
        public int id { get; set; }
        public string ngayNhap { get; set; }
        public string vatTu { get; set; }
        public int soLuong { get; set; }
        public string donViTinh { get; set; }
        public float donGia { get; set; }
        public float thanhTien { get; set; }
        public string ghiChu { get; set; }
    }
}