using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using Empleados.Models;

namespace DatabaseRepository.Sql
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly Tarea3Context _db;

        public SqlEmployeeRepository(Tarea3Context db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Empleado>> GetAsync()
        {
            return await _db.Empleado.
                AsNoTracking().
                ToListAsync();
        }

        public async Task<Empleado> GetAsync(int id)
        {
            return await _db.Empleado
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdEmpleado == id);
        }

        public Task DeleteAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Empleado> UpsertAsync(Empleado customer)
        {
            throw new NotImplementedException();
        }
    }
}
