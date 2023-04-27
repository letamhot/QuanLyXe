using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.ChiPhiLichTrinhXe
{
    public class ChiPhiLTXModel
    {
        public int id { get; set; }
        public int idLichTrinhXe { get; set; }
        public string maChuyen { get; set; }
        public Nullable<int> loaiChiPhi { get; set; }
        public string tenChiPhi { get; set; }
        public float giaTien { get; set; }
        public string daXoa { get; set; }
    }
}