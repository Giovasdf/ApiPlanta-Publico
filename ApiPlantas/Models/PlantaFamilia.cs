using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPlantas.Models
{
    public class PlantaFamilia
    {
        public int idPlantaFamilia { get; set; }
        public int idPlanta { get; set; }
        public int idFamilia { get; set; }

        public Familia familia { get; set; }
        public Planta planta { get; set; }

    }
}
