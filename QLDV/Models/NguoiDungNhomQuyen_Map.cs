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
    
    public partial class NguoiDungNhomQuyen_Map
    {
        public int Id { get; set; }
        public int NguoiDungId { get; set; }
        public int NhomQuyenId { get; set; }
        public string MoTa { get; set; }
    
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual NhomQuyen NhomQuyen { get; set; }
    }
}
