using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentsManager.BusinessLogicLayer;

namespace StudentsManagerDBBackuper
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonSettingsTools.ConfigFilePathName = "App.config";
            CommonSettingsTools.MakeDatabaseBackup();
        }
    }
}
