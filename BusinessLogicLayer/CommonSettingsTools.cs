using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using StudentsManager.DataAccessLayer;
using DTO = StudentsManager.BusinessLogicLayer.DataTransferObjects;

namespace StudentsManager.BusinessLogicLayer
{
    public static class CommonSettingsTools
    {
        public static String ConfigFilePathName 
        { 
            set
            {
                WebConfigTools.ConfigFilePathName = value;
            }
            get
            {
                return WebConfigTools.ConfigFilePathName; 
            }
        }

        private static String _connectionStringName = "StudentsManagerEntities";

        public static void GetCurrentConnectionStringParameters
        (
            out String dataSource,
            out String initialCatalog,
            out String userId,
            out String password
        )
        {
            String connStr = WebConfigTools.ReadConnectionStringByName(_connectionStringName);

            dataSource = StringTools.GetPartBetweenDefinedStringMarkers(connStr, "data source=", ";");
            initialCatalog = StringTools.GetPartBetweenDefinedStringMarkers(connStr, "initial catalog=", ";");
            userId = StringTools.GetPartBetweenDefinedStringMarkers(connStr, "User ID=", ";");
            password = StringTools.GetPartBetweenDefinedStringMarkers(connStr, "Password=", ";");
        }

        public static void UpdateCurrentConnectionStringWithParameters
        (
            String dataSource,
            String initialCatalog,
            String userId,
            String password
        )
        {
            String connStr = WebConfigTools.ReadConnectionStringByName(_connectionStringName);

            connStr = StringTools.SetPartBetweenDefinedStringMarkers(connStr, "data source=", ";", dataSource);
            connStr = StringTools.SetPartBetweenDefinedStringMarkers(connStr, "initial catalog=", ";", initialCatalog);
            connStr = StringTools.SetPartBetweenDefinedStringMarkers(connStr, "User ID=", ";", userId);
            connStr = StringTools.SetPartBetweenDefinedStringMarkers(connStr, "Password=", ";", password);

            WebConfigTools.UpdateConnectionStringByName(_connectionStringName, connStr);
        }

        public static String GetValue(String name)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblCommonSetting currItem = ctx.tblCommonSettings.SingleOrDefault(x => x.Name == name);

            if (currItem == null) return null;

            return currItem.Value;
        }

        public static String SetValue(String name, String value)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblCommonSetting currItem = ctx.tblCommonSettings.SingleOrDefault(x => x.Name == name);

            if (currItem == null)
            {
                currItem = new tblCommonSetting()
                {
                    Name = name,
                    Value = value
                };
            }
            else
            {
                currItem.Value = value;
            }
            ctx.SaveChanges();

            return currItem.Value;
        }

        public static void MakeDatabaseBackup()
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblCommonSetting currItem = ctx.tblCommonSettings.SingleOrDefault(x => x.Name == "DatabaseBackupPath");
            if( currItem!=null && String.IsNullOrWhiteSpace(currItem.Value)==false )
            {
                String Action = "EXEC [dbo].[prcMakeBackup] @pathOnly = N\'" + currItem.Value + "\'";
                ctx.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, Action);
            }
        }
    }
}
