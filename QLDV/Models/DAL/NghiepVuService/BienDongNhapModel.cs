using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.NghiepVuService
{
    public class BienDongNhapModel
    {
        public int BienDongDoanVienId { get; set; }
        public string TenBienDong { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayNhap { get; set; }
        public string DanhSachDoanVien { get; set; }
        public int TrangThaiBienDong { get; set; }
    }

    public class BienDongXuLyModel
    {
        public int BienDongDoanVienId { get; set; }
        public string TenBienDong { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayNhap { get; set; }
        public string NguoiYeuCau { get; set; }
        public string ToChucYeuCau { get; set; }
        public int TrangThaiBienDong { get; set; }
    }
    public class BienDongChiTietModel
    {
        public string ToChucGui { get; set; }
        public string ToChucNhan { get; set; }
        public string NguoiXuLy { get; set; }
        public DateTime NgayThucHien { get; set; }
        public string NoiDung { get; set; }
        public string FileDinhKem { get; set; }
    }
}