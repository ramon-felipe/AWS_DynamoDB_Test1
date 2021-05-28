using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamoDB.APIs.Demo
{
    [DynamoDBTable("Users")]
    public class User
    {
        [DynamoDBProperty("Id")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public int NumberOfChildren { get; set; }
        public List<string> Interests { get; set; }
        public Dictionary<string, int> Skills { get; set; }

        [DynamoDBIgnore] //won't be considered
        public string FullName
        {
            get { return FirstName + ", " + LastName; }
        }
    }
}
