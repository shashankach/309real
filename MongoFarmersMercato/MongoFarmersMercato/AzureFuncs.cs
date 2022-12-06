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
using Microsoft.AspNetCore.Routing;

namespace MongoFarmersMercato
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonElement("username")]
        public string username { get; set; }

        [BsonElement("password")]
        public string password { get; set; }

        [BsonElement("farmer")]
        public bool farmer { get; set; }

        [BsonElement("cart")]
        public List<Product> cart { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonElement("image")]
        public string image { get; set; }

        [BsonElement("loggedIn")]
        public bool loggedIn { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("price")]
        public Double price { get; set; }

        [BsonElement("seller")]
        public string seller { get; set; }

        [BsonElement("image")]
        public string image { get; set; }
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

        [FunctionName("search-farmers")]
        public static async Task<List<User>> SearchFarmers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "search-farmers")] HttpRequest req)
        {
            try
            {
                string connectionString = "mongodb+srv://farmersmercatoadmin:3y1C4McLKhvGA3Ae@farmersmercato.fmitmu2.mongodb.net/test";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<User>("users");

                var accounts = await collection.Find(acct => acct.farmer == true).ToListAsync();

                return accounts;
            }
            catch (Exception)
            {
                return new List<User>();

            }
        }

        [FunctionName("search-products")]
        public static async Task<List<Product>> SearchProducts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "search-products")] HttpRequest req)
        {
            try
            {
                string connectionString = "mongodb+srv://farmersmercatoadmin:3y1C4McLKhvGA3Ae@farmersmercato.fmitmu2.mongodb.net/test";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<Product>("products");

                var products = await collection.Find(_ => true).ToListAsync();

                return products;
            }
            catch (Exception)
            {
                return new List<Product>();

            }
        }

        [FunctionName("update-inventory")]
        public static async Task<IActionResult> UpdateInventory(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "update-inventory")] HttpRequest req)
        {
            try
            {
                string connectionString = "mongodb+srv://farmersmercatoadmin:3y1C4McLKhvGA3Ae@farmersmercato.fmitmu2.mongodb.net/test";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<Product>("products");

                var userInput = await new StreamReader(req.Body).ReadToEndAsync();

                Product newProduct = JsonConvert.DeserializeObject<Product>(userInput);

                await collection.InsertOneAsync(newProduct);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error adding product - " + e.Message);

            }

            return (ActionResult)new OkObjectResult("Successfully added product");
        }

        [FunctionName("remove-product")]
        public static async Task<IActionResult> RemoveProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "remove-product-{name}-{seller}")] HttpRequest req, string name, string seller)
        {
            try
            {
                string connectionString = "mongodb+srv://farmersmercatoadmin:3y1C4McLKhvGA3Ae@farmersmercato.fmitmu2.mongodb.net/test";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<Product>("products");

                await collection.DeleteOneAsync(Builders<Product>.Filter.Eq(p => p.name, name) &
                    Builders<Product>.Filter.Eq(p => p.seller, seller));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error removing product - " + e.Message);

            }

            return (ActionResult)new OkObjectResult("Successfully removed product");
        }

        /*[FunctionName("add-to-cart")]
        public static async Task<IActionResult> AddToCart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "add-to-cart-{username}-{cart}")] HttpRequest req, string username, List<Product> cart)
        {
            try
            {
                string connectionString = "mongodb+srv://farmersmercatoadmin:3y1C4McLKhvGA3Ae@farmersmercato.fmitmu2.mongodb.net/test";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<User>("users");

                var userInput = await new StreamReader(req.Body).ReadToEndAsync();

                Product product = JsonConvert.DeserializeObject<Product>(userInput);

                cart.Add(product);

                await collection.UpdateOneAsync(Builders<User>.Filter.Eq("username", username), Builders<User>.Update.Set("cart", cart));

                var products = await collection.Find(_ => true).ToListAsync();

                return (ActionResult)new OkObjectResult(products);

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error adding to cart - " + e.Message);

            }

            return (ActionResult)new OkObjectResult("Successfully added to cart");
        }*/

        /*[FunctionName("update-loggedIn")]
        public static async Task<IActionResult> UpdateLoggedIn(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "update-loggedIn-{username}")] HttpRequest req, string username)
        {
            try
            {
                string connectionString = "mongodb+srv://farmersmercatoadmin:3y1C4McLKhvGA3Ae@farmersmercato.fmitmu2.mongodb.net/test";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<User>("users");

                var userInput = await new StreamReader(req.Body).ReadToEndAsync();

                await collection.UpdateOneAsync(Builders<User>.Filter.Eq("username", username), Builders<User>.Update.Set("loggedIn", userInput));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error updating loggedIn - " + e.Message);

            }

            return (ActionResult)new OkObjectResult("Successfully updated loggedIn");
        }*/
    }
}
