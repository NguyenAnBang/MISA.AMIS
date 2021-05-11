using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository: IDataAccessBaseRepository<Employee>
    {
        //Gán giá trị của departmentName (tên phòng ban) tương ứng với departmentId
        public EmployeeDepartment GetDepartmentName(Guid departmentId);

        //Check trùng mã nhân viên trong database
        public bool CheckDuplicateEmployeeCode(string employeeCode, Guid employeeId, HttpType http);

        //Trả về dữ liệu dựa theo kết quả tìm kiếm theo mã hoặc tên nhân viên
        public IEnumerable<Employee> GetSearchResult(string searchResult);

        //Trả về mã khách hàng lớn nhất trong database
        public string GetMaxCode();
    }
}
