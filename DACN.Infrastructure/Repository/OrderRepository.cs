using DACN.Core.Entity;
using DACN.Core.IRepository;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace DACN.Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<SessionOrder>, IOrderRepository
    {
        public Boolean AddItems(List<Product> orderItems)
        {
            string userId = "cefe8f16-c744-11ed-9a0d-d8d09038cbd3";
            var sqlConnector = new MySqlConnection(connectString);
            try
            {
                var orderDetail = JsonSerializer.Serialize(orderItems).ToString();
                if (CheckOrderExist(userId))
                {
                    var totalAmount = CalculationItem(orderItems);
                    var sqlQuery = "Update SessionOrder " +
                                   $"Set OrderDetail = '{orderDetail}', TotalPayment = '{totalAmount}'" +
                                   $"Where IdUser = '{userId}'";
                    var res = sqlConnector.Query(sqlQuery);
                }
                else
                {
                    var queryProc = "Proc_Insert_SessionOrder";
                    var parameters = new DynamicParameters();
                    parameters.Add("v_OrderDetail", orderDetail);
                    parameters.Add("v_IdUser", userId);
                    parameters.Add("v_DiscountCombo", 1);
                    parameters.Add("v_TotalPayment", CalculationItem(orderItems));
                    parameters.Add("v_PaymentType", 1);
                    parameters.Add("v_PaymentStatus", 1);
                    parameters.Add("v_LastTime", DateTime.Today);
                    parameters.Add("v_PaymentFee", 30000);

                    sqlConnector.Query(queryProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public SessionOrder GetItems(string userId)
        {
            userId = "1cee3429-c5a8-11ed-9a0d-d8d09038cbd3";
            var sqlConnector = new MySqlConnection(connectString);
            var sqlQuery = $"Select * from SessionOrder where IdUser = '{userId}'";
            var res = sqlConnector.Query(sqlQuery).FirstOrDefault();
            if(res != null)
            {
                var sessionOrder = new SessionOrder();
                sessionOrder.IdSessionOrder = res.IdSessionOrder;
                sessionOrder.PaymentStatus = res.PaymentStatus;
                sessionOrder.DiscountCombo = res.DiscountCombo; 
                sessionOrder.TotalPayment = res.TotalPayment;
                sessionOrder.PaymentType = res.PaymentType;
                sessionOrder.PaymentFee = res.PaymentFee;
                sessionOrder.OrderDetail = JsonSerializer.Deserialize<List<Product>>(res.OrderDetail.ToString());
                return sessionOrder;
            }
            return null;
        }

        private Boolean CheckOrderExist(string userId)
        {
            var sqlConnector = new MySqlConnection(connectString);
            var sqlQuery = $"Select * from SessionOrder where IdUser = '{userId}'";
            var res = sqlConnector.Query(sqlQuery);
            if(res.Count() > 0)
            {
                return true;
            }
            return false;
        }

        private static float CalculationItem(List<Product> orderItems)
        {
            var totalAmount = 0;
            foreach (Product product in orderItems)
            {
                totalAmount += product.PriceProduct * product.Quantity;
            }
            return totalAmount;
        }
    }
}
