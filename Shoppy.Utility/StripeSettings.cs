using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shoppy.Utility
{
    public class StripeSettings
    {
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
     
    }
}
