using Shoppy.DataAccess.Data;
using Shoppy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppy.DataAccess.Repository.IRepository
{
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
		private readonly ApplicationDBContext _db;

		public OrderHeaderRepository(ApplicationDBContext db) : base(db)
		{
			_db = db;
		}

		public void Update(OrderHeader orderHeader)
		{
			_db.OrderHeader.Update(orderHeader);
		}

		public void UpdateStatus(int id, string status)
		{
			var orderFromDb = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
			if(orderFromDb != null)
			{
				orderFromDb.Status = status;
			}
		}
	}
}
