﻿using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDB.APIs.Demo
{
    public class DataModelSample
    {
        // private readonly IAmazonDynamoDB _amazonDynamoDB;
        private readonly User _user;
        // private readonly DynamoDBContextConfig _config;
        private readonly DynamoDBContext _context;
        public DataModelSample(/*IAmazonDynamoDB amazonDynamoDB, DynamoDBContextConfig config, */DynamoDBContext context, User user)
        {
            _context = context;
            _user = user;
            /*_amazonDynamoDB = amazonDynamoDB;
            _config = config;*/
        }

        public async Task ExecuteAsync()
        {
            await _context.SaveAsync(_user);
            Console.WriteLine("User saved");

            var loadedUser = await _context.LoadAsync<User>("normj@foo.com");
            Console.WriteLine("User loaded");

            await _context.DeleteAsync<User>("normj@foo.com");
        }
    }
}
