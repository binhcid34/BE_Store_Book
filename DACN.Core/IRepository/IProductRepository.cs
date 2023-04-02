using DACN.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACN.Core.IRepository
{
    public interface IProductRepository :IBaseRepository<Product>
    {
        public void InsertProduct(Product product) { }

        public void UpdateProduct(Product product) { }
    }
}
