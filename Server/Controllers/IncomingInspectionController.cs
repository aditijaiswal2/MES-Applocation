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
        public async Task<IActionResult> GetAll()
        {
            var inspections = await _repository.GetAllAsync();
            return Ok(inspections);
        }

        // GET: api/IncomingInspection/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inspection = await _repository.GetByIdAsync(id);
            if (inspection == null)
            {
                return NotFound();
            }
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

            // Save data to the database
            await _repository.AddAsync(inspection);

            // Generate PDF
            byte[] pdfBytes = GeneratePDF(inspection);

            // Send email with PDF attachment
            await SendEmailWithAttachment(pdfBytes, inspection);

            return CreatedAtAction(nameof(GetById), new { id = inspection.Id }, inspection);
        }

        /// <summary>
        /// Generates a PDF file from the form data.
        /// </summary>
        private byte[] GeneratePDF(IncomingInspection inspection)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, stream);
                document.Open();

                document.Add(new Paragraph("Incoming Inspection Report"));
                document.Add(new Paragraph($"Sales Order Number: {inspection.SalesOrderNumber}"));
                document.Add(new Paragraph($"Work Order: {inspection.WorkOrder}"));
                document.Add(new Paragraph($"Customer: {inspection.Customer}"));
                document.Add(new Paragraph($"Location: {inspection.Location}"));
                document.Add(new Paragraph($"Date Received: {inspection.Received}"));
                document.Add(new Paragraph($"Inspected On: {inspection.Inspected}"));

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
        public async Task<IActionResult> Update(int id, [FromBody] IncomingInspection inspection)
        {
            if (id != inspection.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(inspection);
            return NoContent();
        }

        // DELETE: api/IncomingInspection/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingInspection = await _repository.GetByIdAsync(id);
            if (existingInspection == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
