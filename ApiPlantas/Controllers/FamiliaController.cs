using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPlantas.Models;
using ApiPlantas.Azure;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiPlantas.Controllers
{    
     //localhost:8080/api/Familia
     //192.168.1.20:8080/api/Familia
     //mipaginaweb.com/api/Familia
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliaController : ControllerBase
    {
        //GET /api/familia/all
        [HttpGet("all")]
        public JsonResult ObtenerFamilias()
        {
            //json
            var familiasRetornadas = FamiliaAzure.ObtenerFamilias();
            return new JsonResult(familiasRetornadas);
        }

        //GET /api/familia/{id}-{nombre}
        [HttpGet("{Familia}")]
        public JsonResult ObtenerFamilia(string Familia)
        {
            var conversionExitosa =  int.TryParse(Familia, out int idConvertido);

           Familia familiaRetornada;

            if (conversionExitosa)
            {
                familiaRetornada = FamiliaAzure.ObtenerFamilia(idConvertido);
            }
            else
            {
                familiaRetornada = FamiliaAzure.ObtenerFamilia(Familia);
            }

            if(familiaRetornada is null)
            {
                return new JsonResult($"Intente nuevamente con un parametro distinto a {Familia}");
            }
            else
            {
                return new JsonResult(familiaRetornada);
            }
            
           
        }


    }
}
