using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_doorlock_web_server.Database
{
    public class SDDatabase
    {
        private MySqlConnection _connection = null;
        private static readonly SDDatabase _instance = new SDDatabase();
        public static SDDatabase Instance {
            get { return _instance; }
        }

        private SDDatabase()
        {
        }

        public MySqlConnection Connection
        {
            get { return _connection; }
        }

        public void LoadConnectionInfo(string databaseAddr, uint databasePort,  string userName, string password, string databaseName)
        {
            if (String.IsNullOrEmpty(databaseAddr))
            {
                throw new ArgumentNullException("Database address has not been provided");
            }
            if (databasePort <= 0 || databasePort >= 65535)
            {
                throw new ArgumentOutOfRangeException("Invalid database port given: " + databasePort);
            }
            if (String.IsNullOrEmpty(databaseName) || String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Database name, user name and/or password has not been provided");
            }
                   
            string connString = string.Format("Server={0}; port={1}; database={2}; UID={3}; password={4}", 
                                              databaseAddr,databasePort,databaseName,userName,password);
            _connection = new MySqlConnection(connString);
        }   
    }
}
