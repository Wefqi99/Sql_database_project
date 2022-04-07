using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;

namespace EmployeeDatatbaseProject
{
    internal class Program
    {
        public static List<Employee> GetEmployeeFromDatabase(DbCommand cmd)
        {
            List<Employee> results = new List<Employee>();
            cmd.CommandText = "select * from Employees";
            Employee emp;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    emp = new Employee(Convert.ToInt32(reader["ENum"]), Convert.ToString(reader["Name"]), Convert.ToSingle(reader["Rate"]), Convert.ToString(reader["Position"]));
                    results.Add(emp);
                }
            }
            return results;
        }

        public static int GetNextEmpNum(List<Employee> employees)
        {
            int maxId = 0;
            foreach (Employee employee in employees)
            {
                if (employee.Num > maxId)
                {
                    maxId = employee.Num;
                }
            }
            return maxId + 1;
                
        }

        static void Main(string[] args)
        {
            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            List<Employee> employees;


            //Create Connection to the database

            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
            using (DbConnection conn = factory.CreateConnection())
            {
                if (conn == null)
                {
                    Console.WriteLine("Could not connect to the database");
                    Console.ReadLine();
                    return;
                }
                conn.ConnectionString = connectionString;
                conn.Open();

                //This creates how we can issue commands
                DbCommand cmd = conn.CreateCommand();  //How we issue commands in the database

                employees = GetEmployeeFromDatabase(cmd);
                EmployeeWriter.WriteEmployeeToScreen(employees);
                Console.ReadLine();

                //Inserting a new record
                //This will ask the user to add an empoyee and add it to the table
                Console.Write("Enter a new Employee: ");
                string employeeName = Console.ReadLine();
                Console.Write("Enter the hourly rate: ");
                double hourlyRate = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter employee position: ");
                string pos = Console.ReadLine();

                int newNum = GetNextEmpNum(employees);

                string query = String.Format("insert into Employees values ({0},'{1}',{2:F2},'{3}')", newNum, employeeName, hourlyRate, pos);
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                Console.WriteLine("Here is the result after adding the new empolyee: ");
                employees = GetEmployeeFromDatabase(cmd);
                EmployeeWriter.WriteEmployeeToScreen(employees);
                Console.ReadLine();

                Console.Write("Enter the name of the employee you want to delete: ");
                string deleteEmployee = Console.ReadLine();
                Console.WriteLine("After deleting the first employee...");
                cmd.CommandText = String.Format("delete from Employees where Name = '{0}'", deleteEmployee);
                cmd.ExecuteNonQuery();
                employees = GetEmployeeFromDatabase(cmd);
                EmployeeWriter.WriteEmployeeToScreen(employees);




            }
        }
    }
}
