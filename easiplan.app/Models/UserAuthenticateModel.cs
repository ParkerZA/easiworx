using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.app.Models
{
    public class UserAuthenticationRequestModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool rememberMe { get; set; }
    }

    public class LoggedInUserModel
    {
        public long id { get; set; }
        public string fullName { get; set; }
        public string emailAddress { get; set; }
        public string role { get; set; }
        public string companyName { get; set; }
        public string databaseType { get; set; }      
    }

    public class UserAccountModel
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string surname { get; set; }
        public string emailAddress { get; set; }
       // public String status { get; set; }
        public string phoneNumber { get; set; }
        public string role { get; set; }
        //public Company company { get; set; }
        public bool activated { get; set; }
        public bool verified { get; set; }
    }
}
