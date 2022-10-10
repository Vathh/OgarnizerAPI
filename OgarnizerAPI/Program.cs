using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using OgarnizerAPI;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Middleware;
using OgarnizerAPI.Models;
using OgarnizerAPI.Models.Validators;
using OgarnizerAPI.Services;
using System.Reflection;
using System.Text;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
    var authenticationSettings = new AuthenticationSettings();

    builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

    // Add services to the container.
    builder.Services.AddSingleton(authenticationSettings);
    builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = "Bearer";
        option.DefaultChallengeScheme = "Bearer";
        option.DefaultScheme = "Bearer";
    }).AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
#pragma warning disable CS8604 // Possible null reference argument.
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = authenticationSettings.JwtIssuer,
            ValidAudience = authenticationSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
        };
#pragma warning restore CS8604 // Possible null reference argument.
    });

    builder.Services.AddControllersWithViews();
    builder.Services.AddControllers();

    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();
    builder.Services.AddDbContext<OgarnizerDbContext>();
    builder.Services.AddScoped<OgarnizerSeeder>();
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

    builder.Services.AddScoped<IJobService, JobService>();
    builder.Services.AddScoped<IClosedJobService, ClosedJobService>();
    builder.Services.AddScoped<IServiceService, ServiceService>();
    builder.Services.AddScoped<IClosedServiceService, ClosedServiceService>();
    builder.Services.AddScoped<IAccountService, AccountService>();

    builder.Services.AddScoped<ErrorHandlingMiddleware>();
    builder.Services.AddScoped<RequestTimeMiddleware>();
    builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

    builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
    builder.Services.AddScoped<IValidator<OrderQuery>, JobQueryValidator>();
    builder.Services.AddScoped<IValidator<ClosedJobQuery>, ClosedJobQueryValidator>();
    builder.Services.AddScoped<IValidator<ServiceQuery>, ServiceQueryValidator>();
    builder.Services.AddScoped<IValidator<ClosedServiceQuery>, ClosedServiceQueryValidator>();

    builder.Services.AddScoped<IUserContextService, UserContextService>();
    builder.Services.AddHttpContextAccessor();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddSwaggerGen();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("FrontEndClient", builder =>
        {
            builder.AllowAnyMethod()
                .AllowAnyHeader()
                //.WithOrigins(Configuration["AllowedOrigins"]);
                .AllowAnyOrigin();
        });
    });

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<OgarnizerSeeder>();

    app.UseResponseCaching();
    app.UseStaticFiles();
    app.UseCors("FronEndClient");

    seeder.Seed();

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestTimeMiddleware>();

    app.UseAuthentication();
    app.UseHttpsRedirection();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ogarnizer API");
    });

    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}


