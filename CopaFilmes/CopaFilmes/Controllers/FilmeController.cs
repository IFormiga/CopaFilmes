using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaFilmes.Domain.Entities.DTO;
using CopaFilmes.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CopaFilmes.Controllers
{
    [Route("api/[controller]")]
    public class FilmeController : Controller
    {
        private readonly IFilmeService _filmeService;

        public FilmeController(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IList<FilmeDTO> ObterTodosFilmes()
        {
            return _filmeService.ObterPesquisaFilmes();
        }
    }
}
