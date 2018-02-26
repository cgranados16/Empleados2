using Empleados.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRepository
{
    public interface IFamilyRepository
    {

        Task<IEnumerable<Familiares>> GetEmployeeFamilyAsync(int id);

        /// <summary>
        /// Adds a new familymember.
        /// </summary>
        Task<Familiares> UpsertAsync(Familiares familyMember);
    }
}
