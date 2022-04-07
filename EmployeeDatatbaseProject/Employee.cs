using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDatatbaseProject
{
    internal class Employee
    {
        public int Num  {get; set;}
        public string Name { get; set; }
        public float Rate { get; set; }
        public string Position { get; set; }

        public Employee()
        {
            Num = 0;
            Name = "Employee Name";
            Rate = 0;
            Position = "Employee Position";
        }

        public Employee(int num, string name, float rate, string position)
        {
            Num = num;
            Name = name;
            Rate = rate;
            Position = position;
        }

        public override string ToString()
        {
            return String.Format("Employee Num={0}, Name={1}, Hourly Rate={2:C}, Position={3}", Num, Name, Rate, Position);
        }
    }
}
