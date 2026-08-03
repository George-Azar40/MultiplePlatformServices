using MultiplePlatformServices.DAL.Data;
using MultiplePlatformServices.DAL.Models;
using MultiplePlatformServices.DAL.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.Repository
{
    public class ServiceOrderRepository : GenericRepository<ServiceOrder> , IServiceOrderRepository
    {
        public ServiceOrderRepository(ApplicationDbContext context) : base(context)
        {
        
        }
    }
}
