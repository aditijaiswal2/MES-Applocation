using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MES.Shared.Models.Rotors;
using MES.Server.Data;
using MES.Shared.DTOs;
using MailKit.Security;
using MES.Shared.DTOs.MES.Shared.DTOs.Rotors;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Identity;
using MES.Shared.Entities;

namespace YourAppNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotorsFinalInspectionController : ControllerBase
    {
        private readonly ProjectdbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RotorsFinalInspectionController(ProjectdbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/RotorsFinalInspection
        [HttpGet("GetFinalInspection")]
        public async Task<ActionResult<IEnumerable<RotorsFinalInspection>>> GetRotorsFinalInspections()
        {
            return await _context.RotorsFinalInspections.ToListAsync();
        }

        [HttpGet("GetAllFIData")]
        public async Task<ActionResult<IEnumerable<RotorsFinalInspection>>> GetAll()
        {
            var records = await _context.RotorsFinalInspections.ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No final inspection records found.");

            return Ok(records);
        }

        // GET: api/RotorsFinalInspection/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RotorsFinalInspection>> GetRotorsFinalInspection(int id)
        {
            var rotor = await _context.RotorsFinalInspections.FindAsync(id);

            if (rotor == null)
            {
                return NotFound();
            }

            return rotor;
        }

        // POST: api/RotorsFinalInspection
        [HttpPost("AddFinalInspection")]
        public async Task<ActionResult<RotorsFinalInspection>> PostRotorsFinalInspection(RotorsFinalInspection rotor)
        {
            try
            {
                // _context.RotorsFinalInspections.Add(rotor);
                // Add the inspection to the database
                _context.RotorsFinalInspections.Add(rotor);

                // Generate PDF from the incoming data
                byte[] pdfBytes = GeneratePDF(rotor);

                // Send email with PDF as attachment
                await SendEmailWithAttachment(pdfBytes, rotor);

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRotorsFinalInspection), new { id = rotor.Id }, rotor);
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.WriteLine($"Error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }


        private byte[] GeneratePDF(RotorsFinalInspection rotor)
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

                Paragraph title = new Paragraph("Final Inspection Report", titleFont)
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
                AddRow("Serial Number:", rotor.SerialNumber);
                AddRow("Module:", rotor.Module);
                AddRow("Sales Order Number:", rotor.SalesOrderNumber);
                AddRow("Work Order:", rotor.WorkOrder);
                AddRow("Material Number:", rotor.MatNumber);
                AddRow("Customer:", rotor.Customer);
                AddRow("Location:", rotor.Location);
                AddRow("Received:", rotor.Received);
                AddRow("Inspected:", rotor.Inspected);
                AddRow("Rotors:", rotor.RotorsNumber);
                AddRow("Material:", rotor.Materials);
                AddRow("Rotors Dia:", rotor.RotorsDia);

                AddRow("Rotor Categorization:", rotor.RotorCategorization);
                AddRow("Component Type:", rotor.ComponentType);

                AddRow("Target Date:", rotor.TargetDate.ToString());
                AddRow("Customer Importance:", rotor.CustomerImportance);
                AddRow("Advanced Sharping Status:", rotor.AdvancedSharpingStatus);
                AddRow("Workcenter:", rotor.Workcenters);
                AddRow("Rotors Dia Left:", rotor.RotorsDiaLeft);
                AddRow("Rotors Dia Right:", rotor.RotorsDiaRight);
                AddRow("Relief Land:", rotor.ReliefLand);
                AddRow("Tooth Face Left:", rotor.ToothFaceLeft);
                AddRow("Tooth Face Right:", rotor.ToothFaceRight);
                AddRow("Centers Left:", rotor.CentersLeft);
                AddRow("Centers Right:", rotor.CentersRight);
                AddRow("Visual Checks:", rotor.VisualChecks);
                AddRow("InspectedBy:", rotor.InspectedBy);
                AddRow("Grinding Start Date:", rotor.GrindingStartDate.ToString());
                AddRow("Grinding End Date:", rotor.GrindingEndDate);
                AddRow("Notes:", rotor.Notes);
                AddRow("Delay Reason Tracking:", rotor.DelayReasonTracking);
                AddRow("Customer Po Num:", rotor.CustomerPoNum);
                AddRow("DWG Number:", rotor.DWGNum);
                AddRow("AG Num:", rotor.AGNum);
                AddRow("Special Note Comment:", rotor.SpecialNoteComment);
                AddRow("Dressed with new bearing:", rotor.Dressedwithnewbearing);

                document.Add(table);

                document.Close();
                return stream.ToArray();
            }
        }


        private async Task SendEmailWithAttachment(byte[] pdfBytes, RotorsFinalInspection rotor)
        {
            var fromEmail = "aditi.jaiswal@axiscades.in";
            var emailSubject = $"{rotor.SerialNumber} - {rotor.Customer}";

            var bodyText = $@"Dear Sir/Madam,

Please find the attached Final inspection report.

Regards,
Final Inspection 
{rotor.Users}";

            // Fetch users where IsSalesUser == true
            var salesUsers = await _userManager.Users
                .Where(u => u.IsSalesUser && u.isDeleted == 0)
                .ToListAsync();

            var salesEmails = salesUsers
                .Where(u => !string.IsNullOrEmpty(u.Email)) // Ensure Email is not null
                .Select(u => u.Email)
                .Distinct()
                .ToList();

            if (salesEmails.Count == 0)
            {
                // No sales users found
                return;
            }

            var bodyPart = new TextPart(TextFormat.Plain) { Text = bodyText };

            foreach (var recipientEmail in salesEmails)
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("MES", fromEmail));
                email.To.Add(new MailboxAddress("Sales Team", recipientEmail));
                email.Subject = emailSubject;

                var attachment = new MimePart("application", "pdf")
                {
                    Content = new MimeContent(new MemoryStream(pdfBytes)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = "FinalInspectionReport.pdf"
                };

                var multipart = new Multipart("mixed");
                multipart.Add(bodyPart);
                multipart.Add(attachment);
                email.Body = multipart;

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(fromEmail, "AxisMar@2025"); // ❗ Ideally use secure credentials!
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
        // PUT: api/RotorsFinalInspection/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRotorsFinalInspection(int id, RotorsFinalInspection rotor)
        {
            if (id != rotor.Id)
            {
                return BadRequest();
            }

            _context.Entry(rotor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.RotorsFinalInspections.Any(e => e.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/RotorsFinalInspection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRotorsFinalInspection(int id)
        {
            var rotor = await _context.RotorsFinalInspections.FindAsync(id);
            if (rotor == null)
            {
                return NotFound();
            }

            _context.RotorsFinalInspections.Remove(rotor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("AddAllData")]
        public async Task<IActionResult> PostFinalInspection([FromBody] RotorFinalInspectionDTO model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data.");
            }

            return Ok(new { message = "Inspection data saved successfully" });
        }

        [HttpGet("CheckSerialExists/{serialNumber}")]
        public async Task<IActionResult> CheckSerialExists(string serialNumber)
        {
            var exists = await _context.RotorsFinalInspections
             .AnyAsync(r => r.SerialNumber.ToLower() == serialNumber.ToLower());

            return Ok(exists);
        }

    }
}

