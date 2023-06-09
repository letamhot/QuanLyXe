//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLDV.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            this.BienDong_DoanVien_Map = new HashSet<BienDong_DoanVien_Map>();
            this.DoanVien_KiemNhiem = new HashSet<DoanVien_KiemNhiem>();
            this.NguoiDung_ToChuc = new HashSet<NguoiDung_ToChuc>();
            this.NguoiDungBangCap_Map = new HashSet<NguoiDungBangCap_Map>();
            this.NguoiDungNhomQuyen_Map = new HashSet<NguoiDungNhomQuyen_Map>();
        }
    
        public int NguoiDungId { get; set; }
        public int DonViId { get; set; }
        public Nullable<int> ToChucId { get; set; }
        public string MaNguoiDung { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string AnhDaiDien { get; set; }
        public string TenKhac { get; set; }
        public Nullable<bool> GioiTinh { get; set; }
        public string CMND { get; set; }
        public Nullable<System.DateTime> NgayCap { get; set; }
        public string NoiCap { get; set; }
        public Nullable<int> DanToc { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public Nullable<int> NoiSinhTinh { get; set; }
        public Nullable<int> NoiSinhHuyen { get; set; }
        public Nullable<int> TrinhDoPhoThong { get; set; }
        public Nullable<int> QueQuanTinh { get; set; }
        public Nullable<int> QueQuanHuyen { get; set; }
        public Nullable<int> QueQuanXP { get; set; }
        public Nullable<int> ThuongTruTinh { get; set; }
        public Nullable<int> ThuongTruHuyen { get; set; }
        public Nullable<int> ThuongTruXP { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public Nullable<int> TonGiao { get; set; }
        public Nullable<int> LinhVucChuyenMon { get; set; }
        public Nullable<int> HocVi { get; set; }
        public Nullable<int> HocHam { get; set; }
        public Nullable<int> LyLuanChinhTri { get; set; }
        public Nullable<int> NgoaiNgu { get; set; }
        public Nullable<System.DateTime> NgayKetNapDoan { get; set; }
        public string NoiKetNapDoan { get; set; }
        public Nullable<System.DateTime> NgayKetNapDang { get; set; }
        public string NoiKetNapDang { get; set; }
        public string NoiSinhHoat { get; set; }
        public string SoSoDoan { get; set; }
        public string SoTheDoan { get; set; }
        public Nullable<bool> HasAccount { get; set; }
        public Nullable<bool> KiemNhiem { get; set; }
        public Nullable<int> ChucVu { get; set; }
        public Nullable<bool> ChuHo { get; set; }
        public Nullable<bool> HoNgheo { get; set; }
        public Nullable<bool> CoViecLam { get; set; }
        public Nullable<bool> HoiVien { get; set; }
        public Nullable<bool> DoanVienDanhDu { get; set; }
        public Nullable<bool> DangSinhHoat { get; set; }
        public Nullable<int> QuyenKhaiThacToChuc { get; set; }
        public Nullable<int> QuyenKhaiThacDonVi { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BienDong_DoanVien_Map> BienDong_DoanVien_Map { get; set; }
        public virtual DanToc DanToc1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoanVien_KiemNhiem> DoanVien_KiemNhiem { get; set; }
        public virtual DonVi DonVi { get; set; }
        public virtual HocHam HocHam1 { get; set; }
        public virtual HocVi HocVi1 { get; set; }
        public virtual LyLuanChinhTri LyLuanChinhTri1 { get; set; }
        public virtual NgoaiNgu NgoaiNgu1 { get; set; }
        public virtual PhuongXa PhuongXa { get; set; }
        public virtual QuanHuyen QuanHuyen { get; set; }
        public virtual QuanHuyen QuanHuyen1 { get; set; }
        public virtual TinhThanh TinhThanh { get; set; }
        public virtual TinhThanh TinhThanh1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDung_ToChuc> NguoiDung_ToChuc { get; set; }
        public virtual TonGiao TonGiao1 { get; set; }
        public virtual TrinhDoPhoThong TrinhDoPhoThong1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDungBangCap_Map> NguoiDungBangCap_Map { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDungNhomQuyen_Map> NguoiDungNhomQuyen_Map { get; set; }
    }
}
