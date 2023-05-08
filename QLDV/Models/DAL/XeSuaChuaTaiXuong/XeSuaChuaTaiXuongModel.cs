using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.XeSuaChuaTaiXuong
{
    public class XeSuaChuaTaiXuongModel
    {
        public int id { get; set; }
        public string ngayThang { get; set; }
        public string maXe { get; set; }
        public string maKhachHang { get; set; }
        public int idXeVanTai { get; set; }
        public int idKhachHang { get; set; }
        public string noiDungSuaChua { get; set; }
        public int soLuong { get; set; }
        public string donViTinh { get; set; }
        public Nullable<double> donGia { get; set; }
        public Nullable<double> thanhTienChuaThue { get; set; }
        public Nullable<double> thueVat { get; set; }
        public Nullable<double> tongThanhToan { get; set; }
        public Nullable<double> no { get; set; }
        public Nullable<double> canTru { get; set; }
        public string ghiChu { get; set; }
    }
}