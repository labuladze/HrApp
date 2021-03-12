using HrApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrApp.Data
{
    public interface IData
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        bool CreateEmpl(Employee empl );
        bool UpdateEmpl(int id, Employee empl);
        bool DeleteEmpl(int id);

    }
}
