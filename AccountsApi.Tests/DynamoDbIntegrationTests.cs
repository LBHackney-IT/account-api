using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace AccountsApi.Tests
{
    public class DynamoDbIntegrationTests<TStartup> : IDisposable where TStartup : class
    {
        protected HttpClient Client { get; private set; }
        private readonly DynamoDbMockWebApplicationFactory<TStartup> _factory;
        protected IDynamoDBContext DynamoDbContext => _factory?.DynamoDbContext;
        protected List<Action> CleanupActions { get; }

        private readonly List<TableDef> _tables = new List<TableDef>
        {
            new TableDef()
            {
                TableName = "Accounts",
                PartitionKey = new AttributeDef()
                {
                    KeyName = "id",
                    KeyType = KeyType.HASH,
                    KeyScalarType = ScalarAttributeType.S
                },
                Indices = new List<GlobalIndexDef>{
                    new GlobalIndexDef()
                    {
                        KeyName = "target_id",
                        KeyType = KeyType.HASH,
                        KeyScalarType = ScalarAttributeType.S,
                        IndexName = "target_id_dx",
                        ProjectionType = "ALL"
                    },
                    new GlobalIndexDef()
                    {
                        KeyName = "account_type",
                        KeyType = KeyType.HASH,
                        KeyScalarType = ScalarAttributeType.S,
                        IndexName = "account_type_dx",
                        ProjectionType = "ALL"
                    }
                }
            },
        };

        private static void EnsureEnvVarConfigured(string name, string defaultValue)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(name)))
            {
                Environment.SetEnvironmentVariable(name, defaultValue);
            }
        }

        public DynamoDbIntegrationTests()
        {
            EnsureEnvVarConfigured("DynamoDb_LocalMode", "true");
            EnsureEnvVarConfigured("DynamoDb_LocalServiceUrl", "http://localhost:8000");
            EnsureEnvVarConfigured("DynamoDb_LocalSecretKey", "vymxp");
            EnsureEnvVarConfigured("DynamoDb_LocalAccessKey", "2cl9i");
            _factory = new DynamoDbMockWebApplicationFactory<TStartup>(_tables);

            Client = _factory.CreateClient();
            CleanupActions = new List<Action>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                foreach (var act in CleanupActions)
                {
                    act();
                }
                Client.Dispose();

                if (null != _factory)
                    _factory.Dispose();
                _disposed = true;
            }
        }
    }

    public class TableDef
    {
        public string TableName { get; set; }
        public AttributeDef PartitionKey { get; set; }
        public List<GlobalIndexDef> Indices { get; set; }
    }

    public class AttributeDef
    {
        public string KeyName { get; set; }
        public ScalarAttributeType KeyScalarType { get; set; }
        public KeyType KeyType { get; set; }
    }

    public class GlobalIndexDef : AttributeDef
    {
        public string IndexName { get; set; }
        public string ProjectionType { get; set; }
    }
}
