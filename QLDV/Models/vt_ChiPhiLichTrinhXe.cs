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
    
    public partial class vt_ChiPhiLichTrinhXe
    {
        public int id { get; set; }
        public int idLichTrinhXe { get; set; }
        public Nullable<int> loaiChiPhi { get; set; }
        public string tenChiPhi { get; set; }
        public Nullable<double> giaTien { get; set; }
        public string daXoa { get; set; }
    }
}
