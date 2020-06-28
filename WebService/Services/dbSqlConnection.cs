using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace WebService.Services
{
    public class dbSqlConnection
    {
        SQLiteConnection connection;

        public dbSqlConnection()
        {
          
        }

        public DataTable getCredencial(string name ,string password)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=./Data/Apidb.db;Version=3;UseUTF8Encoding=True;");
            connection.Open();
            //creamos una tabla de datos
            DataTable dt = new DataTable();
            //creamos el adaptador de datos
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT COUNT(*) FROM credencial cr WHERE cr.name='" + name + "' AND cr.password='" + password + "'", connection);
            //llenamos la tabla con los resultados del adaptador
            da.Fill(dt);
            //cerramos conexion
            connection.Close();
            //se cierra la conexión
            return dt;
        }
    }
}
