using Cosmos_Connectivity.AD.CosmosDB;
using Cosmos_Connectivity.AD.Entities;
using Cosmos_Connectivity.AD.Entities.AdminEns;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cosmos_Connectivity.AD.Functions.AdminFunc
{
    public class CreateAdmin
    {
        private readonly ICosmosDB _cosmosservice;

        public CreateAdmin(ICosmosDB cosmosservice)
        {
            _cosmosservice = cosmosservice;
        }

        [Function("createAdminData")]


        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "postAd")] HttpRequestData req)
        {
            //Read the requested Json data and store in string asynchronassly
            string reqBody = await new StreamReader(req.Body).ReadToEndAsync();

            //Convert the data in to string object
            AdminE? userData;

            userData = JsonConvert.DeserializeObject<AdminE>(reqBody);

            //validate
            if (userData == null || string.IsNullOrEmpty(userData.adminName) || string.IsNullOrEmpty(userData.adminEmail))
            {
                //create Response
                var resp2 = req.CreateResponse(HttpStatusCode.BadGateway);

                //Write response
                await resp2.WriteStringAsync("Invalid Json Body/ missing params");

                return resp2;
            }

            var store = _cosmosservice.CreateUserAsync(userData);
            var resp1 = req.CreateResponse(HttpStatusCode.Created);

            await resp1.WriteAsJsonAsync(store);

            return resp1;

        }
    }

}