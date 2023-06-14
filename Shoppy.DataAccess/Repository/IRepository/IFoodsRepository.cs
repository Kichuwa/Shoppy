using Shoppy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppy.DataAccess.Repository.IRepository
{
    public interface IFoodsRepository : IRepository<Food> 
    {
        void Update(Food food);
    }
}
