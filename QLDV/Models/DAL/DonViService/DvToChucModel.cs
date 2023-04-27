using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.DonViService
{
    public class DvToChucModel
    {
        public int Id { get; set; }
        public string LoaiToChuc { get; set; }
        public string CapToChuc { get; set; }
        public string TenToChuc { get; set; }
        public DateTime NgayThanhLap { get; set; }
    }
}