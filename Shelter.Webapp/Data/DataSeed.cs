using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shelter.Core.Entities;
using Shelter.Infrastructure.Data;
using Shelter.Webapp.Services;

namespace Shelter.Webapp.Data
{
    public static class DataSeed
    {
        public static void Initialize(IServiceProvider provider)
        {
            provider.GetRequiredService<ShelterDbContext>().Database.Migrate();

            var context = provider.GetRequiredService<ShelterDbContext>();

            GoogleService.GetConnection();
            SeedInitialDataCustomers(context);
        }

        private static void SeedInitialDataCustomers(ShelterDbContext context)
        {
            var data = GoogleService.ReadEntries(Constants.SheetCustomers);

            if (data != null)
            {
                if (!context.Customers.Any())
                {
                    var customers = new List<Customer>();
                    var first = true;

                    foreach (var row in data)
                    {
                        if (first)
                        {
                            first = false;
                            continue;
                        }

                        customers.Add(new Customer
                        {
                            Name = row[0].ToString(),
                            Surname = row[1].ToString(),
                            PhoneNumber = row[2].ToString(),
                            BirthDate = DateTime.ParseExact(row[3].ToString(),"dd/MM/yyyy", CultureInfo.InvariantCulture),
                        });
                    }

                    context.Customers.AddRange(customers);
                    context.SaveChanges();
                }
            }
        }
    }
}
