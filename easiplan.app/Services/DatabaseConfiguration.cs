using System.Threading.Tasks;
using Finx.App;
using Finx.App.Models;

namespace easiplan.app.Services
{
    public class DatabaseConfiguration
    {
        public void SetDatabaseConfiguration(Database database)
        {
            Program.ConnectionInfo.ConnectionType = "MySQL";
            Program.ConnectionInfo.ServerName = database.ConnectionString;
            Program.ConnectionInfo.DbName = database.DatabaseName;
            Program.ConnectionInfo.DbSchema = database.DatabaseName;
            Program.ConnectionInfo.PortNumber = database.Port;
            Program.ConnectionInfo.UserName = database.Username;
            Program.ConnectionInfo.UserPwd = database.Password;

        }
    }
}