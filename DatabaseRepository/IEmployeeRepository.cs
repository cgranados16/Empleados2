using Empleados.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseRepository
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Returns all employees. 
        /// </summary>
        Task<IEnumerable<Empleado>> GetAsync();


        /// <summary>
        /// Returns the employees with the given id. 
        /// </summary>
        Task<Empleado> GetAsync(int id);

        /// <summary>
        /// Adds a new employee if the customer does not exist, updates the 
        /// existing customer otherwise.
        /// </summary>
        Task<Empleado> UpsertAsync(Empleado customer);

        /// <summary>
        /// Deletes a employee.
        /// </summary>
        Task DeleteAsync(int customerId);
    }
}
