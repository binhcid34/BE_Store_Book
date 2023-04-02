using DACN.Core.Entity;
using DACN.Core.IRepository;
using DACN.Core.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("ShouldContainRole")]
    public class AdminController : ControllerBase
    {
        IUserRepository _IUserRepository;
        IUserService _IUserService;

        public AdminController(IUserRepository IUserRepository, IUserService userService)
        {
            _IUserRepository = IUserRepository;
            _IUserService= userService;
        }

        /// <summary>
        ///  Lấy toàn bộ thông tin account
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel getAllAccount()
        {
            var response = new ResponseModel();

            try
            {
                response.Data = _IUserRepository.GetAll();
            }
            catch(Exception ex) {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Thêm tài khoản
        /// </summary>
        /// <returns></returns>
        [HttpPost("insert")]
        public ResponseModel insertAccount(User newUser) {
            var response = new ResponseModel();
            try
            {
                _IUserService.InsertUser(newUser, response);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <returns></returns>
        [HttpPost("search/{search}")]
        public ResponseModel searchByEmail(string search )
        {
            var response = new ResponseModel();
            try
            {
                response.Data  = _IUserRepository.searchByEmail(search);
                response.Success = true;
                response.Status =   200;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        /// <summary>
        /// Xóa nhân viên theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpDelete("{email}")]
        public ResponseModel insertAccount(string email )
        {
            var response = new ResponseModel();
            try
            {
                _IUserRepository.DeleteUser(email);

                response.Message = "Đã xóa thành công";

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Xóa toàn bộ nhưng đang fix cứng là xóa 10 bản ghi , Không nên test
        /// </summary>
        /// <returns></returns>
        [HttpDelete("deleteAll")]
        public ResponseModel DeleteAll ()
        {
            var response = new ResponseModel();
            try
            {
                _IUserRepository.DeleteAll();

                response.Message = "Đã xóa thành công";

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
