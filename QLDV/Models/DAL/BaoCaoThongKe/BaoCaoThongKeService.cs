using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Models.DAL.BaoCaoThongKe
{
    public class BaoCaoThongKeService
    {
        private QuanLyDoanVienEntities _entities = new QuanLyDoanVienEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public List<BaoCaoThongKeChiPhiModel> getBaoCaoThongChiPhiXe(DateTime from, DateTime to)
        {

            List<BaoCaoThongKeChiPhiModel> result = new List<BaoCaoThongKeChiPhiModel>();
            var dsLichTrinhXe = _entities.vt_LichTrinhXe.Where(y => y.thoiGianLapPhieu >= from && y.thoiGianLapPhieu <= to && y.daXoa == 0).ToList();
            int stt = 0;
            for (int i = 0; i < dsLichTrinhXe.Count; i++)
            {
                stt = i+1;
                int idLichTrinh = dsLichTrinhXe[i].id;
                var dsChiPhiXe = _entities.vt_ChiPhiLichTrinhXe.Where(x => x.daXoa == "0" && x.idLichTrinhXe == idLichTrinh).ToList();
                var dsLaiXe = _entities.vt_LaiXe.Where(x => x.daXoa == 0).ToList();
                for(int j = 0; j < dsLaiXe.Count; j++)
                {
                    int maXe = dsLaiXe[j].id;

                    var dsXeVanTai = _entities.vt_XeVanTai.Where(x => x.daXoa == "0" && x.idLaiXe == maXe).ToList();

                    foreach (var item in dsChiPhiXe)
                    {
                        foreach (var item2 in dsXeVanTai)
                        {
                            BaoCaoThongKeChiPhiModel tt = new BaoCaoThongKeChiPhiModel();
                            tt.stt = stt;
                            tt.maChuyen = dsLichTrinhXe[i].maChuyen;
                            tt.maXe = item2.maSoXe;
                            tt.chiPhi = (int)item.giaTien;
                            tt.laiXe = _entities.vt_LaiXe.Find(dsLichTrinhXe[i].idLaiXe).maLaiXe;
                            result.Add(tt);

                        }


                    }
                }
                


            }


            return result;

        }
        public List<BaoCaoCongNoPhaiThuModel> getBaoCaoCongNoPhaiThu(DateTime from, DateTime to, int loaiCongNo)
        {

            List<BaoCaoCongNoPhaiThuModel> result = new List<BaoCaoCongNoPhaiThuModel>();
            var dsLichTrinhXe = _entities.vt_LichTrinhXe.Where(y => y.thoiGianLapPhieu >= from && y.thoiGianLapPhieu <= to && y.tinhTrangThanhToan == loaiCongNo && y.daXoa == 0).ToList();
            int stt = 0;
            for (int i = 0; i < dsLichTrinhXe.Count; i++)
            {
                stt = i + 1;
                int idLichTrinh = dsLichTrinhXe[i].id;
                var dsChiPhiXe = _entities.vt_ChiPhiLichTrinhXe.Where(x => x.daXoa == "0" && x.idLichTrinhXe == idLichTrinh).ToList();
                var dsLaiXe = _entities.vt_LaiXe.Where(x => x.daXoa == 0).ToList();
                for (int j = 0; j < dsLaiXe.Count; j++)
                {
                    int maXe = dsLaiXe[j].id;

                    var dsXeVanTai = _entities.vt_XeVanTai.Where(x => x.daXoa == "0" && x.idLaiXe == maXe).ToList();

                    foreach (var item in dsChiPhiXe)
                    {
                        foreach (var item2 in dsXeVanTai)
                        {
                            BaoCaoCongNoPhaiThuModel tt = new BaoCaoCongNoPhaiThuModel();
                            tt.stt = stt;
                            tt.maChuyen = dsLichTrinhXe[i].maChuyen;
                            tt.maXe = item2.maSoXe;
                            tt.laiXe = _entities.vt_LaiXe.Find(dsLichTrinhXe[i].idLaiXe).maLaiXe;
                            tt.maKhachHang = _entities.vt_DMKhachHang.Find(dsLichTrinhXe[i].idKhachHang).tenKhachHang;
                            tt.thoiGianLapPhieu = dsLichTrinhXe[i].thoiGianLapPhieu.ToString("dd/MM/yyyy");
                            tt.noiDungLamViec = dsLichTrinhXe[i].noiDungLamViec;
                            tt.noiDi = dsLichTrinhXe[i].noiDi;
                            tt.noiDen = dsLichTrinhXe[i].noiDen;
                            tt.giaChuaThue = (double)dsLichTrinhXe[i].giaChuaThue;
                            if(dsLichTrinhXe[i].thueVat == 1)
                            {
                                tt.vat = "5%";

                            }
                            else if(dsLichTrinhXe[i].thueVat == 2)
                            {
                                tt.vat = "8%";

                            }
                            else{
                                tt.vat = "10%";

                            }
                            tt.tongThanhToan = (double)dsLichTrinhXe[i].tongThanhToan;
                            result.Add(tt);

                        }


                    }
                }



            }


            return result;

        }
    }
}