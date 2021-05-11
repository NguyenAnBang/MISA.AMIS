using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface IEmployeeServices: IDataAccessBaseServices<Employee>
    {
        //Trả về dữ liệu dựa theo kết quả tìm kiếm theo mã hoặc tên nhân viên
        public IEnumerable<Employee> GetSearchResult(string searchResult);
        //Trả về dữ liệu mã nhân viên lớn nhất trong database
        public string GetMaxCode();
    }
}
