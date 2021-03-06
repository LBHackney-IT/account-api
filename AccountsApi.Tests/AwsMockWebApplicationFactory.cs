using System;
using AccountsApi.V1.Infrastructure;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.SimpleNotificationService;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Strategies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AccountsApi.Tests
{
    public class AwsMockWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly List<TableDef> _tables;

        public IAmazonDynamoDB DynamoDb { get; private set; }
        public IDynamoDBContext DynamoDbContext { get; private set; }
        public IAmazonSimpleNotificationService SimpleNotificationService { get; private set; }

        public AwsMockWebApplicationFactory(List<TableDef> tables)
        {
            _tables = tables;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(b => b.AddEnvironmentVariables())
                .UseStartup<Startup>();
            builder.ConfigureServices(services =>
            {
                var url = Environment.GetEnvironmentVariable("DynamoDb_LocalServiceUrl");
                var snsUrl = Environment.GetEnvironmentVariable("Localstack_SnsServiceUrl");
                services.AddSingleton<IAmazonDynamoDB>(sp =>
                {
                    var clientConfig = new AmazonDynamoDBConfig { ServiceURL = url };
                    return new AmazonDynamoDBClient(clientConfig);
                });
                services.AddSingleton<IAmazonSimpleNotificationService>(sp =>
                {
                    var clientConfig = new AmazonSimpleNotificationServiceConfig { ServiceURL = snsUrl };
                    return new AmazonSimpleNotificationServiceClient(clientConfig);
                });

                services.ConfigureAws();

                var serviceProvider = services.BuildServiceProvider();
                DynamoDb = serviceProvider.GetRequiredService<IAmazonDynamoDB>();
                DynamoDbContext = serviceProvider.GetRequiredService<IDynamoDBContext>();
                SimpleNotificationService = serviceProvider.GetRequiredService<IAmazonSimpleNotificationService>();

                EnsureTablesExist(DynamoDb, _tables);
            });
        }

        private static void EnsureTablesExist(IAmazonDynamoDB dynamoDb, List<TableDef> tables)
        {
            foreach (var table in tables)
            {
                try
                {
                    // Hanna Holosova
                    // This command helps to prevent the next exception:
                    // Amazon.XRay.Recorder.Core.Exceptions.EntityNotAvailableException : Entity doesn't exist in AsyncLocal
                    AWSXRayRecorder.Instance.ContextMissingStrategy = ContextMissingStrategy.LOG_ERROR;

                    List<AttributeDefinition> attributeDefinitions = new List<AttributeDefinition>();
                    List<GlobalSecondaryIndex> globalSecondaryIndexes = new List<GlobalSecondaryIndex>();

                    attributeDefinitions.Add(new AttributeDefinition(table.PartitionKey.KeyName,
                        table.PartitionKey.KeyScalarType));

                    if (table.Indices != null)
                    {
                        foreach (var index in table.Indices)
                        {
                            globalSecondaryIndexes.Add(
                                new GlobalSecondaryIndex()
                                {
                                    IndexName = index.IndexName,
                                    ProvisionedThroughput =
                                        new ProvisionedThroughput { ReadCapacityUnits = 1L, WriteCapacityUnits = 1L },
                                    KeySchema =
                                    {
                                        new KeySchemaElement { AttributeName = index.KeyName, KeyType = index.KeyType }
                                    },
                                    Projection = new Projection { ProjectionType = index.ProjectionType }
                                });
                            attributeDefinitions.Add(new AttributeDefinition(index.KeyName, index.KeyScalarType));
                        }
                    }

                    CreateTableRequest request = new CreateTableRequest
                    {
                        TableName = table.TableName,
                        ProvisionedThroughput =
                            new ProvisionedThroughput { ReadCapacityUnits = (long) 3, WriteCapacityUnits = (long) 3 },
                        AttributeDefinitions = attributeDefinitions,
                        KeySchema = new List<KeySchemaElement>
                        {
                            new KeySchemaElement(table.PartitionKey.KeyName, table.PartitionKey.KeyType)
                        },
                        GlobalSecondaryIndexes = globalSecondaryIndexes
                    };

                    _ = dynamoDb.CreateTableAsync(request).GetAwaiter().GetResult();
                }
                catch (ResourceInUseException)
                {
                    // It already exists :-)
                }
                catch (Exception exception)
                {
                    throw new Exception("Exception in checking table existence", exception);
                }
            }
        }
    }
}
