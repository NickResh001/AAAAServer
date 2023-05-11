using ASP_busstation.Models1;
using System.Linq;

namespace ASP_busstation.Data
{
    public static class BusstationContextSeed
    {
        /// <summary>
        /// Инициализирует базу данных начальными значениями.
        /// </summary>
        /// <param name="context">Контекс базы данных.</param>
        /// <returns></returns>
        public static async Task SeedAsync(AspbusstationContext context)
        {

            try
            {
                context.Database.EnsureCreated();

                if (!context.Regions.Any())
                {
                    var regions = new Region[]
                    {
                        new Region {Title = "Ивановская область"}
                    };

                    foreach (var region in regions)
                    {
                        context.Regions.Add(region);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Settlements.Any())
                {
                    var settlements = new Settlement[]
                    {
                        new Settlement {Title = "Иваново",  RegionFk = 1},
                        new Settlement {Title = "Вичуга",   RegionFk = 1},
                        new Settlement {Title = "Шуя",      RegionFk = 1},
                        new Settlement {Title = "Кинешма",  RegionFk = 1},
                        new Settlement {Title = "Юрьевец",  RegionFk = 1}
                    };
                    foreach (var settlement in settlements)
                    {
                        context.Settlements.Add(settlement);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch
            {
                throw;
            }
        }
        //public static void Initialize(AspbusstationContext context)
        //{
            
        //}
    }
}
