using DACN.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACN.Core.IRepository
{
    public interface IOrderRepository :IBaseRepository<SessionOrder>
    {
        public Boolean AddItems(List<Product> orderItems);
        public SessionOrder GetItems(string userID);
    }
}
