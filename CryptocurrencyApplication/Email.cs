using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace CryptocurrencyApplication
{
    class Email
    {
        private string myEmail = "crypto.currency.app.test31@gmail.com";
        private string password = "testing_123";
        private string pdfFilePath = "temp\\CryptoCurrency.pdf";

        public void SendEmail(string emailToSendTo)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(myEmail);
            mail.To.Add(emailToSendTo);
            mail.Subject = "Your cryptocurrency data that you requested!";
            mail.Body = "The mail has the file as an attachement!";

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            this.pdfFilePath = projectDirectory + "\\" + this.pdfFilePath;

            Attachment attachment;
            attachment = new Attachment(this.pdfFilePath);
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential(myEmail,password);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
        public Email()
        {
        }
    }
}
