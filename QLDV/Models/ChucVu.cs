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
    
    public partial class ChucVu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChucVu()
        {
            this.DoanVien_KiemNhiem = new HashSet<DoanVien_KiemNhiem>();
        }
    
        public int ChucVuId { get; set; }
        public string TenChucVu { get; set; }
        public Nullable<int> CapChucVu { get; set; }
        public Nullable<bool> LanhDao { get; set; }
        public Nullable<bool> GuiEmail { get; set; }
        public Nullable<bool> GuiSMS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoanVien_KiemNhiem> DoanVien_KiemNhiem { get; set; }
    }
}
