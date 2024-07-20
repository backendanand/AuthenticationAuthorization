using AuthenticationAuthorization.Data;
using AuthenticationAuthorization.Models;
using AuthenticationAuthorization.Repositories.Interfaces;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AuthenticationAuthorization.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;
        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task CreateEmployee(Employee employee)
        {
            string query = @"INSERT INTO Employees (Name, NameInHindi, Gender, GenderInHindi, DateOfBirth, Photo, CardNumber, QrCode) VALUES (@Name, @NameInHindi, @Gender, @GenderInHindi, @DateOfBirth, @Photo, @CardNumber, @QrCode)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("NameInHindi", employee.NameInHindi, DbType.String);
            parameters.Add("Gender", employee.Gender, DbType.String);
            parameters.Add("GenderInHindi", employee.GenderInHindi, DbType.String);
            parameters.Add("DateOfBirth", employee.DateOfBirth, DbType.Date);
            parameters.Add("Photo", employee.Photo, DbType.String);
            parameters.Add("CardNumber", employee.CardNumber, DbType.String);
            parameters.Add("QrCode", employee.QrCode, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<Employee> GetCard(string cardNumber)
        {
            string query = @"SELECT * FROM Employees WHERE CardNumber = @CardNumber";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Employee>(query, new { CardNumber = cardNumber });
            }
        }

        public async Task PrintCard(string cardId)
        {
            throw new NotImplementedException();
        }


    }
}
