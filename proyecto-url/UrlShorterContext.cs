using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using proyecto_url.Controllers;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Options;
using proyecto_url.Entities;

namespace proyecto_url
{
    public class UrlShorterContext : DbContext //hereda DbContext que se instalo cuando con la paquetería de entityFramework
    {
        public DbSet<Url> Urls { get; set; } //especificamos las tres tablas
        public DbSet<Category> Categories { get; set; }
        public DbSet <User> Users { get; set; }
        public UrlShorterContext(DbContextOptions<UrlShorterContext> options) : base(options) // este es el constructuor que recibe un conjunto de opciones que son del tipo DbContext que van a tener la conexión
        //con base(options) le pasamos al padre la conexión que hicimos
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //QUe el usuario guarde las url
            modelBuilder.Entity<Url>()
                 .HasOne(url => url.User)
                 .WithMany()
                 .HasForeignKey(url => url.UserId); //que atributo me permite relacionarlo

            modelBuilder.Entity<Url>()
                .HasOne(url => url.Category)
                .WithMany()
                .HasForeignKey(url => url.CategoryId);

            User Mabel = new User()
            {
                Id = 2,
                Name = "Mabel Enrique",
                Email = "mabelucita@gmail.com",
                Password = "lamismadesiempre",
                UserName = "mabeluci",
            };

            modelBuilder.Entity<User>().HasData(
               Mabel);

            base.OnModelCreating(modelBuilder);
        }
    }
}


/*UrlShorterContext: Es una clase que actúa como el contexto de la base de datos. El contexto de la base de datos
 * es una parte esencial de Entity Framework Core y se utiliza para interactuar con la base de datos. Contiene
 * propiedades que representan tablas en la base de datos y métodos para realizar operaciones de consulta y escritura
 * en esas tablas.
 * public UrlShorterContext(DbContextOptions<UrlShorterContext> options) : base(options): Este es el constructor 
 * de la clase UrlShorterContext. Recibe una instancia de DbContextOptions<UrlShorterContext> como parámetro, que
 * proporciona información sobre la configuración de la base de datos, como la cadena de conexión. El constructor
 * llama al constructor de la clase base DbContext pasando esos parámetros.
 * public DbSet<XYZController> URLItems { get; set; }: Esto define una propiedad llamada URLItems que se utiliza
 * para acceder a la tabla correspondiente en la base de datos. La propiedad DbSet es un conjunto de entidades que
 * representan filas en la tabla. La entidad XYZController se asocia con esta tabla. XYZController debería ser la
 * entidad que representa los datos que deseas almacenar en la tabla de la base de datos.
 * El propósito principal de esta clase es proporcionar un punto de entrada para interactuar con la base de datos. 
 * Puedes utilizar instancias de UrlShorterContext para realizar operaciones como agregar, actualizar, eliminar
 * y consultar datos en la base de datos. */
