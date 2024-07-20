using AuthenticationAuthorization.Models;

namespace AuthenticationAuthorization.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task CreateEmployee(Employee employee);
        Task<Employee> GetCard(string cardNumber);
        Task PrintCard(string cardId);
    }
}
