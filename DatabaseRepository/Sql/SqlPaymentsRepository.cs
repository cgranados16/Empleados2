using Empleados.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseRepository.Sql
{
    public class SqlPaymentsRepository : IPaymentsRepository
    {
        private readonly Tarea3Context _db;

        public SqlPaymentsRepository(Tarea3Context db)
        {
            _db = db;
        }

        public async Task<IEnumerable<PagosRealizados>> GetAsync()
        {
            return await _db.PagosRealizados.                
                AsNoTracking().
                ToListAsync();
        }

        public async Task<PagosRealizados> GetAsync(int id)
        {
            return await _db.PagosRealizados
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdEmpleado == id);
        }

        public async Task<IEnumerable<PagosRealizados>> GetEmployeePaymentsAsync(int id)
        {
            return await _db.PagosRealizados
                .Where(x => x.IdEmpleado == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PagosRealizados> UpsertAsync(PagosRealizados payment)
        {
            var current = await _db.PagosRealizados.FirstOrDefaultAsync(x => x.IdEmpleado == payment.IdEmpleado);
            if (null == current)
            {
                _db.PagosRealizados.Add(payment);
            }
            else
            {
                
                _db.Entry(current).CurrentValues.SetValues(payment);
            }
            await _db.SaveChangesAsync();
            return payment;
        }

    }
}
