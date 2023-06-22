using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compori.Shopware.Types
{
    public class ErrorSource
    {
        [JsonProperty(PropertyName = "pointer")]
        public string Pointer { get; set; }
    }
}
