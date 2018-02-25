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
        /// Returns all payments. 
        /// </summary>
        Task<IEnumerable<PagosRealizados>> GetAsync();

        /// <summary>
        /// Returns the payments with the given id. 
        /// </summary>
        Task<PagosRealizados> GetAsync(int id);

        Task<IEnumerable<PagosRealizados>> GetEmployeePaymentsAsync(int id);


        /// <summary>
        /// Adds a new payments.
        /// </summary>
        Task<PagosRealizados> UpsertAsync(PagosRealizados customer);
    }
}
