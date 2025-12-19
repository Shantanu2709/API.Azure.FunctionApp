using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cosmos_Connectivity.AD.Entities.AdminEns
{
    public class AdminE
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public string? adminId {  get; set; }   
        public string? adminName { get; set; }
        public string? adminEmail { get; set; }
    }
}
