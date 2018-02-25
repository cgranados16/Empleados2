using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseRepository
{
    public interface IEmpleadosRepository
    {
        IEmployeeRepository Employees { get; }
        IPeopleRepository People { get; }
        IPaymentsRepository Payments { get; }

    }
}
