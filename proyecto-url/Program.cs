using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using proyecto_url.Data.Implementations;
using proyecto_url.Data.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;


namespace proyecto_url
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
            builder.Services.AddSwaggerGen(setupAction =>
            {
                setupAction.AddSecurityDefinition("Proyecto-urlBearerAuth", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "Ac� pegar el token generado al loguearse."
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Proyecto-urlBearerAuth" } //Tiene que coincidir con el id seteado arriba en la definici�n
                }, new List<string>() }
    });
            });



            builder.Services.AddDbContext<UrlShorterContext>(dbContextOptions => dbContextOptions.UseSqlite(
    builder.Configuration["ConnectionStrings:proyecto-urlDBConnectionString"])); //inyecci�n de dependencia





            builder.Services.AddAuthentication("Bearer") //"Bearer" es el tipo de auntenticaci�n que tenemos que elegir despu�s en PostMan para pasarle el token
                        .AddJwtBearer(options => //Ac� definimos la configuraci�n de la autenticaci�n. le decimos qu� cosas queremos comprobar. La fecha de expiraci�n se valida por defecto.
                        {
                            options.TokenValidationParameters = new()
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = builder.Configuration["Authentication:Issuer"],
                                ValidAudience = builder.Configuration["Authentication:Audience"],
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                            };
                        }
                    );




            #region DependencyInjections
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUrlService, UrlService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            #endregion

            var app = builder.Build();

            app.UseCors(options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}