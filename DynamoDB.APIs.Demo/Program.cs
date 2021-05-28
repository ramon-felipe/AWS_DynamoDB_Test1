using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;

namespace DynamoDB.APIs.Demo
{
    class Program
    {
        public static void Main(string[] args)
        {
            var amazonDBClient = new AmazonDynamoDBClient();

            Console.WriteLine("Low Level Sample");
            // LowLevelSample lowLevelSample = new LowLevelSample(amazonDBClient);
            // lowLevelSample.ExecuteAsync().Wait();
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Document Model Sample");
            //DocumentModelSample.ExecuteAsync().Wait();
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Data Model Sample");
            var user = new User
            {
                Email = "normj@foo.com",
                FirstName = "Norm",
                LastName = "Johanson",
                Address = "Seattle Washington",
                Active = true,
                NumberOfChildren = 2,
                Interests = new List<string>
                {
                    "Hiking", "Running", "Learning new Tech", "Video Games"
                },
                Skills = new Dictionary<string, int>
                {
                    { "C#", 7 },
                    { "PowerShell", 5 },
                    { "F#", 2 },
                    { "Java", 3 },
                    { "Spelling", 1 }
                }
            };

            DynamoDBContextConfig config = new DynamoDBContextConfig
            {
                ConsistentRead = true,
                Conversion = DynamoDBEntryConversion.V2
            };
            using DynamoDBContext context = new DynamoDBContext(amazonDBClient, config);

            DataModelSample dataModelSample = new DataModelSample(context, user);
            dataModelSample.ExecuteAsync().Wait();
            Console.WriteLine(Environment.NewLine);
        }
    }
}
