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
    
    public partial class BienDong_QuyTrinh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BienDong_QuyTrinh()
        {
            this.BienDong_QuyTrinh_ChiTiet = new HashSet<BienDong_QuyTrinh_ChiTiet>();
        }
    
        public int QuyTrinhId { get; set; }
        public int BienDongId { get; set; }
        public string TenQuyTrinh { get; set; }
        public int LoaiHinhToChucId { get; set; }
        public int CapToChucId { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
    
        public virtual BienDong BienDong { get; set; }
        public virtual CapToChuc CapToChuc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BienDong_QuyTrinh_ChiTiet> BienDong_QuyTrinh_ChiTiet { get; set; }
        public virtual LoaiHinhToChuc LoaiHinhToChuc { get; set; }
    }
}
