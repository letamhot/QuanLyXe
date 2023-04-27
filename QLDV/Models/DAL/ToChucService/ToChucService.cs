using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.ToChucService
{
    public class ToChucService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        public List<ToChucAssignModel> getListDoanVienToChuc(int iddonvi, int idtochuc)
        {
            List<ToChucAssignModel> rs = new List<ToChucAssignModel>();
            List<NguoiDung> lsNguoiDung = _entities.NguoiDungs.Where(x => x.DonViId == iddonvi).ToList();
            List<NguoiDung_ToChuc> lsNguoiDung_tc = new List<NguoiDung_ToChuc>();
            lsNguoiDung_tc = _entities.NguoiDung_ToChuc.Where(x => x.ToChucId == idtochuc).ToList();
            if (lsNguoiDung.Count > 0)
            {
                for (int i = 0; i < lsNguoiDung.Count; i++)
                {
                    bool isCheck = false;
                    int chucvuid = 0;
                    string gioitinh = "";
                    if (lsNguoiDung_tc.Count > 0)
                    {
                        for (int j = 0; j < lsNguoiDung_tc.Count; j++)
                        {
                            if (lsNguoiDung_tc[j].NguoiDungId == lsNguoiDung[i].NguoiDungId)
                            {
                                isCheck = true;
                                chucvuid = int.Parse(lsNguoiDung_tc[j].ChucVuId.ToString());
                                break;
                            }
                        }
                    }
                    if (lsNguoiDung[i].GioiTinh == true)
                        gioitinh = "Nam";
                    else
                        gioitinh = "Nữ";
                    rs.Add(new ToChucAssignModel
                    {
                        NguoiDungId = lsNguoiDung[i].NguoiDungId,
                        TenNguoiDung = lsNguoiDung[i].HoTen,
                        NgaySinh = DateTime.Parse(lsNguoiDung[i].NgaySinh.ToString()),
                        GioiTinh = gioitinh,
                        ChucVuId = chucvuid,
                        Selected = isCheck
                    });
                }
            }
            return rs;
        }
    }
}