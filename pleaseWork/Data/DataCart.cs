using System;
namespace pleaseWork.Data;

public class DataCart
{
    private Item[] cart = {
        new Item("Apples", 2.99, "Bello Farms", "Some Apples", 5, "PUT_URL_HERE"),
        new Item("Apples", 2.99, "Bello Farms", "Some Apples", 5, "PUT_URL_HERE"),
        new Item("Bananas", 0.99, "Bello Farms", "Some Apples", 5, "PUT_URL_HERE"),
        new Item("Bananas", 0.99, "Bello Farms", "Some Apples", 5, "PUT_URL_HERE"),
    };

    public Task<Item[]> getCart()
    {
        return Task.FromResult(cart);
    }

    public Item[] getCart2()
    {
        return cart;
    }

    public class Item
    {
        public Item(string name, double price, string seller,
            string description, int rating, string imageURL)
        {
            this.name = name;
            this.price = price;
            this.seller = seller;
            this.description = description;
            this.rating = rating;
            this.imageURL = imageURL;
        }

        public string name { get; set; }
        public double price { get; set; }
        public string seller { get; set; }
        public string description { get; set; }
        public int rating { get; set; }
        public string imageURL { get; set; }
    }
}


