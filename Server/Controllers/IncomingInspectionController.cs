using iTextSharp.text.pdf;
using iTextSharp.text;
using MailKit.Security;
using MES.Server.Contracts;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using System.Collections.Generic;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Threading.Tasks;
using iTextSharp.text.pdf.draw;
using static MES.Client.Pages.LoginVC;
using Microsoft.EntityFrameworkCore;
using MES.Shared.DTOs.MES.Shared.DTOs.Rotors;
using MES.Server.Data.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;


namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomingInspectionController : ControllerBase
    {
        // private readonly IIncomingInspection _repository;


        private readonly HttpClient _httpClient;
        private readonly IncomingInspectionRepository _repository;
        //public WIPDataController(HttpClient httpClient, IncomingInspectionRepository repository)
        //{
        //    _httpClient = httpClient;
        //    _wipService = wipService;
        //}

        public IncomingInspectionController(HttpClient httpClient, IncomingInspectionRepository repository)
        {
            _httpClient = httpClient;
            _repository = repository;
        }

        // GET: api/IncomingInspection
        [HttpGet("getIncomingData")]
        //public async Task<ActionResult<IEnumerable<IncomingInspection>>> GetAll()
        //{
        //    var inspections = await _repository.GetAllAsync();
        //    return Ok(inspections);
        //}


        [HttpGet("CheckSerialExists/{serialNumber}")]
        public async Task<IActionResult> CheckSerialExists(string serialNumber)
        {
            var exists = await _repository.SerialNumberExistsAsync(serialNumber);
            return Ok(exists);
        }



        // GET: api/IncomingInspection/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IncomingInspection>> Get(int id)
        {
            var inspection = await _repository.GetByIdAsync(id);
            if (inspection == null) return NotFound();
            return Ok(inspection);
        }


        /// <summary>
        /// Generates a PDF file from the form data.
        /// </summary>
        private byte[] GeneratePDF(IncomingInspectionDTO IncomingDataDTO)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 36, 36, 36, 36);
                PdfWriter.GetInstance(document, stream);
                document.Open();

                // Title
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                var sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                var labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                var valueFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                Paragraph title = new Paragraph("Incoming Rotors Inspection", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20
                };
                document.Add(title);

                // Line separator
                document.Add(new Paragraph(new Chunk(new LineSeparator())));

                // Info Table
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;
                table.SpacingBefore = 20;
                table.DefaultCell.Border = Rectangle.NO_BORDER;

                // Helper method to add rows
                void AddRow(string label, string value)
                {
                    table.AddCell(new PdfPCell(new Phrase(label, labelFont)) { Border = Rectangle.NO_BORDER, Padding = 5 });
                    table.AddCell(new PdfPCell(new Phrase(value ?? "", valueFont)) { Border = Rectangle.NO_BORDER, Padding = 5 });
                }
                AddRow("Serial Number:", IncomingDataDTO.SerialNumber);
                AddRow("Module:", IncomingDataDTO.Module);
                AddRow("Sales Order Number:", IncomingDataDTO.SalesOrderNumber);
                AddRow("Work Order:", IncomingDataDTO.WorkOrder);
                AddRow("Material Number:", IncomingDataDTO.MatNumber);
                AddRow("Customer:", IncomingDataDTO.Customer);
                AddRow("Location:", IncomingDataDTO.Location);
                AddRow("Received:", IncomingDataDTO.Received);
                AddRow("Inspected:", IncomingDataDTO.Inspected);
                AddRow("Rotors:", IncomingDataDTO.RotorsNumber);
                AddRow("Initials:", IncomingDataDTO.Initials);
                AddRow("Make:", IncomingDataDTO.Make);
                AddRow("Dia:", IncomingDataDTO.Dia);
                AddRow("Len:", IncomingDataDTO.Len);
                AddRow("Fits:", IncomingDataDTO.Fits);
                AddRow("Material:", IncomingDataDTO.Materials);
                AddRow("Other:", IncomingDataDTO.Others);
                AddRow("Rotors Dia:", IncomingDataDTO.RotorsDia);
                AddRow("Rotors Style:", IncomingDataDTO.RotorStyle);
                AddRow("Type:", IncomingDataDTO.Type);
                AddRow("Bearing Removed:", IncomingDataDTO.BearingRemoved);
                AddRow("Bearings:", IncomingDataDTO.Bearing);
                AddRow("Bearing Seals:", IncomingDataDTO.BearingSeals);
                AddRow("Ceramic Seals:", IncomingDataDTO.CeramicSeals);
                AddRow("Bearing Journal Dia (Right):", IncomingDataDTO.Right);
                AddRow("Bearing Journal Dia (Left):", IncomingDataDTO.Left);
                AddRow("Basic Sharpening:", IncomingDataDTO.BasicSharpening);
                AddRow("Basic Sharpening Details:", IncomingDataDTO.IfYBasicSharpening);
                AddRow("Wedgelock Alignment Marks; Present:", IncomingDataDTO.WedgelockAlignmentMarks);
                AddRow("Center Grinding:", IncomingDataDTO.CenterGrinding);
                AddRow("Center Grinding Details:", IncomingDataDTO.IfYCenterGrinding);
                AddRow("Aligned:", IncomingDataDTO.Aligned);
                AddRow("Plastic Sleaves:", IncomingDataDTO.PlasticSleaves);
                AddRow("Welding:", IncomingDataDTO.Welding);
                AddRow("Welding Num:", IncomingDataDTO.WeldingNum);
                AddRow("Bed knife in box:", IncomingDataDTO.BedKnife);
                AddRow("Replace Blades:", IncomingDataDTO.BoxReceivedWithSaddles);
                AddRow("Re-Profile:", IncomingDataDTO.ReProfile);
                AddRow("Sand Blasting:", IncomingDataDTO.SandBlasting);
                AddRow("Manual Labor:", IncomingDataDTO.ManualLabor);
                AddRow("TIR Left Journal:", IncomingDataDTO.TirLeftJournal);
                AddRow("TIR Right Journal:", IncomingDataDTO.TirRightJournal);
                AddRow("Box Received with Saddles(Bottom):", IncomingDataDTO.Bottom);
                AddRow("Box Received with Saddles(Top):", IncomingDataDTO.Top);
                AddRow("Add Qty:", IncomingDataDTO.AddQty.ToString());
                AddRow("Saddle Part Number:", IncomingDataDTO.SaddlePartNumber);

                document.Add(table);

                document.Close();
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Sends an email with the generated PDF attachment.
        /// </summary>
        private async Task SendEmailWithAttachment(byte[] pdfBytes, IncomingInspectionDTO IncomingDataDTO)
        {
            var user = HttpContext.User;
            var fromEmail = user.Identity?.IsAuthenticated == true
                ? user.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value
                : "aditi.jaiswal@axiscades.in";
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Axiscades", fromEmail));
            email.To.Add(new MailboxAddress("Sales Team", "aditi.jaiswal@axiscades.in"));
            email.Subject = $"{IncomingDataDTO.SerialNumber} - {IncomingDataDTO.Customer}";
            email.Body = new TextPart(TextFormat.Plain) { Text = "Please find the attached inspection report." };

            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(new MemoryStream(pdfBytes)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = "InspectionReport.pdf"
            };

            var multipart = new Multipart("mixed");
            multipart.Add(email.Body);
            multipart.Add(attachment);
            email.Body = multipart;

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls); // If using Outlook
            await smtp.AuthenticateAsync("aditi.jaiswal@axiscades.in", "AxisMar@2025"); // Use correct password

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpPost("addincoming")]

        public async Task<IActionResult> Create([FromBody] IncomingInspectionDTO incomingDataDTO)
        {
            if (incomingDataDTO == null)
            {
                return BadRequest("Incoming data is null.");
            }

            try
            {

                // Add the inspection to the database
                var createdInspection = await _repository.Add(incomingDataDTO);

                if (createdInspection == null)
                {
                    return StatusCode(500, "Failed to create inspection.");
                }

                // Generate PDF from the incoming data
                byte[] pdfBytes = GeneratePDF(incomingDataDTO);

                // Send email with PDF as attachment
                await SendEmailWithAttachment(pdfBytes, incomingDataDTO);

                return CreatedAtAction(nameof(Get), new { id = createdInspection.Id }, createdInspection);
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.WriteLine($"Error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }


    }
}
