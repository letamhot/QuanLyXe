using OfficeOpenXml;
using OfficeOpenXml.Style;
using QLDV.Models.DAL.BaoCaoThongKe;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDV.Controllers
{
    public class BaoCaoChiPhiXeController : Controller
    {
        // GET: BaoCaoChiPhiXe
        private BaoCaoThongKeService _bctkService = new BaoCaoThongKeService();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult bcThongKeChiPhiXe()
        {
            string startDate = Request.QueryString["from"];
            string endDate = Request.QueryString["to"];
            DateTime from = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return Json(new { data = _bctkService.getBaoCaoThongChiPhiXe(from, to) }, JsonRequestBehavior.AllowGet);
        }
        public void ExportExcelBaoCaoThongke()
        {
            string startDate = Request.QueryString["from"];
            string endDate = Request.QueryString["to"];
            DateTime from = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var data = _bctkService.getBaoCaoThongChiPhiXe(from, to);

            ExcelPackage ep = new ExcelPackage();
            ExcelWorksheet sheet = ep.Workbook.Worksheets.Add("Report");
            int row = 1;
            //sheet.Column(1).Width = 5;
            //sheet.Column(2).Width = 35;
            //sheet.Column(3).Width = 55;
            //sheet.Column(4).Width = 35;
            //sheet.Column(5).Width = 20;
            //sheet.Column(6).Width = 30;
            sheet.Cells[row, 1, row + 2, 5].Merge = true;
            sheet.Cells["A" + row.ToString()].Value = "Báo Cáo Thống Kê Chi Phí Xe";
            sheet.Cells["A" + row.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells["A" + row.ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            row = row + 3;
            sheet.Cells["A" + row.ToString()].Value = "STT";
            sheet.Cells["B" + row.ToString()].Value = "Mã chuyến";
            sheet.Cells["C" + row.ToString()].Value = "Mã xe";
            sheet.Cells["D" + row.ToString()].Value = "Mã lái xe";

            sheet.Cells["E" + row.ToString()].Value = "Chi phí(VND)";
           
            sheet.Row(row).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Row(row).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            sheet.Row(row).Style.Font.Bold = true;
            sheet.Cells[row, 1, row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sheet.Cells[row, 1, row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sheet.Cells[row, 1, row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[row, 1, row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            //sheet.Cells[row, 1, row, 3].Style.b
            //a
            row++;
            int stt = 1;
            if (data != null && data.Count > 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].stt != 0)
                    {
                        sheet.Cells["A" + row.ToString()].Value = data[i].stt;

                    }
                    else
                    {
                        sheet.Cells["A" + row.ToString()].Value = "";
                    }
                    sheet.Cells["B" + row.ToString()].Value = data[i].maChuyen;
                    sheet.Cells["C" + row.ToString()].Value = data[i].maXe;
                    sheet.Cells["D" + row.ToString()].Value = data[i].laiXe;
                    sheet.Cells["E" + row.ToString()].Value = data[i].chiPhi;
                    
                   
                    sheet.Cells[row, 1, row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[row, 1, row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[row, 1, row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[row, 1, row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    stt++;
                    row++;
                }
            }
            sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "BaoCaoChiPhiXe.xlsx");
            Response.BinaryWrite(ep.GetAsByteArray());
            Response.End();



        }
    }
}