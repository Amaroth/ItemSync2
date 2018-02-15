using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Windows;

namespace ItemSync2.Core
{
    class SQLConnector
    {
        private SQLDataConfig sqlConfig;
        private MySqlConnection connection;

        private string host;
        private int port;
        private string database;
        private string table;
        private SecureString login;
        private SecureString password;

        private SecureString connectionString;

        public SQLConnector()
        {
            try
            {
                sqlConfig = new SQLDataConfig("Configs/SQLConfig.xml");
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); throw; }
        }

        /// <summary>
        /// Sets connection string based on provided input.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public void SetConnectionInformation(string host, int port, string database, string table, SecureString login, SecureString password)
        {
            this.host = host;
            this.port = port;
            this.database = database;
            this.table = table;
            this.login = login;
            this.password = password;
            connectionString = Utilities.ToSecureString(string.Format("Server={0}; Port={1}; Database={2}; Uid={3}; Pwd={4}; SslMode=none;",
                host, port, database, Utilities.ToInsecureString(login), Utilities.ToInsecureString(password)));
        }

        /// <summary>
        /// Tests wheter database connection with provided data can be successfuly established.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public void TestConnection(string host, int port, string database, string table, SecureString login, SecureString password)
        {
            SetConnectionInformation(host, port, database, table, login, password);
            try
            {
                connection = new MySqlConnection(Utilities.ToInsecureString(connectionString));
                connection.Open();
                MessageBox.Show("Connection was succesful.");
                connection.Close();
            }
            catch (Exception e) { throw new Exception("Error occured while establishing database connection.\n\n" + e.Message); }
        }
    }
}
