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
    
    public partial class CapToChuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CapToChuc()
        {
            this.BienDong_QuyTrinh = new HashSet<BienDong_QuyTrinh>();
            this.CapToChucLoaiHinh_Map = new HashSet<CapToChucLoaiHinh_Map>();
            this.DonViToChuc_Map = new HashSet<DonViToChuc_Map>();
            this.NhomQuyenCapTc_Map = new HashSet<NhomQuyenCapTc_Map>();
        }
    
        public int CapToChucId { get; set; }
        public string TenCapToChuc { get; set; }
        public Nullable<int> ThuTu { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BienDong_QuyTrinh> BienDong_QuyTrinh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CapToChucLoaiHinh_Map> CapToChucLoaiHinh_Map { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonViToChuc_Map> DonViToChuc_Map { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhomQuyenCapTc_Map> NhomQuyenCapTc_Map { get; set; }
    }
}