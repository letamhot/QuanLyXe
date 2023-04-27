using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.ToChucService
{
    public class ToChucAssignModel
    {
        public int NguoiDungId { get; set; }
        public string TenNguoiDung { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public int ChucVuId { get; set; }
        public bool Selected { get; set; }
    }
}