using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using LTCDataManager.Email;
using LTCDataModel;
using LTCDataModel.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace LTC_Covid.Data
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<EmailManager.ElasticEmail> _email;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EmailSender(
            IOptions<EmailSettings> emailSettings,
            IOptions<EmailManager.ElasticEmail> email,
            IHostingEnvironment hostingEnvironment)
        {
            _email = email;
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
            //EmailManager.Send(
            //    subject,
            //    new[] { email },
            //    htmlMessage,
            //    new EmailManager.ElasticEmail
            //    {
            //        Email = _email.Value.Email,
            //        FromName = _email.Value.FromName,
            //        APIKey = _email.Value.APIKey
            //    });
        }
    }
}
