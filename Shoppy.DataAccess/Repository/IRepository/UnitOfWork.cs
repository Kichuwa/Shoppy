using Shoppy.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppy.DataAccess.Repository.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _db;
        public UnitOfWork(ApplicationDBContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Food = new FoodsRepository(_db);
            MenuItem = new MenuItemRepository(_db);
        }

        public ICategoryRepository Category { get;private set; }

        public IFoodsRepository Food { get; private set; }
        public IMenuItemRepository MenuItem { get; private set; }

        public void Dispose()
        {
             _db.Dispose();
        } 

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
