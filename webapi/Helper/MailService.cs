using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;
using System.Net.Mail;
using System.Net;
namespace webapi.Helper
{
    public class MailService
    {
        private string emailServer;
        private string userName;
        private string password;
        private string emailTo;
        private string emailBCC;

        public MailService(IOptions<EmailSettings> options)
        {
            emailServer = options.Value.EmailServer;
            userName = options.Value.UserName;
            password = options.Value.Passwrod;
            emailTo = options.Value.EmailTo;
            emailBCC = options.Value.EmailBCC;
        }

        public string SendEmail(string subject, string body)
        {
            string result = string.Empty;
            try
            {
                SmtpClient client = new SmtpClient();
                /*client.Port = 25;
                client.EnableSsl = false;
                */
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Host = emailServer;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(userName,password);
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(userName);
                mailMessage.To.Add(emailTo);
                mailMessage.Bcc.Add(emailBCC);
                mailMessage.Body = body;
                mailMessage.Subject = subject;
                client.Send(mailMessage);

            }
            catch (Exception ee)
            {
                result = ee.Message;
            }
            return result;
        }
    }
}
