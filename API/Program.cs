using API;
using System.Text;
using API.Database;
using API.Services;
using API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using LogClient;
using LogClient.Types;
using API.UnitOfWork;
using API.Biz.Interfaces;
using API.Biz.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put Bearer + your token in the box below",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<string>()
        }
    });
});

string connectionString = null;
#if DEBUG
    connectionString = builder.Configuration.GetConnectionString("DevelopmentDbConnection");
#else
    connectionString = builder.Configuration.GetConnectionString("ProductionDbConnection");
#endif

builder.Services.AddDbContext<TestDbContext>(opt =>
{
    opt.UseSqlServer(connectionString);
});

builder.Services.AddCors();
builder.Services.AddIdentityCore<User>(opt =>
    {
        opt.Password.RequireUppercase = true;
        opt.Password.RequiredLength = 6;
        opt.Password.RequireLowercase = true;
        opt.User.RequireUniqueEmail = true;
        opt.Password.RequireDigit = true;
        opt.Password.RequireNonAlphanumeric = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TestDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:TokenKey"]))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenService>();

string host = null;
#if DEBUG
    host = builder.Configuration["DevLogHost"];
#else
    host = builder.Configuration["ProdLogHost"];
#endif
builder.Services.AddSingleton<LogClient.ILogger>(
    new WebLogger(host, Product.Tester, LayerType.BackEnd));

builder.Services.AddSingleton<LogClient.ITracer>(
    new WebTracer(host, Product.Tester));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork<TestDbContext>>();
builder.Services.AddTransient<IUserProfile, UserProfileService>();
builder.Services.AddTransient<AppCacheService>();
builder.Services.AddMemoryCache();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
    });
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .WithOrigins([
            "http://localhost:3004",
            "http://127.0.0.1:3004",
            "http://askold-001-site1.atempurl.com",
            "http://askold-001-site2.atempurl.com",
            "http://quiz-it.online"
        ]);
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<TestDbContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    context.Database.Migrate();
    DbInitializer.Initialize(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "A problem occurred during migrations.");
}

app.Run();
