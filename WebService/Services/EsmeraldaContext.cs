using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace WebService.Services
{
    public class EsmeraldaContext : DbContext
    {
        public string ConnectionString { get; set; }
        //LLamar el contructor
        public EsmeraldaContext(string connection)
        {
            this.ConnectionString = connection;
        }
        //Conexión que devuelve la conexíon
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        /// <summary>
        /// Métodos para agregar sospechas 
        /// </summary>
        public Sospecha AddSospecha()
        {
            Sospecha sospecha = new Sospecha();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sospecha.ID = 1;
                        sospecha.age = 23;
                        //list.Add(new Album()
                        //{
                        //    Id = Convert.ToInt32(reader["Id"]),
                        //    Name = reader["Name"].ToString(),
                        //    ArtistName = reader["ArtistName"].ToString(),
                        //    Price = Convert.ToInt32(reader["Price"]),
                        //    Genre = reader["genre"].ToString()
                        //});
                    }
                }
            }
            return sospecha;
        }
    }

}
