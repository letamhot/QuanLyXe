using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.GiaoUocService
{
    public class GiaoUocModel
    {
        public int Id { get; set; }
        public int IdToChuc { get; set; }
        public string Nam { get; set; }
        public List<TieuChiGiaoUocModel> TieuChiGiaoUocs { get; set; }
    }
    public class TieuChiGiaoUocModel
    {
        public int Id { get; set; }
        public int IdGiaoUoc { get; set; }
        public string NoiDung { get; set; }
        public bool OpenChiTiet { get; set; }
        public List<NoiDungTieuChiModel> NoiDungTieuChis { get; set; }
    }
    public class NoiDungTieuChiModel
    {
        public int Id { get; set; }
        public int IdTieuChi { get; set; }
        public string BaoCao { get; set; }
        public int DiemBC { get; set; }
        public int DiemChuan { get; set; }
        public int DiemDK { get; set; }
        public int DiemNX { get; set; }
        public string NoiDung { get; set; }
        public int SLTK { get; set; }
    }
}