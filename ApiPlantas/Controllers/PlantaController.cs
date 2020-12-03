using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPlantas.Azure;
using ApiPlantas.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiPlantas.Controllers
{
    //localhost:44386/api/planta
    [Route("api/[controller]")]
    [ApiController]
    public class PlantaController : ControllerBase
    {
        [HttpGet("hola")]
        public JsonResult Saludos()
        {
            return new JsonResult("Hola!");
        }

        // GET: api/planta/all
        [HttpGet("all")]
        public JsonResult ObtenerPlantas()
        {
            var plantasRecibidas = PlantaAzure.ObtenerPlantas();
            return new JsonResult(plantasRecibidas);
        }

        //GET: api/planta/{1}-{nombre}

        [HttpGet("{idPlanta}")]
        public JsonResult ObtenerPlanta(string idPlanta)
        {
            var conversionExitosa = int.TryParse(idPlanta, out int idConvertido);
            Planta plantaRecibida;

            if (conversionExitosa)
            {
                plantaRecibida = PlantaAzure.ObtenerPlantaPorId(idConvertido);
            }
            else
            {
                plantaRecibida = PlantaAzure.ObtenerPlantaPorNombrePopular(idPlanta);
            }

            if(plantaRecibida is null)
            {
                return new JsonResult($"Intente nuevamente con un parametro distinto a {idPlanta}");
            }
            else
            {
                return new JsonResult(plantaRecibida);
            }

        }

        //POST: api/planta
        [HttpPost]
        public void AgregarPlanta([FromBody]Planta planta)
        {
            PlantaAzure.AgregarPlanta(planta);
        }
    }
}
