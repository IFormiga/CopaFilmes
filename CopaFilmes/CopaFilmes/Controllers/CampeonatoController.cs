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
    public class CampeonatoController : Controller
    {
        private readonly ICampeonatoService _campeonatoService;

        public CampeonatoController(ICampeonatoService campeonatoService, IFilmeService filmeService)
        {
            _campeonatoService = campeonatoService;
        }

        // POST api/<controller>
        [HttpPost]
        public ResultadoCampeonatoDTO Post([FromBody]IList<string> idsFilmes)
        {
            return _campeonatoService.GerarCampeonato(idsFilmes);
        }
    }
}
