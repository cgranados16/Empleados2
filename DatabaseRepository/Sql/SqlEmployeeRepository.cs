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
                Include(employee => employee.Persona).
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

        public async Task<Empleado> UpsertAsync(Empleado customer)
        {
            var current = await _db.Empleado.FirstOrDefaultAsync(x => x.IdEmpleado == customer.IdEmpleado);
            if (null == current)
            {
                _db.Persona.Add(customer.Persona);
                _db.Empleado.Add(customer);
            }
            else
            {
                var current_persona = await _db.Persona.FirstOrDefaultAsync(x => x.IdPersona == customer.Persona.IdPersona);
                _db.Entry(current_persona).CurrentValues.SetValues(customer.Persona);
                _db.Entry(current).CurrentValues.SetValues(customer);
            }
            await _db.SaveChangesAsync();
            return customer;
        }
    }
}
