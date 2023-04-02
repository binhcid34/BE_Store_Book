using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACN.Core.Entity
{
    public class SessionOrder
    {
        public string? IdSessionOrder { get; set; }
        public List<Product>? OrderDetail { get; set; }
        public string? IdUser { get; set; }
        public int Status { get; set; } //  0: chưa thanh toán, 1: Đang vận chuyển, 2: Thành công ,3: Bị lỗi
        public int DiscountCombo { get; set; }
        public float TotalPayment { get; set; }
        public int PaymentStatus { get; set; } // 0 = chưa thanh toán, 1 : Đã thanh toán, 2 : Chờ chuyển khoản
        public int PaymentFee { get; set; } // phí vẩn chuyển
        public int PaymentType { get; set; } // cách thanh toán:// 1: chuyển tiền mặt , 2 : chuyển khoản
    }
}
