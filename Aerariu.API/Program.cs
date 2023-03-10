
using Aerariu.API.Lib.Mappings;
using Aerariu.API.Lib.Middleware;
using Aerariu.Core;
using Aerariu.Persistence;
using Aerariu.Utils.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace Aerariu.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter the token in the text input below."
                });
            });

            builder.Services.AddAutoMapper(typeof(AuthProfile));

            ConfigureDbContext(builder);

            ConfigureAuth(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
            });

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureDbContext(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AerariuDbContext>(options =>
                            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void ConfigureAuth(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    ValidAudience = builder.Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Token:key"])),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero

                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthHelper.Policy.RequiresAdmin, p => p.RequireRole(AuthHelper.Role.Administrator));
                options.AddPolicy(AuthHelper.Policy.RequiresUser, p => p.RequireRole(AuthHelper.Role.User));
            });
        }
    }
}