using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Configuration;
using AccountsApi.V1.Domain;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AccountsApi.V1.Gateways.Interfaces;


namespace AccountsApi.V1.Gateways
{
    public class AccountSnsGateway : ISnsGateway
    {
        private readonly IAmazonSimpleNotificationService _amazonSimpleNotificationService;
        private readonly IConfiguration _configuration;
        private readonly JsonSerializerOptions _jsonOptions;

        public AccountSnsGateway(IAmazonSimpleNotificationService amazonSimpleNotificationService,
            IConfiguration configuration)
        {
            _amazonSimpleNotificationService = amazonSimpleNotificationService;
            _configuration = configuration;
            _jsonOptions = CreateJsonOptions();
        }

        private static JsonSerializerOptions CreateJsonOptions()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }

        public async Task Publish(AccountSns accountSns)
        {
            string message = JsonSerializer.Serialize(accountSns, _jsonOptions);
            var request = new PublishRequest
            {
                Message = message,
                TopicArn = Environment.GetEnvironmentVariable("ACCOUNTS_SNS_ARN"),
                MessageGroupId = "AccountSnsGroupId"
            };
            await _amazonSimpleNotificationService.PublishAsync(request).ConfigureAwait(false);
        }
    }
}

