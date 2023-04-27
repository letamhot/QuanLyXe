using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.CapToChucService
{
    public class ToChucSelectedModel
    {
        public int ID { get; set; }
        public string TenCapToChuc { get; set; }
        public bool Selected { get; set; }
    }
}