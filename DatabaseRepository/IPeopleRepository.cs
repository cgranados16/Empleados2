using Empleados.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseRepository
{
    public interface IPeopleRepository
    {
        /// <summary>
        /// Returns all people. 
        /// </summary>
        Task<IEnumerable<Persona>> GetAsync();

        /// <summary>
        /// Returns the people with the given id. 
        /// </summary>
        Task<Persona> GetAsync(int id);

        /// <summary>
        /// Adds a new people if the customer does not exist, updates the 
        /// existing customer otherwise.
        /// </summary>
        Task<Persona> UpsertAsync(Persona people);

    }
}
