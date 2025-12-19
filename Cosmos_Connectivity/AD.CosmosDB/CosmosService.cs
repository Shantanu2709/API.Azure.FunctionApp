using Cosmos_Connectivity.AD.Entities;
using Cosmos_Connectivity.AD.Entities.AdminEns;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos_Connectivity.AD.CosmosDB
{
    public class CosmosService : ICosmosDB
    {

        private readonly Container _container;

        public CosmosService(string connectionString, string cosmosDBName, string cosmosContainerName)
        {
            var client = new CosmosClient(connectionString);

            _container = client.GetContainer(cosmosDBName, cosmosContainerName);
        }

        public async Task<AdminE> CreateUserAsync(AdminE admin)
        {
            if(string.IsNullOrEmpty(admin.adminId))
            {
                admin.adminId = Guid.NewGuid().ToString();
                admin.id = admin.adminId;
            }

            var resp = await _container.CreateItemAsync(admin, new PartitionKey(admin.adminId));
            return resp.Resource;
        }
    }
}
