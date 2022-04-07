using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDatatbaseProject
{
    internal class EmployeeWriter
    {
        public static void WriteEmployeeToScreen(List<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                Console.WriteLine(employee);
            }
        }
    }
}
