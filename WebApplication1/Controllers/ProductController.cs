using DACN.Core.Entity;
using DACN.Core.IRepository;
using DACN.Core.IService;
using DACN.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository _IProductRepository;

        public ProductController(IProductRepository IProductRepository)
        {
            _IProductRepository = IProductRepository;
        }

        [HttpGet]
        public ResponseModel getAllProduct()
        {
            var res = new ResponseModel();

            res.Data = _IProductRepository.GetAll();

            return res;
        }

        [HttpGet("count")]

        public ResponseModel getCountProduct()
        {
            var res = new ResponseModel();

            res.Data = _IProductRepository.Count();

            return res;
        }

    }
}
