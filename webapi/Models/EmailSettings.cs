using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class EmailSettings
    {
        public string EmailServer { get; set; }
        public string UserName { get; set; }
        public string Passwrod { get; set; }
        public string EmailTo { get; set; }
        public string EmailBCC { get; set; }
       
        public EmailSettings() { }
    }
}
