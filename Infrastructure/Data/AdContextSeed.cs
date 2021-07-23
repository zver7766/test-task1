using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class AdContextSeed
    {
        public static async Task SeedAsync(AdContext context, ILoggerFactory loggerFactory)
        {
            try
            {

                if (!context.Categories.Any())
                {
                    var categoriesData =
                        await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/categories.json");

                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                    if (categories != null)
                    {
                        foreach (var item in categories)
                        {
                            await context.Categories.AddAsync(item);
                        }

                        await context.SaveChangesAsync();
                    }
                }

                if (!context.Advertisements.Any())
                {
                    var advertisementsData = 
                        await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/advertisements.json");

                    var advertisements = JsonSerializer.Deserialize<List<Advertisement>>(advertisementsData);

                    if (advertisements != null)
                    {
                        foreach (var item in advertisements)
                        {
                            await context.Advertisements.AddAsync(item);
                        }
                    
                        await context.SaveChangesAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AdContextSeed>();
                logger.LogError(ex.Message, "Exception on AdContextSeed");
            }
        }
    }
}
