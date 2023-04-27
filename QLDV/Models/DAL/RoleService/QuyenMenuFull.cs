using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.RoleService
{
    public class QuyenMenuFull
    {
        public int QuyenMenuId { get; set; }
        public string TenQuyen { get; set; }
        public int QuyenCha { get; set; }
        public string Class { get; set; }
        public string Styles { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<QuyenMenuFull> Children { get; set; }
    }
}