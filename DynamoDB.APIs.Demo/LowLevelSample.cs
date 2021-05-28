using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DynamoDB.APIs.Demo.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDB.APIs.Demo
{
    public class LowLevelSample : IPersistence
    {
        private readonly IAmazonDynamoDB ddbClient;

        public LowLevelSample(AmazonDynamoDBClient amazonDynamoDBClient)
        {
            ddbClient = amazonDynamoDBClient;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                await PutItemAsync();
                var user = await GetItemAsync();
                await DeleteItemAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Error putting item. {e.Message}");
            }
        }

        public async Task PutItemAsync()
        {
            var item = new Dictionary<string, AttributeValue>
                    {
                        { "Id", new AttributeValue { S = "normj@foo.com" } },
                        { "FirstName", new AttributeValue { S = "Norm" } },
                        { "LastName", new AttributeValue { S = "Johanson" } },
                        { "Address", new AttributeValue { S = "Seattle Washington" } },
                        { "Active", new AttributeValue { BOOL = true } },
                        { "NumberOfChildren", new AttributeValue { N = "2" } },
                        { "Interests", new AttributeValue
                            {
                                L = new List<AttributeValue>
                                {
                                    new AttributeValue { S = "Hiking" },
                                    new AttributeValue { S = "Running" },
                                    new AttributeValue { S = "Learning new tech" },
                                    new AttributeValue { S = "Video Games" }
                                }
                            }
                        },
                        { "Skills", new AttributeValue
                            {
                                M = new Dictionary<string, AttributeValue>
                                {
                                    { "C#", new AttributeValue { N = "7" } },
                                    { "PowerShell", new AttributeValue { N = "5" } },
                                    { "F#", new AttributeValue { N = "2" } },
                                    { "Java", new AttributeValue { N = "3" } },
                                    { "Spelling", new AttributeValue { N = "1" } }
                                }
                            }
                        }
                    };

            var putItemRequest = new PutItemRequest
            {
                TableName = "Users",
                Item = item
            };

            await ddbClient.PutItemAsync(putItemRequest);
            Console.WriteLine("User saved.");
        }

        public async Task<Dictionary<string, AttributeValue>> GetItemAsync()
        {
            var getItemRequest = new GetItemRequest
            {
                TableName = "Users",
                ConsistentRead = true,
                Key = new Dictionary<string, AttributeValue>
                    {
                        { "Id", new AttributeValue { S = "normj@foo.com" } }
                    }
            };

            var user = (await ddbClient.GetItemAsync(getItemRequest)).Item;
            Console.WriteLine("Reading user...");

            return user;
        }

        public async Task DeleteItemAsync()
        {
            var deleteRequest = new DeleteItemRequest
            {
                TableName = "Users",
                Key = new Dictionary<string, AttributeValue>
                    {
                        { "Id", new AttributeValue { S = "normj@foo.com" } }
                    }
            };

            await ddbClient.DeleteItemAsync(deleteRequest);
            Console.WriteLine("User deleted");
        }
    }
}
