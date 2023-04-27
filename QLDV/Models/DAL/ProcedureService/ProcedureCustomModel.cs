using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.ProcedureService
{
    public class ProcedureCustomModel
    {
        public int QuyTrinhId { get; set; }
        public string TenQuyTrinh { get; set; }
        public string CapToChuc { get; set; }
    }

    public class QuyTrinhChiTiet
    {
        public int QuyTrinhChiTietId { get; set; }
        public int CapToChucId { get; set; }
        public string CapToChuc { get; set; }
        public string Email { get; set; }
        public string Sms { get; set; }
    }
}