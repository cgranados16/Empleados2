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
            Debug.Print(familyMember.Empleado.IdEmpleado.ToString());
            
            var current = await _db.Familiares.FindAsync(familyMember.Empleado.IdEmpleado, familyMember.Familiar.IdPersona);
            if (current == null)
            {
                Debug.Print("Insert");
                var existing = await _db.Persona.FirstOrDefaultAsync(x => x.IdPersona == familyMember.Familiar.IdPersona);
                Familiares newFamiliar = new Familiares();
                newFamiliar.IdEmpleado = familyMember.Empleado.IdEmpleado;
                newFamiliar.IdFamiliar = familyMember.Familiar.IdPersona;
                if (existing != null){
                    newFamiliar.Familiar = existing;
                }
                else{
                    newFamiliar.Familiar = familyMember.Familiar;
                }
                newFamiliar.Relacion = familyMember.Relacion;
                _db.Familiares.Add(newFamiliar);
                familyMember = newFamiliar;
            }
            else
            {
                Debug.Print("Update");
                _db.Entry(current).CurrentValues.SetValues(familyMember);
            }
            await _db.SaveChangesAsync();
            return familyMember;
        }

        public async Task DeleteAsync(int IdEmpleado, int IdFamiliar)
        {
            var current = await _db.Familiares.FindAsync(IdEmpleado, IdFamiliar);
            if (null != current)
            {
                _db.Familiares.Remove(current);
                await _db.SaveChangesAsync();
            }
        }
    }
}
