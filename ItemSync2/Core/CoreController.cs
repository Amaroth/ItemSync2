using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;

namespace ItemSync2.Core
{
    class CoreController
    {
        private static CoreController instance;

        private CoreController() { }

        public static CoreController Instance
        {
            get
            {
                if (instance == null)
                    instance = new CoreController();
                return instance;
            }
        }

        private UserSettings usi = UserSettings.Instance;
        private SQLConnector sql = new SQLConnector();
        private DBCConnector dbc = new DBCConnector();

        public void TestConnection()
        {
            try
            {
                sql.TestConnection(usi.host, usi.port, usi.database, usi.table, usi.login, usi.password);
            }
            catch (Exception e) { MessageBox.Show("Connection to MySQL server was not successful.\n\n" + e.Message); }
        }

        public void SaveUserSettings()
        {
            usi.SaveUserSettings();
        }

        public void Help()
        {
            System.Diagnostics.Process.Start("https://github.com/Amaroth/ItemSync2/issues");
        }

        public void CheckChanges()
        {

        }

        public void DbToDbcSync()
        {

        }

        public void DbcToDbSync()
        {

        }
    }
}
