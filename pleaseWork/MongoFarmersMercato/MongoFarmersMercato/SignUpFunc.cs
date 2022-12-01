using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoFarmersMercato
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("username")]
        public string username { get; set; }

        [BsonElement("password")]
        public string password { get; set; }

       /* [BsonElement("userType")]
        public string userType { get; set; }*/
    }

    public static class SignUpFunc
    {
        [FunctionName("sign-up")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var client = new MongoClient(System.Environment.GetEnvironmentVariable("MongoDBAtlasConnectionString"));
            var database = client.GetDatabase("MongoFarmersMercato");
            var collection = database.GetCollection<User>("users");

            string username = req.Query["username"];
            string password = req.Query["password"];
            /*string userType = req.Query["userType"];*/

            var newUser = new User
            {
                username = username,
                password = password
                /*userType = userType*/
            };

            await collection.InsertOneAsync(newUser);

            return new OkObjectResult("user added");
        }
        public static class LoginFunc
        {
            [FunctionName("log-in")]
            public static async Task<IActionResult> Run(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
                ILogger log)
            {
                var client = new MongoClient(System.Environment.GetEnvironmentVariable("MongoDBAtlasConnectionString"));
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<User>("users");

                string username = req.Query["username"];
                string password = req.Query["password"];
                /*string userType = req.Query["userType"];*/

                var acct = await collection.Find({username: username, password: password});

                if (acct)
                {
                    return new OkObjectResult("user exists");
                }
                else
                {
                    return new OkObjectResult("user does not exist");
                }
            }
        }
    }
}