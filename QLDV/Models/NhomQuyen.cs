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
    
    public partial class NhomQuyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhomQuyen()
        {
            this.NguoiDungNhomQuyen_Map = new HashSet<NguoiDungNhomQuyen_Map>();
            this.NhomQuyenCapTc_Map = new HashSet<NhomQuyenCapTc_Map>();
            this.NhomQuyenMenu_Map = new HashSet<NhomQuyenMenu_Map>();
        }
    
        public int NhomQuyenId { get; set; }
        public string TenNhomQuyen { get; set; }
        public string MoTa { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDungNhomQuyen_Map> NguoiDungNhomQuyen_Map { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhomQuyenCapTc_Map> NhomQuyenCapTc_Map { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhomQuyenMenu_Map> NhomQuyenMenu_Map { get; set; }
    }
}
