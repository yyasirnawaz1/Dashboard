using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DentalDataManager.DataManager.NewsLetter;
using LTCDataManager.Email;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace LTC_Covid.Models
{
    public class HostingService : IHostedService
    { 
        private Timer _timer;
        private readonly IOptions<EmailManager.ElasticEmail> _email;

        public HostingService(IOptions<EmailManager.ElasticEmail> email)
        {
            _email = email;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            gEmailSchedular.GetTemplates(_email.Value.Email, _email.Value.APIKey, _email.Value.FromName);

            //if (DateTime.Now.Hour == 8 || DateTime.Now.Hour == 12) // send message at 8 AM
            //{
            //}

            //ExceptionHandler.SendLiveMessage(settingFile);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
