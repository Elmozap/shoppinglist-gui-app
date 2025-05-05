using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassLibrary6
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            //file path to where you will save the data
            string filePath = "C:\\Users\\Desire\\Documents\\IPT VS Code\\ClassLibrary6\\ClassLibrary6\\jsonfiles\\productsoutput.json";
  

           //Create product objects
            var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 800.00 },
            new Product { Id = 2, Name = "Phone", Price = 500.00 }
        };

            // Write the data to a JSON file
            await WriteJsonToFileAsync(filePath, products);

            // Read the data back from the JSON file
            var loadedProducts = await ReadJsonFromFileAsync(filePath);

            // Display the data
            foreach (var product in loadedProducts)
            {
                Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}");
            }
        }

        // Method to write data to a JSON file
        static async Task    WriteJsonToFileAsync(string filePath, List<Product> products)
        {
            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }

        // Method to read data from a JSON file
        static async Task<List<Product>> ReadJsonFromFileAsync(string filePath)
        {
            string jsonread = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Product>>(jsonread);
        }
    }
}

