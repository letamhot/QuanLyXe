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
    
    public partial class NoiDungTieuChiGiaoUoc
    {
        public int Id { get; set; }
        public int TieuChiId { get; set; }
        public string BaoCao { get; set; }
        public int DiemBC { get; set; }
        public int DiemChuan { get; set; }
        public int DiemDK { get; set; }
        public int DiemNX { get; set; }
        public string NoiDung { get; set; }
        public int SLTK { get; set; }
    
        public virtual TieuChiGiaoUoc TieuChiGiaoUoc { get; set; }
    }
}