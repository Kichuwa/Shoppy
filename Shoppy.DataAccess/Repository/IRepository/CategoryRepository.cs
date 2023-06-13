using Shoppy.DataAccess.Data;
using Shoppy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoppy.DataAccess.Repository.IRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDBContext _db;

        public CategoryRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category category)
        {
            var objFromDb = _db.Category.FirstOrDefault(u  => u.Id == category.Id);
            objFromDb.Name = category.Name;
            objFromDb.DisplayOrder = category.DisplayOrder;
        }
    }
}
