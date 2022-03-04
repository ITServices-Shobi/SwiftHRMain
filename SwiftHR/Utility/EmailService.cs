using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class EmailService
    {
        public void Send(string from, string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            //smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Connect("smtp.gmail.com", 465);
            smtp.Authenticate("swifthrsoft@gmail.com", "SwiftHR0904");
            //smtp.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);


            //// gmail
            //smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

            //// hotmail
            //smtp.Connect("smtp.live.com", 587, SecureSocketOptions.StartTls);

            //// office 365
            //smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
        }

        public void SendSalaryPdf(string from, string to, string subject, string html,byte[] attachmentfc)
        {
            // create message
            var email = new System.Net.Mail.MailMessage();
            email.From = new System.Net.Mail.MailAddress(from);
            email.To.Add(to);
            email.Subject = subject;
            email.Body = html;
            email.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(attachmentfc), "SalarySlip.pdf"));
            //SmtpClient SmtpServer = new SmtpClient("smtpout.asia.secureserver.net");
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient();

            SmtpServer.Host = "smtp.gmail.com";

            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            SmtpServer.UseDefaultCredentials = false;
           SmtpServer.Credentials = new System.Net.NetworkCredential("swifthrsoft@gmail.com", "SwiftHR0904");
            SmtpServer.Send(email);

        }
    }
}
