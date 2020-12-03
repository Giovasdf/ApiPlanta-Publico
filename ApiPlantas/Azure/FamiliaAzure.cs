using ApiPlantas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPlantas.Azure
{
    public class FamiliaAzure
    {
        static string connectionString = @"Server=DESKTOP-S6JN4IA\SQLEXPRESS;Database=ApiPlantas;Trusted_Connection=True;";

        private static List<Familia> familias;

        public static List<Familia> ObtenerFamilias()
        {            
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var comando = AbrirConexionSqlFamilias(sqlConnection);

                var dataTable = LLenadoTabla(comando);

                return ListarFamilias(dataTable);
            }
        }
        public static Familia ObtenerFamilia(string nombre)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"select * from Familia where nombre = '{nombre}'";

                var comando = AbrirConexionSqlFamilia(sqlConnection,query);
                
                var dataTable = LLenadoTabla(comando);

                return CreacionFamilia(dataTable);

            }
        }
        public static Familia ObtenerFamilia(int idFamilia)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"select * from Familia where idFamilia = '{idFamilia}'";

                var comando = AbrirConexionSqlFamilia(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionFamilia(dataTable);

            }
        }
        public static int AgregarFamilia(Familia familia)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Insert into Familia (nombre) values (@nombre)";

                sqlCommand.Parameters.AddWithValue("@nombre", familia.nombre);

                try
                {
                    sqlConnection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return filasAfectadas;
            }            
        }
        public static int AgregarFamilia(string familia)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Insert into Familia (nombre) values (@nombre)";

                sqlCommand.Parameters.AddWithValue("@nombre", familia);

                try
                {
                    sqlConnection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return filasAfectadas;
            }

        }
        public static int EliminarFamiliaPorNombre(string nombreFamilia)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Delete from familia where nombre = @nombre";

                sqlCommand.Parameters.AddWithValue("@nombre", nombreFamilia);

                try
                {
                    sqlConnection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return filasAfectadas;
            }
        }

        public static int ActualizarFamiliaPorId(Familia familia)
        {
            int filasAfectadas = 0;

            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update Familia SET nombre = @nombre where idFamilia = @idFamilia";

                sqlCommand.Parameters.AddWithValue("@nombre", familia.nombre);
                sqlCommand.Parameters.AddWithValue("@idFamilia", familia.idFamilia);

                try
                {
                    sqlConnection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return filasAfectadas;
            }           
        }


        private static SqlCommand AbrirConexionSqlFamilias(SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
            sqlCommand.CommandText = "select * from Familia";
            sqlConnection.Open();
            return sqlCommand;
        }
        private static SqlCommand AbrirConexionSqlFamilia(SqlConnection sqlConnection, string query)
        {
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);            
            sqlConnection.Open();
            return sqlCommand;
        }
        private static DataTable LLenadoTabla(SqlCommand comando)
        {
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(comando);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
        private static List<Familia> ListarFamilias(DataTable dataTable)
        {
            familias = new List<Familia>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Familia familia = new Familia();
                familia.idFamilia = int.Parse(dataTable.Rows[i]["idFamilia"].ToString());
                familia.nombre = dataTable.Rows[i]["nombre"].ToString();
                familias.Add(familia);
            }

            return familias;
        }
        private static Familia CreacionFamilia(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Familia familia = new Familia();
                familia.idFamilia = int.Parse(dataTable.Rows[0]["idFamilia"].ToString());
                familia.nombre = dataTable.Rows[0]["nombre"].ToString();
                return familia;
            }
            else
            {
                return null;
            }
        }


    }
}
