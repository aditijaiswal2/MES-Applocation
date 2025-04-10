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


namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomingInspectionController : ControllerBase
    {
        private readonly IIncomingInspection _repository;

        public IncomingInspectionController(IIncomingInspection repository)
        {
            _repository = repository;
        }

        // GET: api/IncomingInspection
        [HttpGet("getIncomingData")]
        public async Task<ActionResult<IEnumerable<IncomingInspection>>> GetAll()
        {
            var inspections = await _repository.GetAllAsync();
            return Ok(inspections);
        }



        // GET: api/IncomingInspection/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IncomingInspection>> Get(int id)
        {
            var inspection = await _repository.GetByIdAsync(id);
            if (inspection == null) return NotFound();
            return Ok(inspection);
        }

        // POST: api/IncomingInspection
        [HttpPost("PostIncomingdata")]
        public async Task<IActionResult> Create([FromBody] IncomingInspection inspection)
        {
            if (inspection == null)
            {
                return BadRequest("Invalid data.");
            }

         //   SaveImagesToDisk(inspection);

            // Save data to the database
            var created = await _repository.AddAsync(inspection);

            // Generate PDF
            byte[] pdfBytes = GeneratePDF(inspection);

            // Send email with PDF attachment
            await SendEmailWithAttachment(pdfBytes, inspection);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        /// <summary>
        /// Generates a PDF file from the form data.
        /// </summary>
        private byte[] GeneratePDF(IncomingInspection inspection)
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

                Paragraph title = new Paragraph("Incomingn Rotors Inspection", titleFont)
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
                AddRow("Serial Number:", inspection.SerialNumber);
                AddRow("Module:", inspection.Module);
                AddRow("Sales Order Number:", inspection.SalesOrderNumber);
                AddRow("Work Order:", inspection.WorkOrder);
                AddRow("Material Number:", inspection.MatNumber);
                AddRow("Customer:", inspection.Customer);
                AddRow("Location:", inspection.Location);
                AddRow("Received:", inspection.Received);
                AddRow("Inspected:", inspection.Inspected);
                AddRow("Rotors:", inspection.RotorsNumber);
                AddRow("Initials:", inspection.Initials);
                AddRow("Make:", inspection.Make);
                AddRow("Dia:", inspection.Dia);
                AddRow("Len:", inspection.Len);
                AddRow("Fits:", inspection.Fits);
                AddRow("Material:", inspection.Materials);
                AddRow("Other:", inspection.Others);
                AddRow("Rotors Dia:", inspection.RotorsDia);
                AddRow("Rotors Style:", inspection.RotorStyle);
                AddRow("Type:", inspection.Type);
                AddRow("Bearing Removed:", inspection.BearingRemoved);
                AddRow("Bearings:", inspection.Bearing);
                AddRow("Bearing Seals:", inspection.BearingSeals);
                AddRow("Ceramic Seals:", inspection.CeramicSeals);
                AddRow("Bearing Journal Dia (Right):", inspection.Right);
                AddRow("Bearing Journal Dia (Left):", inspection.Left);
                AddRow("Basic Sharpening:", inspection.BasicSharpening);
                AddRow("Basic Sharpening Details:", inspection.IfYBasicSharpening);
                AddRow("Wedgelock Alignment Marks; Present:", inspection.WedgelockAlignmentMarks);
                AddRow("Center Grinding:", inspection.CenterGrinding);
                AddRow("Center Grinding Details:", inspection.IfYCenterGrinding);
                AddRow("Aligned:", inspection.Aligned);
                AddRow("Plastic Sleaves:", inspection.PlasticSleaves);
                AddRow("Welding:", inspection.Welding);
                AddRow("Welding Num:", inspection.WeldingNum);
                AddRow("Bed knife in box:", inspection.BedKnife);
                AddRow("Replace Blades:", inspection.BoxReceivedWithSaddles );
                AddRow("Re-Profile:", inspection.ReProfile);
                AddRow("Sand Blasting:", inspection.SandBlasting);
                AddRow("Manual Labor:", inspection.ManualLabor);
                AddRow("TIR Left Journal:", inspection.TirLeftJournal);
                AddRow("TIR Right Journal:", inspection.TirRightJournal);
                AddRow("Box Received with Saddles(Bottom):", inspection.Bottom);
                AddRow("Box Received with Saddles(Top):", inspection.Top);
                AddRow("Add Qty:", inspection.AddQty.ToString());
                AddRow("Saddle Part Number:", inspection.SaddlePartNumber);

                document.Add(table);

                document.Close();
                return stream.ToArray();
            }
        }


        /// <summary>
        /// Sends an email with the generated PDF attachment.
        /// </summary>
        private async Task SendEmailWithAttachment(byte[] pdfBytes, IncomingInspection inspection)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Axiscades", "aditi.jaiswal@axiscades.in"));
            email.To.Add(new MailboxAddress("Sales Team", "aditi.jaiswal@axiscades.in"));
            email.Subject = $"Inspection Report - {inspection.SalesOrderNumber}";
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
        // PUT: api/IncomingInspection/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<IncomingInspection>> Update(int id, IncomingInspection inspection)
        {
            var updated = await _repository.UpdateAsync(id, inspection);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        // DELETE: api/IncomingInspection/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
