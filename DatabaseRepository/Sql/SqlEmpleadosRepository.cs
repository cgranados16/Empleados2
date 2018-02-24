using Microsoft.EntityFrameworkCore;

namespace DatabaseRepository.Sql
{
    public class SqlEmpleadosRepository : IEmpleadosRepository
    {
        private readonly DbContextOptions<Tarea3Context> _dbOptions;

        public SqlEmpleadosRepository(DbContextOptionsBuilder<Tarea3Context>
            dbOptionsBuilder)
        {
            _dbOptions = dbOptionsBuilder.Options;
            using (var db = new Tarea3Context(_dbOptions))
            {
                db.Database.EnsureCreated();
            }
        }

        public IEmployeeRepository Employees => new SqlEmployeeRepository(
            new Tarea3Context(_dbOptions));
    }
}
