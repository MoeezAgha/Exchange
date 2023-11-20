using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.BAL.Services.AppConfiguration
{
    public class JwtSetting
    {

        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string IssuerSigningKey { get; set; }

    }
}
