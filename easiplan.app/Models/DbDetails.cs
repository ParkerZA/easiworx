using easiplan.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finx.App.Models
{
    public class DbDetails:BaseEntity<int>
    {
        public string DbType { get; set; }
        public string DbConnStr { get; internal set; }
        public string DbServer { get; set; }
        public string DbPort { get; set; }
        public string DbAdminUser { get; set; }
        public string DbAdminPwd { get; set; }
        public string DbCatalog { get; set; }
       
    }
}
