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
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;


namespace app
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        AwsOptions awsOptions;
        CredentialProfile profile;
        AWSCredentials awsCredentials;

        public Worker(IHttpClientFactory httpClientFactory, ILogger<Worker> logger, IOptionsMonitor<AwsOptions> awsOptionsMonitor)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            awsOptions = awsOptionsMonitor.CurrentValue;
            awsOptionsMonitor.OnChange(o => awsOptions = o);

            var chain = new CredentialProfileStoreChain();

            if (chain.TryGetAWSCredentials("test-profile", out awsCredentials))
            {
                logger.LogInformation("Credentials profile found...");
            }
            else
            {
                logger.LogInformation("Could not find credentials profile. Creating...");
            
                var options = new CredentialProfileOptions
                {
                    AccessKey = awsOptions.AWSAccessKey,
                    SecretKey = awsOptions.AWSSecretKey
                };

                profile = new CredentialProfile("test-profile", options);
                profile.Region = RegionEndpoint.USEast1;

                var sharedFile = new SharedCredentialsFile();
                sharedFile.RegisterProfile(profile);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                // CODE GOES HERE

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
