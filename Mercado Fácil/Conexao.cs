using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado_Fácil
{
    public static class Conexao
    {
        private static string connectionString =
            "server=127.0.0.1;port=3306;database=mercadoFacil;uid=root;pwd=;";

        public static MySqlConnection ObterConexao()
        {
            return new MySqlConnection(connectionString);
        }
    }
}

