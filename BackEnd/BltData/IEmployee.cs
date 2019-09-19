using BltData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BltData
{
    public interface IEmployee
    {
        IEnumerable<Employee> GetAll();
        Employee GetById (int id);
    }
}
