using ApiPlantas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPlantas.Azure
{
    public class PlantaFamiliaAzure
    {

        static string connectionString = @"Server=DESKTOP-S6JN4IA\SQLEXPRESS;Database=ApiPlantas;Trusted_Connection=True;";


        private static List<PlantaFamilia> plantasFamilia;

        public static List<PlantaFamilia> ObtenerPlantasFamilias()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = "select * from PlantaFamilia";

                var comando = ConsultaSqlPlanta(connection, consultaSql);

                var dataTablePlantas = LlenarDataTable(comando);

                return LLenadoPlantas(dataTablePlantas);
            }
        }

        private static SqlCommand ConsultaSqlPlanta(SqlConnection connection, string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = consulta;
            connection.Open();
            return sqlCommand;
        }

        private static DataTable LlenarDataTable(SqlCommand comando)
        {
            //2. llenamos el dataTable(conversion)
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(comando);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
        private static List<PlantaFamilia> LLenadoPlantas(DataTable dataTable)
        {
            plantasFamilia = new List<PlantaFamilia>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                PlantaFamilia plantaFamilia = new PlantaFamilia();
                plantaFamilia.idPlantaFamilia = int.Parse(dataTable.Rows[i]["idPlantaFamilia"].ToString());
                plantaFamilia.idFamilia = int.Parse(dataTable.Rows[i]["idFamilia"].ToString());
                plantaFamilia.idPlanta = int.Parse(dataTable.Rows[i]["idPlanta"].ToString());


                plantaFamilia.familia = FamiliaAzure.ObtenerFamilia(plantaFamilia.idFamilia);
                plantaFamilia.planta = PlantaAzure.ObtenerPlantaPorId(plantaFamilia.idPlanta);

                plantasFamilia.Add(plantaFamilia);
            }
            return plantasFamilia;
        }
    }
}
