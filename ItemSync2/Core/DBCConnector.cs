using System;
using System.Collections.Generic;
using WDBXLib.Reader;
using WDBXLib.Definitions.WotLK;
using WDBXLib.Storage;
using System.Linq;
using System.IO;

namespace ItemSync2.Core
{
    class DBCConnector
    {
        private DBEntry<Item> dbc;

        private string filePath;

        /// <summary>
        /// Sets DBC's path and attempts to open and load provided DBC.
        /// </summary>
        /// <param name="filePath">Path to DBC file.</param>
        public void SetDBCFile(string filePath)
        {
            try
            {
                this.filePath = filePath;
                dbc = DBReader.Read<Item>(filePath);
            }
            catch (Exception e) { throw new Exception("Error occured while attempting to read provided DBC.:\n\n" + e.Message); }
        }

        /// <summary>
        /// Gets all items from within provided range.
        /// </summary>
        /// <param name="start">Minimum ID.</param>
        /// <param name="end">Maximum ID.</param>
        /// <returns>Collection of items found.</returns>
        public Dictionary<int, Item> GetItems(int start, int end)
        {
            var result = new Dictionary<int, Item>();
            foreach (var item in dbc.Rows)
                if (item.ID >= start && item.ID <= end)
                    result.Add(item.ID, item);
            return result;
        }

        /// <summary>
        /// Updates DBC by using provided collection of items from DB.
        /// </summary>
        /// <param name="inDb">Items in DB.</param>
        /// <param name="insertNew">If true, insert missing ones.</param>
        /// <param name="updateOld">If true, update existing ones.</param>
        public void UpdateDBC(Dictionary<int, Item> inDb, bool insertNew, bool updateOld)
        {
            try
            {
                if (!insertNew && !updateOld)
                    return;
                foreach (var item in inDb)
                {
                    if (dbc.Rows.ContainsKey(item.Key) && updateOld)
                    {
                        dbc.Rows.RemoveByKey(item.Key);
                        dbc.Rows.Add(item.Value);
                    }
                    else if (insertNew)
                        dbc.Rows.Add(item.Value);
                }
                DBReader.Write(dbc, filePath);
            }
            catch (Exception e) { throw new Exception("Error occured while attempting to update DBC.:\n\n" + e.Message); }
        }

        /// <summary>
        /// Generates collection of items in DBC, which are not in DB, within provided range.
        /// </summary>
        /// <param name="startID">Minimum ID.</param>
        /// <param name="endID">Maximum ID.</param>
        /// <param name="inDB">Items currently in DB.</param>
        /// <returns>Collection of items missing in DB.</returns>
        public List<Item> GetMissing(int startID, int endID, Dictionary<int, Item> inDB)
        {
            var result = new List<Item>();
            foreach (var item in dbc.Rows)
                if (!inDB.ContainsKey(item.ID))
                    result.Add(item);
            return result;
        }
    }
}
