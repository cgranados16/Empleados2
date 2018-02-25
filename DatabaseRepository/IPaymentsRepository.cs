using Empleados.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRepository
{
    public interface IPaymentsRepository
    {
        /// <summary>
        /// Returns all employees. 
        /// </summary>
        Task<IEnumerable<PagosRealizados>> GetAsync();

        /// <summary>
        /// Returns the employees with the given id. 
        /// </summary>
        Task<PagosRealizados> GetAsync(int id);

        Task<IEnumerable<PagosRealizados>> GetEmployeePaymentsAsync(int id);
        

        /// <summary>
        /// Adds a new employee if the customer does not exist, updates the 
        /// existing customer otherwise.
        /// </summary>
        Task<PagosRealizados> UpsertAsync(PagosRealizados customer);
    }
}
