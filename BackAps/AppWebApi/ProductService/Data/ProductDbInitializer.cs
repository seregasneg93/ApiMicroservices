using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Data
{
    public class ProductDbInitializer
    {
        public static async Task InitAsync(ProductDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!await context.Products.AnyAsync())
            {
                var products = new List<Product>
            {
                new() { Name = "GeForce RTX 4060 Ti", Description = "Видеокарта 8 ГБ GDDR6", Price = 45990 },
                new() { Name = "Intel Core i7-13700K", Description = "16-ядерный процессор 13-го поколения", Price = 51990 },
                new() { Name = "Samsung 980 PRO 1TB", Description = "NVMe SSD, PCIe Gen4", Price = 10990 },
                new() { Name = "Corsair Vengeance 16GB", Description = "DDR5 6000 MHz", Price = 7990 },
                new() { Name = "MSI MAG B760 TOMAHAWK", Description = "Материнская плата LGA1700", Price = 17490 },
                new() { Name = "be quiet! Pure Power 11", Description = "Блок питания 700W", Price = 6490 },
                new() { Name = "Deepcool AK620", Description = "Процессорный кулер", Price = 4990 },
                new() { Name = "NZXT H510", Description = "Корпус Mid Tower", Price = 5990 },
                new() { Name = "Logitech G Pro X", Description = "Игровая гарнитура", Price = 8990 },
                new() { Name = "ASUS TUF Gaming VG27AQ", Description = "27\" 2K монитор, 165 Гц", Price = 23990 }
            };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
