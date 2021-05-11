using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Repositories;
using MISA.CukCuk.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class EmployeeController : DataAccessBaseController<Employee>
    {
        IEmployeeRepository _employeeRepository;
        IEmployeeServices _employeeServices;
        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeServices employeeServices) : base(employeeRepository, employeeServices)
        {
            _employeeRepository = employeeRepository;
            _employeeServices = employeeServices;
        }
        /// <summary>
        /// Lấy dữ liệu phù hợp với kết quả tìm kiếm của người dùng
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns></returns>
        [HttpGet("Search/{searchResult}")]
        public IActionResult GetSearchResult(string searchResult)
        {
            var response = _employeeServices.GetSearchResult(searchResult);
            if (response.Count() > 0) return Ok(response);
            else return NoContent();
        }
        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("Export")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();
            var list = _employeeServices.GetAll();
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong database
        /// </summary>
        /// <returns></returns>
        [HttpGet("MaxCode")]

        public IActionResult GetMaxCode()
        {
            var response = _employeeServices.GetMaxCode();
            if (response != null) return Ok(response);
            else return NoContent();
        }
    }
}
