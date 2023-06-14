using Shoppy.DataAccess.Data;
using Shoppy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppy.DataAccess.Repository.IRepository
{
    public class FoodsRepository : Repository<Food>, IFoodsRepository
    {

        private readonly ApplicationDBContext _db;

        public FoodsRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Food food)
        {
            var objFromDb = _db.Food.FirstOrDefault(u  => u.Id == food.Id);
            objFromDb.Name = food.Name;
        }
    }
}
