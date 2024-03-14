using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Projekt
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Sestavení připojovacího řetězce
            var consStringBuilder = new SqlConnectionStringBuilder
            {
                UserID = "koppel",
                Password = "20Alpha12",
                InitialCatalog = "koppel",
                DataSource = "193.85.203.188",
                ConnectTimeout = 30,
                  TrustServerCertificate = true
            };
            try
            {
                using (SqlConnection connection = new SqlConnection(consStringBuilder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Pripojeno");


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseSqlServer(consStringBuilder.ConnectionString); 
            var context = new MyDbContext(optionsBuilder.Options);

            await ImportProductsFromJson("appsettings.json", context); 
        }
       
        public static async Task ImportProductsFromJson(string jsonFilePath, MyDbContext context)
        {
            var jsonData = System.IO.File.ReadAllText(jsonFilePath);
            var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);

            if (products != null)
            {
                foreach (var product in products)
                {
                    context.Products.Add(product);
                }
                try
                {
                   
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    Console.Error.WriteLine(ex.InnerException?.Message);
                    // Consider logging the error to a file or database for further analysis
                    throw; // Re-throw the exception for now
                }

            }
        }

    }
}
