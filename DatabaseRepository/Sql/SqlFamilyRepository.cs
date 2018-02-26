using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Empleados.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRepository.Sql
{
    class SqlFamilyRepository : IFamilyRepository
    {
        private readonly Tarea3Context _db;

        public SqlFamilyRepository(Tarea3Context db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Familiares>> GetEmployeeFamilyAsync(int id)
        {
            return await _db.Familiares.
               Include(f => f.Familiar).
               Where(f => f.IdEmpleado == id).
               AsNoTracking().
               ToListAsync();
        }

        public async Task<Familiares> UpsertAsync(Familiares familyMember)
        {
            
            var current = await _db.Familiares.FindAsync(familyMember.Empleado.IdEmpleado, familyMember.Familiar.IdPersona);
            if (current == null)
            {
                Debug.Print("Insert");
                var familyMemberExists = await _db.Persona.FirstOrDefaultAsync(x => x.IdPersona == familyMember.Familiar.IdPersona);
                if (familyMemberExists == null) _db.Persona.Add(familyMember.Familiar);
                _db.Familiares.Add(familyMember);

            }
            else
            {
                Debug.Print("Update");
                _db.Entry(current).CurrentValues.SetValues(familyMember);
            }
            await _db.SaveChangesAsync();
            return familyMember;
        }
    }
}
