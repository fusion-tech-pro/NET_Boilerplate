using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionTechBoilerplate.Utilities.EmailSender
{
    public class EmailOptions
    {
        public const string EmailSettings = "Smtp";

        public string User { get; set;}
        public string Password { get; set;}
        public string Server { get; set;}
        public int Port { get; set;}
        public bool UseSsl { get; set;}
        public bool RequiresAuthentication { get; set; }
    }
}
