using Dummy.Domain.Classes;
using Dummy.Domain.Classes.BaseConfiguration;
using Dummy.Domain.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Domain
{
    internal class DummyContext : DbContext
    {
        public DummyContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //El código seleccionado es parte de la implementación de la clase AfterSalesContext en el archivo AfterSalesContext.cs.Esta clase es una subclase de DbContext y se utiliza para configurar y acceder a la base de datos en la aplicación.
            //En el método OnModelCreating, se realiza una configuración especial para aplicar todas las configuraciones de entidades de la aplicación. Esto se hace utilizando el método ApplyConfigurationsFromAssembly del objeto modelBuilder, que es una instancia de ModelBuilder y se utiliza para construir el modelo de la base de datos.
            //El método ApplyConfigurationsFromAssembly toma dos argumentos. El primer argumento es el ensamblado actualmente en ejecución, que se obtiene utilizando Assembly.GetExecutingAssembly().Esto asegura que todas las configuraciones de entidades definidas en el ensamblado actual se apliquen al modelo de la base de datos.
            //El segundo argumento es una función lambda que se utiliza para filtrar las configuraciones de entidades que se deben aplicar. En este caso, se utiliza la función t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) && typeof(BaseEntity).IsAssignableFrom(i.GenericTypeArguments[0])).Esta función verifica si una configuración de entidad implementa la interfaz genérica IEntityTypeConfiguration<> y si la entidad base BaseEntity es asignable desde el tipo genérico de la configuración. Si se cumple esta condición, la configuración se aplica al modelo de la base de datos.
            //En resumen, este código seleccionado se encarga de aplicar todas las configuraciones de entidades definidas en el ensamblado actual al modelo de la base de datos, asegurando que todas las entidades estén configuradas correctamente para su uso en la aplicación.

            modelBuilder.ApplyConfigurationsFromAssembly(
                                Assembly.GetExecutingAssembly(),
                                t => t.GetInterfaces().Any(i =>
                                i.IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                                typeof(BaseEntity).IsAssignableFrom(i.GenericTypeArguments[0])));

            base.OnModelCreating(modelBuilder);
        }

    }
}
