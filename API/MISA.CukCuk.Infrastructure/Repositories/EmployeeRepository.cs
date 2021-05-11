using Dapper;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Enums;
using MISA.CukCuk.Core.Interfaces.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.CukCuk.Infrastructure.Repositories
{
    public class EmployeeRepository : DataAccessBaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <param name="employeeId"></param>
        /// <param name="http"></param>
        /// <returns>True: nếu trùng</returns>
        /// <returns>False: nếu không trùng</returns>
        public bool CheckDuplicateEmployeeCode(string employeeCode, Guid employeeId, HttpType http)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "";
                DynamicParameters dynamicParameters = new DynamicParameters();
                if (http == HttpType.POST)
                {
                    sqlCommand = "Proc_CheckEmployeeCodeExist";
                    dynamicParameters.Add("EmployeeCode", employeeCode);
                }
                else
                {
                    //Check trùng mã khách hàng
                    sqlCommand = "Proc_CheckEmployeeCodeExistPut";
                    dynamicParameters.Add("EmployeeCode", employeeCode);
                    dynamicParameters.Add("Id", employeeId);
                }
                var Exists = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return Exists;
            }
        }

        /// <summary>
        /// Lấy phòng ban nhân viên dựa trên id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>dữ liệu phòng ban từ bảng Department</returns>
        public EmployeeDepartment GetDepartmentName(Guid departmentId)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "Proc_GetDepartmentById";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("departmentId", departmentId);
                
                var department = dbConnection.QueryFirstOrDefault<EmployeeDepartment>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return department;
            }
        }
        /// <summary>
        /// Trả về mã nhân viên lớn nhất trong database
        /// </summary>
        /// <returns>EmployeeCode</returns>
        public string GetMaxCode()
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "Proc_GetMaxEmployeeCode";

                var response = dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        /// <summary>
        /// Trả về dữ liệu dựa theo kết quả tìm kiếm theo mã hoặc tên nhân viên
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns>List dữ liệu phù hợp với kết quả search</returns>
        public IEnumerable<Employee> GetSearchResult(string searchResult)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "Proc_SearchEmployee";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("SearchField", searchResult);

                var response = dbConnection.Query<Employee>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return response;
            }
        }
    }
}
