using EmployeeManagement;
using EmployeeManagement.Model;
using EmployeeManagement.Model.SalaryModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHamcrest.Core;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace EmployeeManagementTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GivenSalaryDetails_AbleToUpdateSalaryDetails()
        {
            //Arrange
            Salary salary = new Salary();
            SalaryUpdateModel updateModel = new SalaryUpdateModel()
            {
                SalaryId = 2,
                Month = "Jan",
                EmployeeSalary = 1300,
                EmployeeId = 5
            };

            //Act
            int EmpSalary = salary.UpdateEmployeeSalary(updateModel);

            //Assert
            Assert.AreEqual(updateModel.EmployeeSalary, EmpSalary);
        }


        [TestMethod]
        public void GivenInitialAndEnddate_AbleToReturnEmployeeDetails()
        {
            //Arrange
            Employee employee = new Employee();
            DateTime InitialDate = FormateDateTime("2019-01-30");
            DateTime EndDate = FormateDateTime("2019-02-05");

            //Act
            List<employeeDisplayModel> list = employee.GetAllEmployeeAsPerDate(InitialDate, EndDate);

            foreach(employeeDisplayModel model in list)
            {
                if(InitialDate <= model.HireDate && EndDate >= model.HireDate)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsTrue(false);
                }
            }
        }

        public static DateTime FormateDateTime(string date)
        {
            CultureInfo culture = new CultureInfo("en-US");
            DateTime result = DateTime.ParseExact(date, "yyyy-MM-dd", culture, DateTimeStyles.None);
            return result;
        }

        [TestMethod]
        public void Return_AverageOfEmployeeSalary()
        {
            Salary salary = new Salary();

            //Act
            double AvergeSalary = salary.DisplayAverageSalaryDetail();

            //Assert
            Assert.AreEqual(1762.50, AvergeSalary);
        }

        [TestMethod]
        public void Return_MinimumEmployeeSalary()
        {
            Salary salary = new Salary();

            //Act
            int minimumSalary = salary.DisplayMinimumSalaryDetail();

            //Assert
            Assert.AreEqual(1300, minimumSalary);
        }

        
    }
}
