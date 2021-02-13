using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIEvol.Shared.Entities;

namespace TIEvol.Server.Data
{
    public class Seeder
    {
        // La base de datos.
        private readonly ApplicationDataContext _dataContext;

        // Constructor de la clase.
        public Seeder(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Este metodo permite poblar la base de datos.
        /// </summary>
        public async Task SeedAsync()
        {
            // Verificar que la base de datos exista.
            await _dataContext.Database.EnsureCreatedAsync();

            // Verificar si existen los registros
            await CheckComunasAsync();
            await CheckCiudades();
        }

        public async Task CheckComunasAsync() 
        {
            // Verificar si no existen las comunas.
            if (!_dataContext.Comunas.Any()) 
            {
                // Agregar data.
                // Para Santiago
                _dataContext.Comunas.Add(new Comuna
                {
                    Codigo = "A1-2",
                    Nombre = "La Florida",
                    Ciudad = new Ciudad {
                        Nombre = "Santiago",
                    }
                });

                _dataContext.Comunas.Add(new Comuna
                {
                    Codigo = "A1-3",
                    Nombre = "Providencia",
                    Ciudad = new Ciudad
                    {
                        Nombre = "Santiago",
                    }
                });

                _dataContext.Comunas.Add(new Comuna
                {
                    Codigo = "A1-5",
                    Nombre = "San Miguel",
                    Ciudad = new Ciudad
                    {
                        Nombre = "Santiago",
                    }
                });

                // Para Antofagasta
                _dataContext.Comunas.Add(new Comuna
                {
                    Codigo = "B2-4",
                    Nombre = "Mejillones",
                    Ciudad = new Ciudad
                    {
                        Nombre = "Antofagasta",
                    }
                });

                _dataContext.Comunas.Add(new Comuna
                {
                    Codigo = "J2-4",
                    Nombre = "Taltal",
                    Ciudad = new Ciudad
                    {
                        Nombre = "Antofagasta",
                    }
                });
            }

            // Guardar cambios.
            await _dataContext.SaveChangesAsync();
        }

        public async Task CheckCiudades()
        {
            if (!_dataContext.Ciudades.Any())
            {
                _dataContext.Ciudades.Add(new Ciudad
                {
                    Nombre = "Antofagasta"
                });

                _dataContext.Ciudades.Add(new Ciudad
                {
                    Nombre = "Santiago"
                });
            }

            await _dataContext.SaveChangesAsync();
        }

    }
}
