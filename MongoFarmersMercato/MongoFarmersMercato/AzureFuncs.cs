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
using System.Linq;

namespace MongoFarmersMercato
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonElement("username")]
        public string username { get; set; }

        [BsonElement("password")]
        public string password { get; set; }
    }

    public static class AzureFuncs
    {
        [FunctionName("sign-up")]
        public static async Task<IActionResult> SignUp(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sign-up")] HttpRequest req)
        {
            try
            {
                string connectionString = "mongodb+srv://farmersmercatoadmin:3y1C4McLKhvGA3Ae@farmersmercato.fmitmu2.mongodb.net/test";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<User>("users");

                var userInput = await new StreamReader(req.Body).ReadToEndAsync();

                User newUser = JsonConvert.DeserializeObject<User>(userInput);

                await collection.InsertOneAsync(newUser);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error adding user - " + e.Message);

            }

            return (ActionResult)new OkObjectResult("Successfully added user");
        }
    
        [FunctionName("log-in")]
        public static async Task<List<User>> LogIn(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "log-in")] HttpRequest req)
        {
            try
            {
                string connectionString = "mongodb+srv://farmersmercatoadmin:3y1C4McLKhvGA3Ae@farmersmercato.fmitmu2.mongodb.net/test";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<User>("users");

                var accounts = await collection.Find(_ => true).ToListAsync();

                return accounts;
            }
            catch (Exception)
            {
                return new List<User>();

            }
        }
    }
}
