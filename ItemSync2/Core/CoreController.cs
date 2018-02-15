using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void TestConnection()
        {

        }

        public void SaveUserSettings()
        {

        }

        public void Help()
        {

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
