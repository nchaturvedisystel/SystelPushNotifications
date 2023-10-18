using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushNotification.Model
{
    public class DBConfigConnectionDTO
    {
        public string ConnectionName { get; set; }
        public string ServerName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DBName { get; set; }
    }
    public class ConfigConnectionList
    {
        public IEnumerable<DBConfigConnectionDTO> list { get; set; }
    }
}