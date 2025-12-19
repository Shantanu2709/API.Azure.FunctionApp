using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos_Connectivity.AD.Entities;
using Cosmos_Connectivity.AD.Entities.AdminEns;

namespace Cosmos_Connectivity.AD.CosmosDB
{
    public interface ICosmosDB
    {

        Task<AdminE> CreateUserAsync(AdminE admin);
    }
}
