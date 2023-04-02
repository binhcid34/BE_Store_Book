using DACN.Core.Entity;
using DACN.Core.IRepository;
using DACN.Core.IService;
using DACN.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository _IEmployeeRepository;
        IEmployeeService _IEmployeeService;

        public EmployeeController(IEmployeeRepository IEmployeeRepository, IEmployeeService IEmployeeService)
        {
            _IEmployeeRepository = IEmployeeRepository;
            _IEmployeeService = IEmployeeService;
        }
        
        /// <summary>
        /// Lấy tất cả bản ghi nhân viên
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel Get()
        {
            ResponseModel response = new ResponseModel();

            response.Data = _IEmployeeRepository.GetAll();
            if (response.Data != null) response.Status = 200;
            else response.Status = 400;
            return response;
        }

        /// <summary>
        /// Lấy danh sách theo tên
        /// </summary>
        /// <returns></returns>
        [HttpGet("name")]
        public ResponseModel GetByName()
        {
            ResponseModel response = new ResponseModel();
            response.Data = _IEmployeeRepository.getEmployeeByName();
            return response;
        }


        /// <summary>
        /// Thêm nhân viên mới
        /// </summary>
        /// <param name="newEmployee"></param>
        [HttpPost("insert")]
        public void Post([FromBody] Employee newEmployee)
        {
            ResponseModel response = new ResponseModel();
            var type = 1;

            _IEmployeeService.insertNewEmployee(response,newEmployee, type);
        }

        /// <summary>
        /// Cập nhật lại dữ liệu
        /// </summary>
        /// <param name="employee"></param>
        [HttpPut("update")]
        public void Update(Employee employee) 
            {
                ResponseModel response = new ResponseModel();
            var type = 2;
                _IEmployeeService.insertNewEmployee(response, employee, type);
            }

       

        // Count Employee
        [HttpGet("count")]
        public ResponseModel GetCount()
        {
            ResponseModel response = new ResponseModel();

            response.Data  = _IEmployeeRepository.Count();
            return response; 
        }

        /// <summary>
        /// Phân trang + lọc dữ liệu nhân viên
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost("filter")]
        public ResponseModel Filter( int pageNumber,  int pageSize, [FromBody] string filter )
        {

            ResponseModel response = new ResponseModel();

            response.Data = _IEmployeeService.filter(filter, pageNumber, pageSize);

            return response;
        }

        /// <summary>
        /// Sắp xếp danh sách theo các cột
        /// </summary>
        /// <param name="listSort"></param>
        /// <returns></returns>
        [HttpPost("sort")]
        public ResponseModel SortByList(string listSort) {
            ResponseModel response = new ResponseModel();

            try
            {
                 _IEmployeeService.sortByList(response,listSort);

                return response;
            }
            catch(Exception ex)
            {
                response.Status = 500;
                response.Message = ex.Message;
                return response ;
            }
           
            
        }


        // Search


        /// <summary>
        /// Xem chi tiết theo mã nhân viên
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpGet("detail")]
        public ResponseModel getDetail (string employeeCode)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                response.Data = _IEmployeeRepository.getDetail(employeeCode);
                if (response.Data != null )
                {
                    response.Success = true;
                    response.Status = 200;
                }
                else
                {
                    response.Success = true;
                    response.Status = 400;
                    response.Message = $"Không có nhân viên nào có mã nhân viên là {employeeCode}";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Upload file excel
        /// </summary>
        /// <param name="fileExcel"></param>
        /// <returns></returns>
        [HttpPost("excel")]
        public ResponseImportModel importExcel (IFormFile fileExcel)
        {
            ResponseImportModel response = new ResponseImportModel();

            _IEmployeeService.importExcel(response, fileExcel);
            return response;
        }

        [HttpDelete("id")]
        public ResponseModel delete(string id)
        {
            ResponseModel response = new ResponseModel();
            try
            {

                _IEmployeeRepository.deleteEmployee(id);
                response.Success = true;
                response.Status = 200;
                return response;
            }
            catch(Exception ex)
            {
                response.Status = 500;
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
