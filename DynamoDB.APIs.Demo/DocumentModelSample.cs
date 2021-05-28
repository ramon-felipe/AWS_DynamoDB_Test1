using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDB.APIs.Demo
{
    public static class DocumentModelSample
    {
        public static async Task ExecuteAsync()
        {
            using IAmazonDynamoDB ddbClient = new AmazonDynamoDBClient();
            Table userTable = Table.LoadTable(ddbClient, "Users", DynamoDBEntryConversion.V2);

            Document newUser = new Document();
            newUser["Id"] = "normj@foo.com";
            newUser["FirstName"] = "Norm";
            newUser["LastName"] = "Johanson";
            newUser["Address"] = "Seattle WA";
            newUser["Active"] = true;
            newUser["NumberOfChildren"] = 2;

            newUser["Interests"] = new List<string>
            {
                "Hiking", "Running", "Learning new tech", "Video Games"
            };

            Document skills = new Document
            {
                ["C#"] = 7,
                ["PowerShell"] = 5,
                ["F#"] = 2,
                ["Java"] = 3,
                ["Spelling"] = 1
            };

            newUser["Skills"] = skills;

            await userTable.PutItemAsync(newUser);
            Console.WriteLine("User saved");


            // Retrieve user
            Document loadedUser = await userTable.GetItemAsync("normj@foo.com");

            // Delete use
            await userTable.DeleteItemAsync("normj@foo.com");
        }
    }
}
