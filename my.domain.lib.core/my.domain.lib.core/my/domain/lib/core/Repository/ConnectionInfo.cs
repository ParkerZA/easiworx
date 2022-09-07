using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my.domain.lib.core.Repository
{
    public class ConnectionInfo : IConnectionInfo
    {
        public string ConnectionType { get; set; }
        [Required(ErrorMessage = "Database Name is Required")]
        public string DbName { get; set; }
        public string DbSchema { get; set; }
        [Required(ErrorMessage = "Server Name or IP is Required")]
        public string ServerName { get; set; }
        [Required(ErrorMessage = "Port Number is Required")]
        public string PortNumber { get; set; }
        [Required(ErrorMessage = "Database UserName is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Database UserPassword is Required")]
        public string UserPwd { get; set; }
        public bool CreateNewSchema { get; set; }
        public bool UpdateSchema { get; set; }
        public Type AssemblyType { get; set; }

        public virtual string ConnectionString(bool CreateDb = false)
        {
            return string.Format("server={0};Port={1};userid={2};database={3};password={4};Persist Security Info=True;", ServerName, PortNumber, UserName, CreateDb ? "" : DbName, UserPwd);

        }

        public virtual void EnsureConnectionClosed()
        {
            
        }

        public virtual void EnsureConnectionOpen()
        {
           
        }

        public virtual string MappingNamespace { get; set; }

        public ConnectionInfo(ConnectionTypes connectionType)
        {
            ConnectionType = connectionType.ToString();
        }
    }
}
