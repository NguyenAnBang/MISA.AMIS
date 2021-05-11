using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Enums;
using MISA.CukCuk.Core.Exceptions;
using MISA.CukCuk.Core.Interfaces.Repositories;
using MISA.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MISA.CukCuk.Core.Services
{
    public class EmployeeServices : DataAccessBaseServices<Employee>, IEmployeeServices
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeServices(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        //Validate dữ liệu khi lấy dữ liệu về từ database
        protected override void FormatData(Employee entity)
        {

            //base.Validate(entity); //Nhận mã từ base
            if (entity is Employee) //Ép kiểu
            {
                var employee = entity as Employee;

                //Xử lý giới tính trả về GenderName trả về phía client
                if (employee.Gender == 0) employee.GenderName = "Nữ";
                else if (employee.Gender == 1) employee.GenderName = "Nam";
                else employee.GenderName = "Khác";

                //Xử lý trả về tên phòng ban trả về phía client
                var department = _employeeRepository.GetDepartmentName(employee.DepartmentId);
                if (department == null) employee.DepartmentName = null;
                else employee.DepartmentName = department.DepartmentName;   

            }
        }
        //Validate dữ liệu khi post lên database
        protected override void CustomValidate(Employee entity, HttpType http)
        {
            if(entity is Employee)
            {
                var employee = entity as Employee;
                //Kiểm tra mã nhân viên đã tồn tại chưa
                var isEmployeeCodeExists = _employeeRepository.CheckDuplicateEmployeeCode(employee.EmployeeCode, employee.EmployeeId, http);
                if (isEmployeeCodeExists == true)
                {
                    throw new CustomerException(Properties.Resources.Employee_Code_Exists_msg);
                }

            }
        }

        //Validate dữ liệu nhập vào ô tìm kiếm của client
        public IEnumerable<Employee> GetSearchResult(string searchResult)
        {
            //Kiểm tra giá trị
            if (!Regex.IsMatch(searchResult.ToString(), @"([A-Za-z0-9_-]+)" /*Properties.Resources.Regex_email*/))
            {
                var response = new
                {
                    devMsg = Properties.Resources.Search_input_invalid_characters,
                    MISACode = Properties.Resources.MISACode
                };
                throw new CustomerException(response.devMsg);
            }
            else
            {
                var response = _employeeRepository.GetSearchResult(searchResult);
                foreach(var entity in response)
                {
                    FormatData(entity);
                }
                
                return response;
            }
        }
        //Trả về mã nhân viên lớn nhất, validate để trả về dạng "NV-0000"
      
        public string GetMaxCode()
        {
            var response = _employeeRepository.GetMaxCode();
            int number = Int32.Parse(response);
            number += 1;
            var result = number.ToString();
            //Vì trong database đang để mã nhân viên là dạng NV-0000, trong trường hợp khác phải nghĩ đến cách làm khác
            if (number < 10) result = Properties.Resources.NV_000 + result;
            else if (10 <= number && number < 100) result = Properties.Resources.NV_00 + result;
            else if (100 <= number && number < 1000) result = Properties.Resources.NV_0 + result;
            else result = Properties.Resources.NV_ + result;
            return result;
        }
    }
}
