using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using LTCDataModel;
using LTCDataModel.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace LTC_Covid.Data
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSetting;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EmailSender(
            IOptions<EmailSettings> emailSettings,
            IHostingEnvironment hostingEnvironment)
        {
            _emailSetting = emailSettings.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// This method is responsible for sending emails
        /// </summary>
        /// <param name="email">To Email</param>
        /// <param name="subject"></param>
        /// <param name="htmlMessage"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            //MailMessage mail = new MailMessage();


            //mail.From = new MailAddress(_emailSetting.SenderEmail);
            //mail.To.Add(email);

            //mail.Subject = subject;
            //mail.Body = htmlMessage;
            //using (SmtpClient smtpServer = new SmtpClient(_emailSetting.MailServer))
            //{
            //    smtpServer.UseDefaultCredentials = false;
            //    smtpServer.Port = _emailSetting.MailPort;
            //    smtpServer.Credentials = new System.Net.NetworkCredential(_emailSetting.SenderEmail, _emailSetting.Password);
            //    smtpServer.EnableSsl = true;
            //    await smtpServer.SendMailAsync(mail);
            //}

            await ElasticEmailClient.Api.Email.SendAsync(subject, _emailSetting.SenderEmail, email, msgTo: new string[] { email }, bodyHtml: htmlMessage, bodyText: htmlMessage);

        }
    }
}
