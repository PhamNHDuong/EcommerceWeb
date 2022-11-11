using EcommerceWeb.API.Repositories;
using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.API.Utilities;
using EcommerceWeb.Data.DatabaseContext;
using EcommerceWeb.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;
using System.Xml.Schema;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore

);// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.ApiKey,
    //    Scheme = "Bearer",
    //    BearerFormat = "JWT",
    //    In = ParameterLocation.Header,
    //    Description = "JWT Authorization using the Bearer scheme."
    //});
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //            {
    //                {
    //                    new OpenApiSecurityScheme
    //                    {
    //                        Reference = new OpenApiReference
    //                        {
    //                            Type = ReferenceType.SecurityScheme,
    //                            Id = "Bearer"
    //                        }
    //                    },
    //                    new string[]{}
    //                }
    //            });
});

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy", opt =>
    {
        opt.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


//builder.Services.AddTransient<SeedData>();
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddDefaultTokenProviders()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = false;
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidAudience = builder.Configuration["JWT:ValidAudience"],
//        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
//    };
//});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ImageExtensions, ImageExtensions>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); ;
}

app.UseHttpsRedirection();

app.UseCors("OpenCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
