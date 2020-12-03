using ApiPlantas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ApiPlantas.Azure
{
    public class PlantaAzure
    {

        static string connectionString = @"Server=DESKTOP-S6JN4IA\SQLEXPRESS;Database=ApiPlantas;Trusted_Connection=True;";


        private static List<Planta> plantas;

        public static List<Planta> ObtenerPlantas()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = "select * from Planta";

                var comando = ConsultaSqlPlanta(connection, consultaSql);

                var dataTablePlantas = LlenarDataTable(comando);
                
                return LLenadoPlantas(dataTablePlantas); 
            }
        }

        public static Planta ObtenerPlantaPorId(int idPlanta)
        {            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from Planta where idPlanta = {idPlanta}";

                var comando = ConsultaSqlPlanta(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);
               
                return CreacionPlanta(dataTable);
            }           
        }

        public static Planta ObtenerPlantaPorNombrePopular(string nombrePopular)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from Planta where nombrePopular = '{nombrePopular}'";

                var comando = ConsultaSqlPlanta(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionPlanta(dataTable);

            }
        }

        public static int AgregarPlanta(Planta planta)
        {
            int filasAfectadas = 0;

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into Planta (nombreCientifico,nombrePopular) values (@nombreCientifico,@nombrePopular)";
                sqlCommand.Parameters.AddWithValue("@nombreCientifico", planta.NombreCientifico);
                sqlCommand.Parameters.AddWithValue("@nombrePopular", planta.NombrePopular);

                try
                {
                    connection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }
            return filasAfectadas;
        }

        public static int AgregarPlanta(string nombrePopular,string nombreCientifico)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into Planta (nombreCientifico,nombrePopular) values (@nombreCientifico,@nombrePopular)";
                sqlCommand.Parameters.AddWithValue("@nombreCientifico", nombreCientifico);
                sqlCommand.Parameters.AddWithValue("@nombrePopular", nombrePopular);
                try
                {
                    connection.Open();
                    resultado = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return resultado;
        }

        public static int EliminarPlantaPorNombrePopular(string nombrePopular)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Delete from Planta where nombrePopular = @nombrePopular";
                sqlCommand.Parameters.AddWithValue("@nombrePopular", nombrePopular);

                try
                {
                    connection.Open();
                    resultado = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return resultado;
            }     
        }

        public static int ActualizarPlantaPorId(Planta planta)
        {
            int resultado = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update Planta SET nombreCientifico = @nombreCientifico,nombrePopular = @nombrePopular where idPlanta = @idPlanta";

                sqlCommand.Parameters.AddWithValue("@nombreCientifico", planta.NombreCientifico);
                sqlCommand.Parameters.AddWithValue("@nombrePopular", planta.NombrePopular);
                sqlCommand.Parameters.AddWithValue("@idPlanta", planta.idPlanta);

                try
                {
                    sqlConnection.Open();
                    resultado = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return resultado;
        }
       
        private static SqlCommand ConsultaSqlPlanta(SqlConnection connection, string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = consulta;
            connection.Open();
            return sqlCommand;
        }
        private static Planta CreacionPlanta(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Planta planta = new Planta();
                planta.idPlanta = int.Parse(dataTable.Rows[0]["idPlanta"].ToString());
                planta.NombrePopular = dataTable.Rows[0]["nombrePopular"].ToString();
                planta.NombreCientifico = dataTable.Rows[0]["nombreCientifico"].ToString();
                return planta;
            }
            else
            {
                return null;
            }
        }
        private static DataTable LlenarDataTable(SqlCommand comando)
        {           
            //2. llenamos el dataTable(conversion)
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(comando);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
        private static List<Planta> LLenadoPlantas(DataTable dataTable)
        {
            plantas = new List<Planta>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Planta planta = new Planta();
                planta.idPlanta = int.Parse(dataTable.Rows[i]["idPlanta"].ToString());
                planta.NombrePopular = dataTable.Rows[i]["nombrePopular"].ToString();
                planta.NombreCientifico = dataTable.Rows[i]["nombreCientifico"].ToString();
                plantas.Add(planta);
            }
            return plantas;
        }
        
      



    }

    
}
