using ApiPlantas.Azure;
using ApiPlantas.Models;
using System;
using System.Linq;
using Xunit;

namespace XUnitTestApiPlanta
{
    public class UnitTestPlantaAzure
    {
        [Fact]
        public void TestObtenerPlantas()
        {
            //Arrange
            bool vieneConDatos = false;
            
            //Act
            var resultado = PlantaAzure.ObtenerPlantas();
            vieneConDatos = resultado.Any();

            //Assert 
            Assert.True(vieneConDatos);
        } 

        [Fact]
        public void TestObtenerPlantaPorId()
        {
            //Arrange
            int idProbar = 1;
            Planta plantaRetornada;
            int resultadoEsperado = 1;
            //Act
            plantaRetornada = PlantaAzure.ObtenerPlantaPorId(idProbar);
            
            //Assert 
            Assert.Equal(resultadoEsperado,plantaRetornada.idPlanta);
        }

        [Fact]
        public void TestObtenerPlantaPorNombrePopular()
        {
            //Arrange
            string nombrePopular = "Melisa";
            Planta plantaRetornada;
            string resultadoEsperado = "Melisa";
            //Act
            plantaRetornada = PlantaAzure.ObtenerPlantaPorNombrePopular(nombrePopular);

            //Assert 
            Assert.Equal(resultadoEsperado, plantaRetornada.NombrePopular);

            
        }

        [Fact]
        public void TestAgregarPlanta()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            Planta planta = new Planta();
            planta.NombreCientifico = "Ocimum basilicum";
            planta.NombrePopular = "Albahaca";

            //Act
            resultadoObtenido = PlantaAzure.AgregarPlanta(planta);

            //Assert 
            Assert.Equal(resultadoEsperado,resultadoObtenido);

        }
        
        [Fact]
        public void TestAgregarPlantaPorParametro()
        {
            //Arrange
            int resultadoEsperado = 1; 
            int resultadoObtenido = 0;
            string NombreCientifico = "Ruta graveolens";
            string NombrePopular = "Ruda";

            //Act
            resultadoObtenido = PlantaAzure.AgregarPlanta(NombrePopular, NombreCientifico);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }


        [Fact]
        public void TestEliminarPlantaPorNombrePopular()
        {
            //Arrange         
            Planta planta = new Planta();
            planta.NombreCientifico = "Orchidaceae";
            planta.NombrePopular = "Orquídea";

            string nombrePlantaEliminar = "Orquídea";

            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            PlantaAzure.AgregarPlanta(planta);

            //Act
            resultadoObtenido = PlantaAzure.EliminarPlantaPorNombrePopular(nombrePlantaEliminar);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }


        [Fact]
        public void TestActualizarPlantaPorId()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            Planta planta = new Planta();
            planta.idPlanta = 1;
            planta.NombreCientifico = "Melissa officinalis";
            planta.NombrePopular = "Toronjil";

            //Act
            resultadoObtenido = PlantaAzure.ActualizarPlantaPorId(planta);

            planta.NombrePopular = "Melisa";
            PlantaAzure.ActualizarPlantaPorId(planta);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }


     
    }
}
