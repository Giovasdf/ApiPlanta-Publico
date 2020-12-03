using ApiPlantas.Azure;
using ApiPlantas.Models;
using System;
using System.Linq;
using Xunit;
namespace XUnitTestApiPlanta
{
    public class UnitTestFamiliaAzure
    {

        [Fact]
        public void TestObtenerFamilias()
        {
            //arrange
            bool vieneConDatos = false;
            //act
            var familiasRetornadas = FamiliaAzure.ObtenerFamilias();
            vieneConDatos = familiasRetornadas.Any();

            //assert
            Assert.True(vieneConDatos);
        }

        [Fact]
        public void TestObtenerFamilia()
        {
            //arrange
            string nombreFamiliaTest = "Fabaceae";
            int idEsperado = 6;
            //act
            var familiaRetornada = FamiliaAzure.ObtenerFamilia(nombreFamiliaTest);

            //assert
            Assert.Equal(idEsperado, familiaRetornada.idFamilia);
        }
        
        [Fact]
        public void TestObtenerFamiliaPorId()
        {
            //arrange
            int idFamiliaTest = 6;
            int idEsperado = 6;
            //act
            var familiaRetornada = FamiliaAzure.ObtenerFamilia(idFamiliaTest);

            //assert
            Assert.Equal(idEsperado, familiaRetornada.idFamilia);
        }
        [Fact]
        public void TestAgregarFamiliaPorInstancia()
        {
            //arrange
            Familia familia = new Familia();
            familia.nombre = "AgregarFamiliaPorInstanciaTest";
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            //act
            resultadoObtenido = FamiliaAzure.AgregarFamilia(familia);

            FamiliaAzure.EliminarFamiliaPorNombre(familia.nombre);

            //assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void TestAgregarFamiliaPorParametros()
        {
            //arrange
            string nombreFamilia = "AgregarFamiliaPorParametros";
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            //act
            resultadoObtenido = FamiliaAzure.AgregarFamilia(nombreFamilia);

            FamiliaAzure.EliminarFamiliaPorNombre(nombreFamilia);

            //assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }
    
        [Fact]
        public void TestEliminarFamiliaPorNombre()
        {
            //arrange
            Familia familia = new Familia();
            familia.nombre = "PruebaNombreFamilia";
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            string familiaEliminar = "PruebaNombreFamilia";

            FamiliaAzure.AgregarFamilia(familia);

            //act
            resultadoObtenido = FamiliaAzure.EliminarFamiliaPorNombre(familiaEliminar);

            //assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void TestActualizarFamilia()
        {
            //arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            Familia familia = new Familia();
            familia.idFamilia = 6;
            familia.nombre = "FabaceaeTEST";

            //act
            resultadoObtenido = FamiliaAzure.ActualizarFamiliaPorId(familia);

            familia.nombre = "Fabaceae";
            FamiliaAzure.ActualizarFamiliaPorId(familia);

            //assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }
    }
}
