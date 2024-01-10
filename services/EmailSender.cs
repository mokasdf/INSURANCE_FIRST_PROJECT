using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace INSURANCE_FIRST_PROJECT.services
{
    public class EmailSender : IEmailSender
    {
        public void SendEmailAsync(string email, string subject, string message,string invoice,string username, string price, string subtype,bool bene)
        {
            DateTime date = DateTime.Now.Date;
            //
            var pdfDocument = new Document();
            var page = pdfDocument.Pages.Add();
            // Create text fragments for invoice information
            var invoiceNumber = new TextFragment($"Invoice {invoice}");
            var invoiceDate = new TextFragment($"Invoice Date: {date}");
            var customerInfo = new TextFragment($"Customer: {username}");

            // Set text formatting (e.g., font size, alignment)
            invoiceNumber.TextState.FontSize = 16;
            invoiceNumber.HorizontalAlignment = HorizontalAlignment.Left;
            invoiceDate.TextState.FontSize = 12;
            invoiceDate.HorizontalAlignment = HorizontalAlignment.Left;
            customerInfo.TextState.FontSize = 12;
            customerInfo.HorizontalAlignment = HorizontalAlignment.Left;

            // Add text fragments to the page
            page.Paragraphs.Add(invoiceNumber);
            page.Paragraphs.Add(invoiceDate);
            page.Paragraphs.Add(customerInfo);

            var table = new Table
            {
                ColumnWidths = "200",
            };
            page.Paragraphs.Add(table);

            // Define table headings
            var row = table.Rows.Add();
            row.Cells.Add("Description");
            row.Cells.Add("Amount");

            // Add invoice items
            var items = new[]
            {
            new { Description = $"subscr{subtype}", Amount = $"${price}" }
            };

            foreach (var item in items)
            {
                row = table.Rows.Add();
                row.Cells.Add(item.Description);
                row.Cells.Add(item.Amount);
            }

            if (bene) { 
            pdfDocument.Save($"Invoice{invoice}.pdf");
            }
            //



            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("smtp.service.asp@outlook.com", "shgh9999")
            };
            Attachment file = new Attachment($"Invoice{invoice}.pdf");

            

            MailMessage s = new MailMessage(from: "smtp.service.asp@outlook.com",
                                to: email,
                                subject,
                                message);
            if (bene) { 
            s.Attachments.Add(file);
            }

            client.SendMailAsync(s);

        }


        //public string pdffile()
        //{
            

        //    var pdfDocument = new Document();
        //    var page = pdfDocument.Pages.Add();
        //    // Create text fragments for invoice information
        //    var invoiceNumber = new TextFragment("Invoice #12345");
        //    var invoiceDate = new TextFragment("Invoice Date: September 1, 2023");
        //    var customerInfo = new TextFragment("Customer: John Doe\nAddress: 123 Main St\nCity: Anytown, USA");

        //    // Set text formatting (e.g., font size, alignment)
        //    invoiceNumber.TextState.FontSize = 16;
        //    invoiceNumber.HorizontalAlignment = HorizontalAlignment.Left;
        //    invoiceDate.TextState.FontSize = 12;
        //    invoiceDate.HorizontalAlignment = HorizontalAlignment.Left;
        //    customerInfo.TextState.FontSize = 12;
        //    customerInfo.HorizontalAlignment = HorizontalAlignment.Left;

        //    // Add text fragments to the page
        //    page.Paragraphs.Add(invoiceNumber);
        //    page.Paragraphs.Add(invoiceDate);
        //    page.Paragraphs.Add(customerInfo);

        //    var table = new Table
        //    {
        //        ColumnWidths = "200",
        //    };
        //    page.Paragraphs.Add(table);

        //    // Define table headings
        //    var row = table.Rows.Add();
        //    row.Cells.Add("Description");
        //    row.Cells.Add("Amount");

        //    // Add invoice items
        //    var items = new[]
        //    {
        //    new { Description = "Item 1", Amount = "$50.00" },
        //    new { Description = "Item 2", Amount = "$75.00" },
        //    new { Description = "Item 3", Amount = "$30.00" },
        //    };

        //    foreach (var item in items)
        //    {
        //        row = table.Rows.Add();
        //        row.Cells.Add(item.Description);
        //        row.Cells.Add(item.Amount);
        //    }

        //     pdfDocument.Save("Invoice.pdf");

        //}
    }
}
