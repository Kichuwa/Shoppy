using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppy.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IFoodsRepository Food { get; }
        ICategoryRepository Category { get; }
        IMenuItemRepository MenuItem { get; }
        void Save();
    }
}
