using System;
using System.Collections.Generic;
using WDBXLib.Reader;
using WDBXLib.Definitions.WotLK;
using WDBXLib.Storage;
using System.Linq;

namespace ItemSync2.Core
{
    class DBCConnector
    {
        private DBEntry<Item> dbc;

        private string filePath;

        /// <summary>
        /// Opens given DBC file for reading.
        /// </summary>
        /// <param name="filePath"></param>
        public void SetDBCFile(string filePath)
        {
            try
            {
                this.filePath = filePath;
                dbc = DBReader.Read<Item>(filePath);
            }
            catch (Exception e) { throw new Exception("Error occured while attempting to read provided DBC.:\n\n" + e.Message); }
        }

        public Dictionary<int, Item> GetItems(int start, int end)
        {
            var result = new Dictionary<int, Item>();
            foreach (var item in dbc.Rows)
                if (item.ID >= start && item.ID <= end)
                    result.Add(item.ID, item);
            return result;
        }
    }
}
