using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.GiaoUocService
{
    public class GUTDService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        public GiaoUocModel getGiaoUocThiDua()
        {
            GiaoUocModel rs = new GiaoUocModel();
            var giaouoc = _entities.GiaoUocThiDuas.Find(1);
            if (giaouoc != null)
            {
                rs.Id = giaouoc.Id;
                rs.IdToChuc = giaouoc.IdToChuc;
                rs.Nam = giaouoc.Nam;
                rs.TieuChiGiaoUocs = new List<TieuChiGiaoUocModel>();
                List<TieuChiGiaoUocModel> tieuChis = new List<TieuChiGiaoUocModel>();
                var tieuchigiaouoc = _entities.TieuChiGiaoUocs.Where(x => x.IdGiaoUoc == giaouoc.Id).ToList();
                if (tieuchigiaouoc != null)
                {
                    for (int i = 0; i < tieuchigiaouoc.Count; i++)
                    {
                        TieuChiGiaoUocModel _newtc = new TieuChiGiaoUocModel
                        {
                            Id = tieuchigiaouoc[i].Id,
                            NoiDung = tieuchigiaouoc[i].NoiDung,
                            IdGiaoUoc = tieuchigiaouoc[i].IdGiaoUoc,
                            OpenChiTiet = false,
                            NoiDungTieuChis = new List<NoiDungTieuChiModel>()
                        };
                        List<NoiDungTieuChiModel> ndtc = new List<NoiDungTieuChiModel>();
                        int idtieuchi = tieuchigiaouoc[i].Id;
                        var noidung = _entities.NoiDungTieuChiGiaoUocs.Where(x => x.TieuChiId == idtieuchi).ToList();
                        if (noidung != null)
                        {
                            for (int j = 0; j < noidung.Count; j++)
                            {
                                NoiDungTieuChiModel _newnd = new NoiDungTieuChiModel
                                {
                                    Id = noidung[j].Id,
                                    IdTieuChi = noidung[j].TieuChiId,
                                    NoiDung = noidung[j].NoiDung,
                                    BaoCao = noidung[j].BaoCao,
                                    DiemBC = noidung[j].DiemBC,
                                    DiemChuan = noidung[j].DiemChuan,
                                    DiemDK = noidung[j].DiemDK,
                                    DiemNX = noidung[j].DiemNX,
                                    SLTK = noidung[j].SLTK,
                                };
                                _newtc.NoiDungTieuChis.Add(_newnd);
                            }
                        }
                        rs.TieuChiGiaoUocs.Add(_newtc);
                    }
                }
            }
            return rs;
        }
    }
}