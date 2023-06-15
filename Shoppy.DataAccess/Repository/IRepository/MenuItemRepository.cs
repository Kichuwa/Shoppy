using Shoppy.DataAccess.Data;
using Shoppy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppy.DataAccess.Repository.IRepository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {

        private readonly ApplicationDBContext _db;

        public MenuItemRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(MenuItem obj)
        {
            var objFromDb = _db.MenuItem.FirstOrDefault(u  => u.Id == obj.Id);
            objFromDb.Name = obj.Name;
            objFromDb.Description = obj.Description;
            objFromDb.Price = obj.Price;
            objFromDb.FoodId = obj.FoodId;
            objFromDb.CategoryId = obj.CategoryId;

            //Ensure link is not null to not break image loading.
            if (objFromDb.Image != null)
            {
                objFromDb.Image = obj.Image;
            }

        }
    }
}
