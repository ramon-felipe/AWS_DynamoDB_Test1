using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDB.APIs.Demo.Persistence
{
    public interface IGetItem
    {
        Task<Dictionary<string, AttributeValue>> GetItemAsync();
    }
}
