using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPlantas.Azure;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiPlantas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantaFamiliaController : ControllerBase
    {
        // GET: api/<PlantaFamiliaController>
        [HttpGet]
        public JsonResult Get()
        {
            var lista = PlantaFamiliaAzure.ObtenerPlantasFamilias();
            return new JsonResult(lista);
            
        }
    }
}
