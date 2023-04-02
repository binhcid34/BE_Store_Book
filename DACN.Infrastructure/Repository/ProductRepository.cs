using DACN.Core.Entity;
using DACN.Core.IRepository;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACN.Infrastructure.Repository
{
    public class ProductRepository :BaseRepository<Product>, IProductRepository
    {
        public ProductRepository() : base() { }

        public void InsertProduct(Product product)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var queryProc = $"Proc_Insert_Product";

            var parameters = new DynamicParameters();
            parameters.Add("v_IdProduct", product.IdProduct);
            parameters.Add("v_IdCategory", product.IdCategory);
            parameters.Add("v_NameProduct", product.NameProduct);
            parameters.Add("v_TitleProduct", product.TitleProduct);
            parameters.Add("v_DescriptionProduct", product.DescriptionProduct);
            parameters.Add("v_QuantitySock", product.QuantitySock);
            parameters.Add("v_QuantitySold", product.QuantitySold);
            parameters.Add("v_PriceProduct", product.PriceProduct);
            parameters.Add("v_ImageProduct", product.ImageProduct);

            // connect db

            // query
            sqlConnector.Query(queryProc, param : parameters, commandType: System.Data.CommandType.StoredProcedure);

        }

        public void UpdateProduct(Product product)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var queryProc = $"Proc_Update_Product";

            var parameters = new DynamicParameters();
            parameters.Add("v_IdProduct", product.IdProduct);
            parameters.Add("v_IdCategory", product.IdCategory);
            parameters.Add("v_NameProduct", product.NameProduct);
            parameters.Add("v_TitleProduct", product.TitleProduct);
            parameters.Add("v_DescriptionProduct", product.DescriptionProduct);
            parameters.Add("v_QuantitySock", product.QuantitySock);
            parameters.Add("v_QuantitySold", product.QuantitySold);
            parameters.Add("v_PriceProduct", product.PriceProduct);
            parameters.Add("v_ImageProduct", product.ImageProduct);

            sqlConnector.Query(queryProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

        }

        
        public IEnumerable<Product> SearchProduct(string search)
        {
            var sqlConnector = new MySqlConnection(base.connectString);

            var likeQuery = '%' + search + '%';
            var querySQL = $"Select * from product where product.NameProduct like '{likeQuery}' or product.TitleProduct like '{likeQuery}' or product.DescriptionProduct like '{likeQuery}'";

            var res = sqlConnector.Query<Product>(querySQL);
            return res;
        }
    }
}
