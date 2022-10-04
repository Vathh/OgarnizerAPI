using OgarnizerAPI.Entities;

namespace OgarnizerAPI
{
    public class OgarnizerSeeder
    {
        private readonly OgarnizerDbContext _dbContext;

        public OgarnizerSeeder(OgarnizerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Users.Any())
                {
                    var users = GetUsers();
                    _dbContext.Users.AddRange(users);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Jobs.Any())
                {
                    var jobs = GetJobs();
                    _dbContext.Jobs.AddRange(jobs);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.ClosedJobs.Any())
                {
                    var closedJobs = GetClosedJobs();
                    _dbContext.ClosedJobs.AddRange(closedJobs);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Services.Any())
                {
                    var services = GetServices();
                    _dbContext.Services.AddRange(services);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.ClosedServices.Any())
                {
                    var closedServices = GetClosedServices();
                    _dbContext.ClosedServices.AddRange(closedServices);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Orders.Any())
                {
                    var orders = GetOrders();
                    _dbContext.Orders.AddRange(orders);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.ClosedOrders.Any())
                {
                    var closedOrders = GetClosedOrders();
                    _dbContext.ClosedOrders.AddRange(closedOrders);
                    _dbContext.SaveChanges();
                }
#pragma warning restore CS8604 // Possible null reference argument.
            }
        }
#pragma warning disable CA1822 // Mark members as static
        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Name = "Arek",
                    Login = "Arek123",
                    PasswordHash = "Arek123",
                    RoleId = 1
                },
                new User()
                {
                    Name = "Przemek",
                    Login = "Przemek123",
                    PasswordHash = "Przemek123",
                    RoleId = 1
                },
                new User()
                {
                    Name = "Maciej",
                    Login = "Maciej123",
                    PasswordHash = "Maciej123",
                    RoleId = 2
                },
                new User()
                {
                    Name = "Kamila",
                    Login = "Kamila123",
                    PasswordHash = "Kamila123",
                    RoleId = 1
                }
            };

            return users;
        }

        private IEnumerable<ClosedJob> GetClosedJobs()

        {
            var closedJobs = new List<ClosedJob>()
            {
                new ClosedJob()
                {
                    UserId = 7,
                    CreatedDate = new DateTime(2022,1,11),
                    Priority = 2,
                    Description = "Makao",
                    Place = "Pacanowo",
                    Object = "maszyna do lodoherbaty",
                    AdditionalInfo = "Pomieszalem harnolda",
                    UpdateInfo = "",
                    IsDone = true,
                    ClosedDate = new DateTime(2022,1,15),
                    CloseUserId = 6
                },
                new ClosedJob()
                {
                    UserId = 5,
                    CreatedDate = new DateTime(2022,2,11),
                    Priority = 3,
                    Description = "Ciasto z dziąsłem",
                    Place = "Piaskowa 15 suwalki",
                    Object = "piekarnik",
                    AdditionalInfo = "zrobic ciasto pyszne smakowite",
                    UpdateInfo = "",
                    IsDone = false,
                    ClosedDate = new DateTime(2022,11,2),
                    CloseUserId = 7
                }
            };

            return closedJobs;
        }

        private IEnumerable<Job> GetJobs()
        {
            var jobs = new List<Job>()
            {
                new Job()
                {
                    UserId = 5,
                    CreatedDate = new DateTime(2022,6,11),
                    Priority = 1,
                    Description = "Wymiana bębna Padma",
                    Place = "Padma 3.0 magazyn",
                    Object = "BIZHUB 223",
                    AdditionalInfo = "Brudzi, sprawdzic beben i transfer",
                    UpdateInfo = ""
                },
                new Job()
                {
                    UserId = 6,
                    CreatedDate = new DateTime(2022,10,9),
                    Priority = 2,
                    Description = "Naprawa finiszera Krystyny",
                    Place = "Utrata 2c/50",
                    Object = "BIZHUB C368",
                    AdditionalInfo = "Finiszer wyrzuca blad C-0512",
                    UpdateInfo = ""
                },
                new Job()
                {
                    UserId = 7,
                    CreatedDate = new DateTime(2022,2,11),
                    Priority = 3,
                    Description = "Lotto Augustów",
                    Place = "",
                    Object = "",
                    AdditionalInfo = "",
                    UpdateInfo = ""
                }
            };

            return jobs;
        }

        private IEnumerable<Service> GetServices()
        {
            var services = new List<Service>()
            {
                new Service()
                {
                    UserId = 5,
                    CreatedDate = new DateTime(2022,3,25),
                    Priority = 1,
                    Description = "Naprawa drukarki KRAM",
                    Object = "HP 2055",
                    AdditionalInfo = "zacina duplex",
                    UpdateInfo = ""
                },
                new Service()
                {
                    UserId = 6,
                    CreatedDate = new DateTime(2022,1,16),
                    Priority = 2,
                    Description = "Naprawa laptopa Mietka",
                    Object = "dell latitude 7490",
                    AdditionalInfo = "zepsuty wiatrak chyba bo trzeszczy",
                    UpdateInfo = ""
                },
                new Service()
                {
                    UserId = 7,
                    CreatedDate = new DateTime(2022,1,18),
                    Priority = 3,
                    Description = "Zamknac zlecenia lotto",
                    Object = "",
                    AdditionalInfo = "",
                    UpdateInfo = ""
                }
            };

            return services;
        }

        private IEnumerable<ClosedService> GetClosedServices()
        {
            var closedServices = new List<ClosedService>()
            {
                new ClosedService()
                {
                    UserId = 6,
                    CreatedDate = new DateTime(2022,2,15),
                    Priority = 2,
                    Description = "Zawiasy w laptopie Mariana",
                    Object = "dell 5450 SN123456789",
                    AdditionalInfo = "",
                    UpdateInfo = "",
                    IsDone = false,
                    ClosedDate = new DateTime(2022,3,15),
                    CloseUserId = 5
                },
                new ClosedService()
                {
                    UserId = 7,
                    CreatedDate = new DateTime(2022,1,23),
                    Priority = 3,
                    Description = "Czyszczenie maszyny do klienta",
                    Object = "bizhub c458",
                    AdditionalInfo = "potestowac wszystkie szuflady i podajnik reczny",
                    UpdateInfo = "",
                    IsDone = true,
                    ClosedDate = new DateTime(2022,2,20),
                    CloseUserId = 6
                }
            };

            return closedServices;
        }

        private IEnumerable<Order> GetOrders()
        {
            var orders = new List<Order>()
            {
                new Order()
                {
                    UserId = 6,
                    CreatedDate = new DateTime(2022,6,11),
                    Priority = 1,
                    Description = "Bateria do laptopa bogdana mariana",
                    Client = "bogdan marian",
                    Object = "hp eq2152nw SN12345561",
                    AdditionalInfo = "poszukac baterii, jak tansza niz 200 to zamowic",
                    UpdateInfo = ""
                },
                new Order()
                {
                    UserId = 5,
                    CreatedDate = new DateTime(2022,6,7),
                    Priority = 2,
                    Description = "toner do HP2055",
                    Client = "kram",
                    Object = "hp2055",
                    AdditionalInfo = "potrzebujemy 15 sztuk",
                    UpdateInfo = ""
                },
                new Order()
                {
                    UserId = 7,
                    CreatedDate = new DateTime(2022,6,22),
                    Priority = 3,
                    Description = "Toner TN312K",
                    Client = "andrzejek",
                    Object = "bizhub c258",
                    AdditionalInfo = "do pttk suwalki kosciuszki",
                    UpdateInfo = ""
                }
            };

            return orders;
        }

        private IEnumerable<ClosedOrder> GetClosedOrders()
        {
            var closedOrders = new List<ClosedOrder>()
            {
                new ClosedOrder()
                {
                    UserId = 7,
                    CreatedDate = new DateTime(2022,6,22),
                    Priority = 2,
                    Description = "Ciasto z masłem",
                    Client = "tajny klient x",
                    Object = "komputer lenovo SN581823817 model jakotaki",
                    AdditionalInfo = "pyszne maslo",
                    UpdateInfo = "",
                    IsDone = true,
                    ClosedDate = new DateTime(2022,6,27),
                    CloseUserId = 8
                },
                new ClosedOrder()
                {
                    UserId = 7,
                    CreatedDate = new DateTime(2022,6,23),
                    Priority = 3,
                    Description = "kartofle",
                    Client = "anna maria wesolowska",
                    Object = "babka ziemniaczana",
                    AdditionalInfo = "2kg kartofli na juz",
                    UpdateInfo = "",
                    IsDone = false,
                    ClosedDate = new DateTime(2022,6,24),
                    CloseUserId = 8
                }
            };

            return closedOrders;
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>() {
                new Role()
                {
                    Name = "Manager"
                },
                new Role() {
                    Name = "Admin"
                }
            };

            return roles;
        }
    }
}

#pragma warning restore CA1822 // Mark members as static
