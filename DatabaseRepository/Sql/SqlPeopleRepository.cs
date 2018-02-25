using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Empleados.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRepository.Sql
{
    class SqlPeopleRepository : IPeopleRepository
    {

        private readonly Tarea3Context _db;

        public SqlPeopleRepository(Tarea3Context db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Persona>> GetAsync()
        {
            return await _db.Persona.
                AsNoTracking().
                ToListAsync();
        }

        public async Task<Persona> GetAsync(int id)
        {
            return await _db.Persona
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdPersona == id);
        }

        public async Task<Persona> UpsertAsync(Persona people)
        {
            var current = await _db.Persona.FirstOrDefaultAsync(x => x.IdPersona == people.IdPersona);
            if (null == current)
            {
                _db.Persona.Add(people);
            }
            else
            {
                _db.Entry(current).CurrentValues.SetValues(people);
            }
            await _db.SaveChangesAsync();
            return people;
        }
    }
}
