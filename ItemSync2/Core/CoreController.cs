using System;
using System.Collections.Generic;
using System.Windows;
using WDBXLib.Definitions.WotLK;

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

        /// <summary>
        /// Tests connection to MySQL database with currently provided input by user.
        /// </summary>
        public void TestConnection()
        {
            try
            {
                sql.SetConnectionInformation(usi.host, usi.port, usi.database, usi.table, usi.login, usi.password);
                sql.TestConnection();
            }
            catch (Exception e) { MessageBox.Show("Connection to MySQL server was not successful.\n\n" + e.Message); }
        }

        /// <summary>
        /// Saves user's current input as on startup default settings.
        /// </summary>
        public void SaveUserSettings()
        {
            usi.SaveUserSettings();
        }

        /// <summary>
        /// Open's repo's issues page.
        /// </summary>
        public void Help()
        {
            System.Diagnostics.Process.Start("https://github.com/Amaroth/ItemSync2/issues");
        }

        /// <summary>
        /// Compares DBC and DB, end provides user with information about how many items don't match or are missing somewhere.
        /// </summary>
        public void CheckChanges()
        {
            try
            {
                sql.SetConnectionInformation(usi.host, usi.port, usi.database, usi.table, usi.login, usi.password);
                dbc.SetDBCFile(usi.dbcPath);
                var inDatabase = sql.GetItems(usi.startID, usi.endID);
                var inDbc = dbc.GetItems(usi.startID, usi.endID);
                int missingInDbc = 0;
                int missingInDb = 0;
                int different = 0;

                foreach (var item in inDatabase)
                {
                    if (!inDbc.ContainsKey(item.Key))
                        missingInDbc++;
                    else if (!Utilities.AreEqual(inDbc[item.Key], inDatabase[item.Key]))
                        different++;
                }
                foreach (var item in inDbc)
                    if (!inDatabase.ContainsKey(item.Key))
                        missingInDb++;


                string output = string.Format("{0} items in database are missing in DBC.\n{1} items in DBC are missing in database.\n{2} items are in both DBC and database, but have different data.\n\n",
                    missingInDbc, missingInDb, different);
                string caption;

                if (missingInDb > 0)
                    output += "Using create new in DB is recommended.\n";
                else
                    output += "Create new in DB is not needed.\n";

                if (missingInDbc > 0)
                    output += "Using create new in DBC is recommended.\n";
                else
                    output += "Create new in DBC is not needed.\n";

                if (different > 0)
                    output += "Using update existing in DBC is recommended.\n";
                else
                    output += "Update existing in DBC is not needed.\n";

                if (missingInDb == 0 && missingInDbc == 0 && different == 0)
                {
                    output += "\nDBC is up to date, and nothing is missing in database - you don't have to do anything.";
                    caption = "DBC up to date";
                }
                else
                    caption = "DBC is outdated";
                MessageBox.Show(output, caption);
            }
            catch (Exception e) { MessageBox.Show("Couldn't check for changes, following error occured.:\n\n" + e.Message); }
        }

        /// <summary>
        /// Syncs DBC with DB, based on user's settings.
        /// </summary>
        public void Sync()
        {
            if (usi.createInDB || usi.createInDBC || usi.updateInDBC)
            {
                try
                {
                    sql.SetConnectionInformation(usi.host, usi.port, usi.database, usi.table, usi.login, usi.password);
                    dbc.SetDBCFile(usi.dbcPath);
                    var inDB = sql.GetItems(usi.startID, usi.endID);
                    dbc.UpdateDBC(inDB, usi.createInDBC, usi.updateInDBC);
                    if (usi.createInDB)
                    {
                        List<Item> test = dbc.GetMissing(usi.startID, usi.endID, inDB);
                        sql.InsertIntoDB(test);
                    }
                    MessageBox.Show("All successfully synced.");
                }
                catch (Exception e) { MessageBox.Show(e.Message); }
            }
            else
                MessageBox.Show("Nothing to do - check at least one checkbox next to DO STUFF!!! button.");
        }
    }
}
