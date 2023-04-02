using DACN.Core.Entity;
using DACN.Core.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IOrderRepository _iOrderRepository;
        public CartController(IOrderRepository iOrderRepository)
        {
            _iOrderRepository = iOrderRepository;
        }   
        [HttpGet]
        public IActionResult GetItems(string userId)
        {
            var res = new ResponseModel();
            res.Data = _iOrderRepository.GetItems(userId);
            return Ok(res);

        }
        [HttpPost]
        public IActionResult AddItem([FromBody] List<Product> orderItems)
        {
            var res = new ResponseModel();
            res.Data = _iOrderRepository.AddItems(orderItems);
            return Ok(res);
        }
    }
}
