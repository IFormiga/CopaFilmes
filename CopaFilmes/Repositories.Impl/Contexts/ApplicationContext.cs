using CopaFilmes.Entities.Entities;
using CopaFilmes.Domain.Repositories.Impl.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CopaFilmes.Domain.Repositories.Impl.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Filme> Filmes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FilmeMap());
        }

        public async Task SeedData()
        {
            await CargaFilmesAsync();
        }

        public async Task CargaFilmesAsync()
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var uri = "https://copadosfilmes.azurewebsites.net/api/filmes";

                var response = httpClient.GetAsync(uri).Result;

                var cargaFilmes = response.Content.ReadAsAsync<List<Filme>>().Result;

                Filmes.AddRange(cargaFilmes);
                await SaveChangesAsync();
            }
        }
    }
}
