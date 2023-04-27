using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.DangKiemGiayToXe
{
    public class DangKiemGiayToXeModel
    {
        public int id { get; set; }
        public int idXeVanTai { get; set; }
        public string maSoXe { get; set; }
        public string kiemDinhSo { get; set; }
        public string ngayKiemDinh { get; set; }
        public string loaiKiemDinh { get; set; }
        public string noiDung { get; set; }
        public string ngayHetHan { get; set; }
        public string ghiChu { get; set; }
        public string daXoa { get; set; }
    }
}