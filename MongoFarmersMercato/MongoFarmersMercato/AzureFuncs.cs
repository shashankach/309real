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
using System.Xml.Linq;

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

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonElement("image")]
        public string image { get; set; }
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

        [FunctionName("add-to-cart")]
        public static async Task<IActionResult> AddToCart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "add-to-cart")] HttpRequest req)
        {
            try
            {
                string connectionString = "mongodb+srv://farmersmercatoadmin:3y1C4McLKhvGA3Ae@farmersmercato.fmitmu2.mongodb.net/test";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<Product>("cart");

                var userInput = await new StreamReader(req.Body).ReadToEndAsync();

                Product newProduct = JsonConvert.DeserializeObject<Product>(userInput);

                await collection.InsertOneAsync(newProduct);

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error adding to cart - " + e.Message);

            }

            return (ActionResult)new OkObjectResult("Successfully added to cart");
        }

        [FunctionName("empty-cart")]
        public static async Task<IActionResult> EmptyCart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "empty-cart")] HttpRequest req)
        {
            try
            {
                string connectionString = "mongodb+srv://farmersmercatoadmin:3y1C4McLKhvGA3Ae@farmersmercato.fmitmu2.mongodb.net/test";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<Product>("cart");

                await collection.DeleteManyAsync(Builders<Product>.Filter.Empty);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error emptying cart - " + e.Message);

            }

            return (ActionResult)new OkObjectResult("Successfully emptied cart");
        }

        [FunctionName("remove-item")]
        public static async Task<IActionResult> RemoveItem(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "remove-item-{name}-{seller}")] HttpRequest req, string name, string seller)
        {
            try
            {
                string connectionString = "mongodb+srv://farmersmercatoadmin:3y1C4McLKhvGA3Ae@farmersmercato.fmitmu2.mongodb.net/test";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<Product>("cart");

                await collection.DeleteOneAsync(Builders<Product>.Filter.Eq(p => p.name, name) &
                    Builders<Product>.Filter.Eq(p => p.seller, seller));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error removing product - " + e.Message);

            }

            return (ActionResult)new OkObjectResult("Successfully removed product");
        }

        [FunctionName("get-cart")]
        public static async Task<List<Product>> GetCart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-cart")] HttpRequest req)
        {
            try
            {
                string connectionString = "mongodb+srv://farmersmercatoadmin:3y1C4McLKhvGA3Ae@farmersmercato.fmitmu2.mongodb.net/test";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("MongoFarmersMercato");
                var collection = database.GetCollection<Product>("cart");

                var cart = await collection.Find(_ => true).ToListAsync();

                return cart;
            }
            catch (Exception)
            {
                return new List<Product>();

            }
        }
    }
}
