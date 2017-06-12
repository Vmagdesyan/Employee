using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeList.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string NamePost { get; set; }
        public decimal Salary { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}