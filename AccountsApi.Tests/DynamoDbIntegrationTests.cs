using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel; 
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace AccountsApi.Tests
{
    public class DynamoDbIntegrationTests<TStartup> where TStartup : class,IDisposable
    {
        //protected HttpClient Client { get; private set; }
        //private DynamoDbMockWebApplicationFactory<TStartup> _factory;
        //protected IDynamoDBContext DynamoDbContext => _factory?.DynamoDbContext;
        //protected List<Action> CleanupActions { get; set; }

        //private readonly List<TableDef> _tables = new List<TableDef>
        //{
        //    // TODO: Populate the list of table(s) and their key property details here, for example:
        //    //new TableDef { Name = "example_table", KeyName = "id", KeyType = ScalarAttributeType.N }
        //};

        //private static void EnsureEnvVarConfigured(string name, string defaultValue)
        //{
        //    if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(name)))
        //        Environment.SetEnvironmentVariable(name, defaultValue);
        //}

        //public DynamoDbIntegrationTests()
        //{ 
        //    EnsureEnvVarConfigured("DynamoDb_LocalMode", "true");
        //    EnsureEnvVarConfigured("DynamoDb_LocalServiceUrl", "http://localhost:8000");
        //    _factory = new DynamoDbMockWebApplicationFactory<TStartup>(_tables); 

        //    Client = _factory.CreateClient();
        //    CleanupActions = new List<Action>();
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //private bool _disposed;
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing && !_disposed)
        //    {
        //        foreach (var act in CleanupActions)
        //        {
        //            act();
        //        }
        //        Client.Dispose();

        //        if (null != _factory)
        //            _factory.Dispose();
        //        _disposed = true;
        //    }
        //}
    }

    public class TableDef
    {
        public string Name { get; set; }
        public string KeyName { get; set; }
        public ScalarAttributeType KeyType { get; set; }
    }
}
