using CopaFilmes.Domain.Services.Impl.Services;
using CopaFilmes.Services.Impl.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.EntityFrameworkCore;
using CopaFilmes.Entities.Entities;
using CopaFilmes.Domain.Repositories.Impl.Contexts;
using CopaFilmes.Domain.Repositories.Impl.Repositories;
using System.Threading.Tasks;

namespace UnitTestsXUnit
{
    public class CampeonatoServiceTest
    {
        ApplicationContext context;
        FilmeRepository filmeRepository;
        FilmeService filmeService;
        CampeonatoService campeonatoService;

        private async void CriarDependenciasMockAsync()
        {
            context = new ApplicationContext();
            filmeRepository = new FilmeRepository(context);
            filmeService = new FilmeService(filmeRepository);
            campeonatoService = new CampeonatoService(filmeService);
            await SeedDatabase();
        }

        private async Task SeedDatabase()
        {
            await context.SeedData();
        }

        /// <summary>
        /// Um exemplo de teste unitário.
        /// </summary>
        [Fact]
        public void Gerar_Campeonato_Com_Numero_Invalido_de_Filmes()
        {
            CriarDependenciasMockAsync();
            IList<string> listaFilmes = new List<string>() { "teste", "teste" };

            Action action = () => campeonatoService.GerarCampeonato(listaFilmes);
            action
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("O número de filmes selecionados precisa ser igual a 8\r\nParameter name: idsFilmesSelecionados");
        }
    }
}
