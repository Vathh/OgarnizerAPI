using Microsoft.AspNetCore.Identity;
using OgarnizerAPI.Entities;

namespace OgarnizerAPI
{
    public class OgarnizerSeeder
    {
        private readonly OgarnizerDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _config;

        public OgarnizerSeeder(OgarnizerDbContext dbContext, IPasswordHasher<User> passwordHasher, IConfiguration config)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _config = config;
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

                if (!_dbContext.Users.Any(x => x.Name == "admin"))
                {
                    var adminUser = AddAdminUser();
                    _dbContext.Users.Add(adminUser);
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
        private User AddAdminUser()
        {
            var adminUser = new User()
            {
                Name = "admin",
                Login = "admin",
                RoleId = 2
            };

            var hashedAdminPassword = _passwordHasher.HashPassword(adminUser, _config.GetValue<string>("AdminPassword"));

            adminUser.PasswordHash = hashedAdminPassword;           

            return adminUser;
        }
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
                    UserId = 2,
                    CreatedDate = new DateTime(2022,1,11),
                    Priority = 2,
                    Description = "Makao",
                    Place = "Pacanowo",
                    Object = "maszyna do lodoherbaty",
                    AdditionalInfo = "Pomieszalem harnolda",
                    UpdateInfo = "",
                    IsDone = true,
                    ClosedDate = new DateTime(2022,1,15),
                    CloseUserId = 1,
                    ToInvoice = true
                },
                new ClosedJob()
                {
                    UserId = 1,
                    CreatedDate = new DateTime(2022,2,11),
                    Priority = 3,
                    Description = "Ciasto z dziąsłem",
                    Place = "Piaskowa 15 suwalki",
                    Object = "piekarnik",
                    AdditionalInfo = "zrobic ciasto pyszne smakowite",
                    UpdateInfo = "",
                    IsDone = false,
                    ClosedDate = new DateTime(2022,11,2),
                    CloseUserId = 3,
                    ToInvoice = true
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
                    UserId = 1,
                    CreatedDate = new DateTime(2022,6,11),
                    Priority = 1,
                    Description = "Wymiana bębna Padma",
                    Place = "Padma 3.0 magazyn",
                    Object = "BIZHUB 223",
                    AdditionalInfo = "Brudzi, sprawdzic beben i transfer",
                    UpdateInfo = "",
                    ToInvoice = false
                },
                new Job()
                {
                    UserId = 2,
                    CreatedDate = new DateTime(2022,10,9),
                    Priority = 2,
                    Description = "Naprawa finiszera Krystyny",
                    Place = "Utrata 2c/50",
                    Object = "BIZHUB C368",
                    AdditionalInfo = "Finiszer wyrzuca blad C-0512",
                    UpdateInfo = "",
                    ToInvoice = false
                },
                new Job()
                {
                    UserId = 3,
                    CreatedDate = new DateTime(2022,2,11),
                    Priority = 3,
                    Description = "Lotto Augustów",
                    Place = "",
                    Object = "",
                    AdditionalInfo = "",
                    UpdateInfo = "",
                    ToInvoice = true
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
                    UserId = 1,
                    CreatedDate = new DateTime(2022,3,25),
                    Priority = 1,
                    Description = "Naprawa drukarki KRAM",
                    Object = "HP 2055",
                    AdditionalInfo = "zacina duplex",
                    UpdateInfo = "",
                    ToInvoice = false,
                    ForRelease = false
                },
                new Service()
                {
                    UserId = 2,
                    CreatedDate = new DateTime(2022,1,16),
                    Priority = 2,
                    Description = "Naprawa laptopa Mietka",
                    Object = "dell latitude 7490",
                    AdditionalInfo = "zepsuty wiatrak chyba bo trzeszczy",
                    UpdateInfo = "",
                    ToInvoice = false,
                    ForRelease = true
                },
                new Service()
                {
                    UserId = 3,
                    CreatedDate = new DateTime(2022,1,18),
                    Priority = 3,
                    Description = "Zamknac zlecenia lotto",
                    Object = "",
                    AdditionalInfo = "",
                    UpdateInfo = "",
                    ToInvoice = true,
                    ForRelease = true
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
                    UserId = 2,
                    CreatedDate = new DateTime(2022,2,15),
                    Priority = 2,
                    Description = "Zawiasy w laptopie Mariana",
                    Object = "dell 5450 SN123456789",
                    AdditionalInfo = "",
                    UpdateInfo = "",
                    IsDone = false,
                    ClosedDate = new DateTime(2022,3,15),
                    CloseUserId = 1,
                    ToInvoice = true,
                    ForRelease = true
                },
                new ClosedService()
                {
                    UserId = 3,
                    CreatedDate = new DateTime(2022,1,23),
                    Priority = 3,
                    Description = "Czyszczenie maszyny do klienta",
                    Object = "bizhub c458",
                    AdditionalInfo = "potestowac wszystkie szuflady i podajnik reczny",
                    UpdateInfo = "",
                    IsDone = true,
                    ClosedDate = new DateTime(2022,2,20),
                    CloseUserId = 2,
                    ToInvoice = true,
                    ForRelease = true
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
                    UserId = 2,
                    CreatedDate = new DateTime(2022,6,11),
                    Priority = 1,
                    Description = "Bateria do laptopa bogdana mariana",
                    Client = "bogdan marian",
                    Object = "hp eq2152nw SN12345561",
                    AdditionalInfo = "poszukac baterii, jak tansza niz 200 to zamowic",
                    UpdateInfo = "",
                    ToInvoice = false,
                    ForRelease = false
                },
                new Order()
                {
                    UserId = 1,
                    CreatedDate = new DateTime(2022,6,7),
                    Priority = 2,
                    Description = "toner do HP2055",
                    Client = "kram",
                    Object = "hp2055",
                    AdditionalInfo = "potrzebujemy 15 sztuk",
                    UpdateInfo = "",
                    ToInvoice = false,
                    ForRelease = true
                },
                new Order()
                {
                    UserId = 3,
                    CreatedDate = new DateTime(2022,6,22),
                    Priority = 3,
                    Description = "Toner TN312K",
                    Client = "andrzejek",
                    Object = "bizhub c258",
                    AdditionalInfo = "do pttk suwalki kosciuszki",
                    UpdateInfo = "",
                    ToInvoice = true,
                    ForRelease = true
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
                    UserId = 3,
                    CreatedDate = new DateTime(2022,6,22),
                    Priority = 2,
                    Description = "Ciasto z masłem",
                    Client = "tajny klient x",
                    Object = "komputer lenovo SN581823817 model jakotaki",
                    AdditionalInfo = "pyszne maslo",
                    UpdateInfo = "",
                    IsDone = true,
                    ClosedDate = new DateTime(2022,6,27),
                    CloseUserId = 4,
                    ToInvoice = true,
                    ForRelease = true
                },
                new ClosedOrder()
                {
                    UserId = 3,
                    CreatedDate = new DateTime(2022,6,23),
                    Priority = 3,
                    Description = "kartofle",
                    Client = "anna maria wesolowska",
                    Object = "babka ziemniaczana",
                    AdditionalInfo = "2kg kartofli na juz",
                    UpdateInfo = "",
                    IsDone = false,
                    ClosedDate = new DateTime(2022,6,24),
                    CloseUserId = 4,
                    ToInvoice = true,
                    ForRelease = true
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
