using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shelter.Core.Entities;
using Shelter.Core.Enums;
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
            SeedInitialDataEmployees(context);
            SeedInitialDataVolunteers(context);
            SeedInitialDataAnimals(context);
            SeedInitialDataSponsorships(context);
            SeedInitialDataTreatments(context);
            SeedInitialDateTransactions(context);
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

        private static void SeedInitialDataEmployees(ShelterDbContext context)
        {
            var data = GoogleService.ReadEntries(Constants.SheetEmployees);

            if (data != null)
            {
                if (!context.Employees.Any())
                {
                    var employees = new List<Employee>();
                    var first = true;

                    foreach (var row in data)
                    {
                        if (first)
                        {
                            first = false;
                            continue;
                        }

                        employees.Add(new Employee
                        {
                            Name = row[0].ToString(),
                            Surname = row[1].ToString(),
                            PhoneNumber = row[2].ToString(),
                            BirthDate = DateTime.ParseExact(row[3].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Active = row[4].Equals("1"),
                            Position = Enum.Parse<Position>(row[5].ToString())
                        });
                    }

                    context.Employees.AddRange(employees);
                    context.SaveChanges();
                }
            }
        }

        private static void SeedInitialDataVolunteers(ShelterDbContext context)
        {
            var data = GoogleService.ReadEntries(Constants.SheetVolunteers);

            if (data != null)
            {
                if (!context.Volunteers.Any())
                {
                    var volunteers = new List<Volunteer>();
                    var first = true;

                    foreach (var row in data)
                    {
                        if (first)
                        {
                            first = false;
                            continue;
                        }

                        volunteers.Add(new Volunteer
                        {
                            Name = row[0].ToString(),
                            Surname = row[1].ToString(),
                            PhoneNumber = row[2].ToString(),
                            BirthDate = DateTime.ParseExact(row[3].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            HelpKind = Enum.Parse<HelpKind>(row[4].ToString())
                        });
                    }

                    context.Volunteers.AddRange(volunteers);
                    context.SaveChanges();
                }
            }
        }

        private static void SeedInitialDataAnimals(ShelterDbContext context)
        {
            var data = GoogleService.ReadEntries(Constants.SheetAnimals);

            if (data != null)
            {
                if (!context.Animals.Any())
                {
                    var animals = new List<Animal>();
                    var first = true;

                    foreach (var row in data)
                    {
                        if (first)
                        {
                            first = false;
                            continue;
                        }

                        int? customerId = Convert.ToInt32(row[6]);

                        if (customerId.Equals(0))
                        {
                            customerId = null;
                        }

                        animals.Add(new Animal
                        {
                            Name = row[0].ToString(),
                            Age = Convert.ToInt32(row[1]),
                            Adopted = row[2].Equals("1"),
                            DateOfArrival = DateTime.ParseExact(row[3].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Status = row[4].Equals("1"), 
                            Feature = Enum.Parse<Feature>(row[5].ToString()),
                            Race = Enum.Parse<Race>(row[6].ToString()),
                            CustomerId = customerId
                        });
                    }

                    context.Animals.AddRange(animals);
                    context.SaveChanges();
                }
            }
        }

        private static void SeedInitialDataSponsorships(ShelterDbContext context)
        {
            var data = GoogleService.ReadEntries(Constants.SheetSponsorships);

            if (data != null)
            {
                if (!context.Sponsorships.Any())
                {
                    var sponsorships = new List<Sponsorship>();
                    var transactions = new List<Transaction>();
                    var first = true;

                    foreach (var row in data)
                    {
                        if (first)
                        {
                            first = false;
                            continue;
                        }

                        sponsorships.Add(new Sponsorship
                        {
                            Date = DateTime.ParseExact(row[0].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Cost = Convert.ToDouble(row[1]),
                            VolunteerId = Convert.ToInt32(row[2])
                        });

                        transactions.Add(new Transaction
                        {
                            Date = DateTime.ParseExact(row[0].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Cost = Convert.ToDouble(row[1]),
                            Type = TransactionType.Sponsoring
                        });
                    }

                    context.Sponsorships.AddRange(sponsorships);
                    context.Transactions.AddRange(transactions);
                    context.SaveChanges();
                }
            }
        }

        private static void SeedInitialDataTreatments(ShelterDbContext context)
        {
            var data = GoogleService.ReadEntries(Constants.SheetTreatments);

            if (data != null)
            {
                if (!context.Treatments.Any())
                {
                    var treatments = new List<Treatment>();
                    var transactions = new List<Transaction>();
                    var first = true;

                    foreach (var row in data)
                    {
                        if (first)
                        {
                            first = false;
                            continue;
                        }

                        treatments.Add(new Treatment()
                        {
                            Date = DateTime.ParseExact(row[0].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Cost = Convert.ToDouble(row[1]),
                            AnimalId = Convert.ToInt32(row[2]),
                            EmployeeId = Convert.ToInt32(row[3]),
                            Disease = Enum.Parse<Disease>(row[4].ToString())
                        });

                        transactions.Add(new Transaction
                        {
                            Date = DateTime.ParseExact(row[0].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Cost = Convert.ToDouble(row[1])*(-1),
                            Type = TransactionType.Treatment
                        });
                    }

                    context.Treatments.AddRange(treatments);
                    context.Transactions.AddRange(transactions);
                    context.SaveChanges();
                }
            }
        }

        private static void SeedInitialDateTransactions(ShelterDbContext context)
        {
            var data = GoogleService.ReadEntries(Constants.SheetTransactions);

            if (data != null)
            {
                var transactions = new List<Transaction>();
                var first = true;

                foreach (var row in data)
                {
                    if (first)
                    {
                        first = false;
                        continue;
                    }

                    transactions.Add(new Transaction
                    {
                        Date = DateTime.ParseExact(row[0].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Cost = Convert.ToDouble(row[1]) * (-1),
                        Type = TransactionType.Expenses
                    });

                    transactions.Add(new Transaction
                    {
                        Date = DateTime.ParseExact(row[0].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Cost = Convert.ToDouble(row[2]),
                        Type = TransactionType.Budget
                    });

                    var salaries = context.Employees.Where(x => x.Active == true).Select(x => x.Position);

                    foreach (var salary in salaries)
                    {
                        transactions.Add(new Transaction
                        {
                            Date = DateTime.ParseExact(row[0].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Cost = (int) Enum.Parse<Salaries>(salary.ToString()) * (-1),
                            Type = TransactionType.Salary
                        });
                    }
                }

                context.Transactions.AddRange(transactions);
                context.SaveChanges();
            }
        }
    }
}
