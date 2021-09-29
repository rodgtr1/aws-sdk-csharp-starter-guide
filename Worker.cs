using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using app.Models;

namespace app
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        AwsOptions awsOptions;

        public Worker(IHttpClientFactory httpClientFactory, ILogger<Worker> logger, IOptionsMonitor<AwsOptions> awsOptionsMonitor)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            awsOptions = awsOptionsMonitor.CurrentValue;
            awsOptionsMonitor.OnChange(o => awsOptions = o);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
