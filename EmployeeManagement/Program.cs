﻿using EmployeeManagement.Model;
using EmployeeManagement.Model.SalaryModel;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace EmployeeManagement
{
    class Program
    {
        static void Main(string[] args)
        {   
            //// Add emp
            //Employee employee = new Employee();
            EmployeeModel employeeOne = new EmployeeModel()
            {
                EmployeeName = "Manubwei",
                JobDiscription = "CLERK",
                Email = "mawexxbi@gmail.com",
                BirthDate = FormateDateTime("1994-12-02"),
                HireDate = FormateDateTime("2019-05-02"),
                DepartmentId = 2
            };
            //bool result = employee.AddEmployee(employeeOne);
            //Console.WriteLine(result);
            Stopwatch stopwatch = new Stopwatch();
            Employee employee = new Employee(employeeOne);
            //employee.AddEmployee();
            employee.AddEmployeeDetailsUsingThreads(employeeOne);
            //Thread thread = new Thread(new ThreadStart(employee.AddEmployee(employeeOne))


            //bool result = employee.updateEmployee(employeeOne,14);

            // Add salary
            Salary salary = new Salary();
            //salary.DisplayAllSalaryDetail();
            //SalaryRquestModel salaryRquest = new SalaryRquestModel()
            //{
            //    Month = "Feb",
            //    EmployeeSalary = 1700,
            //    EmployeeId = 15
            //};
            //bool a = salary.AddSalary(salaryRquest);
            //Console.WriteLine(a);
            

            ////Add DEPT
            //Department department = new Department();
            //department.GetDepartment(2);
            //DepartmentRquestModel newDepartment = new DepartmentRquestModel()
            //{
            //    DNAME = "COMPUTER",
            //    Location = "new jersey"
            //};
            //department.AddDepartment(newDepartment);


        }
        
        public static DateTime FormateDateTime(string date)
        {
            CultureInfo culture = new CultureInfo("en-US");
            DateTime result = DateTime.ParseExact(date, "yyyy-MM-dd", culture, DateTimeStyles.None);
            return result;
        }
    }
}
