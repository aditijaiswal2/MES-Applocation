using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using static MES.Client.Pages.ActiveSummaryReport;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpPost("downloadreport")]
        public IActionResult DownloadAsrReport([FromBody] List<ReceivingInspectionViewModel> data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("MES Report");

            // Add headers
            worksheet.Cell(1, 1).Value = "Serial Number";
            worksheet.Cell(1, 2).Value = "Customer";
            worksheet.Cell(1, 3).Value = "Rotor Number";
            worksheet.Cell(1, 4).Value = "Type";
            worksheet.Cell(1, 5).Value = "Categorize";
            worksheet.Cell(1, 6).Value = "Current Location";
            worksheet.Cell(1, 7).Value = "Received Date";
            worksheet.Cell(1, 8).Value = "Days in Plant";

            // Add data
            for (int i = 0; i < data.Count; i++)
            {
                var item = data[i];
                worksheet.Cell(i + 2, 1).Value = item.SerialNumber;
                worksheet.Cell(i + 2, 2).Value = item.Customer;
                worksheet.Cell(i + 2, 3).Value = item.RotorNumber;
                worksheet.Cell(i + 2, 4).Value = item.Type;
                worksheet.Cell(i + 2, 5).Value = item.Categorize;
                worksheet.Cell(i + 2, 6).Value = item.CurrentLocation;
                worksheet.Cell(i + 2, 7).Value = item.Date.ToString("yyyy-MM-dd");
                worksheet.Cell(i + 2, 8).Value = item.DaysInPlant;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ASR_Report.xlsx");
        }
    }
}
