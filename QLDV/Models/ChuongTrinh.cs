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
    
    public partial class ChuongTrinh
    {
        public int ChuongTrinhId { get; set; }
        public int LoaiChuongTrinhId { get; set; }
        public string TenChuongTrinh { get; set; }
        public string MoTa { get; set; }
        public Nullable<System.DateTime> TuNgay { get; set; }
        public Nullable<System.DateTime> DenNgay { get; set; }
        public Nullable<int> SoVanBan { get; set; }
        public Nullable<int> Diem { get; set; }
        public string FileDinhKem { get; set; }
        public Nullable<short> TrangThai { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
    
        public virtual LoaiChuongTrinh LoaiChuongTrinh { get; set; }
    }
}
