﻿using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace EmployeeManagement
{
    public class Employee
    {
        public Employee(EmployeeModel model)
        {
            this.model = model;
        }
        public Employee()
        {

        }
        static string connectionString = "Data Source=DESKTOP-G47OV5I\\SQLSERVER;Initial Catalog=CompanyDB;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);
        private EmployeeModel model;

        public void AddEmployeeDetailsUsingThreads(EmployeeModel model)
        {
            Stopwatch stopwatch = new Stopwatch();
            Employee newEmployee = new Employee(model);
            Thread thr = new Thread(new ThreadStart(newEmployee.AddEmployee));
            thr.Start();
        }

        /// <summary>
        ///  Add new Employee record to DB.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void AddEmployee()
        {
            try
            {

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spRegisterEmp", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", model.EmployeeName);
                    command.Parameters.AddWithValue("@job", model.JobDiscription);
                    command.Parameters.AddWithValue("@email", model.Email);
                    command.Parameters.AddWithValue("@birthdate", model.BirthDate);
                    command.Parameters.AddWithValue("@hiredate", model.HireDate);
                    command.Parameters.AddWithValue("@DeptNo", model.DepartmentId);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result > 0)
                    {
                        stopwatch.Stop();
                        Console.WriteLine(stopwatch.Elapsed);
                        Console.WriteLine(true);
                    }
                    else
                    {
                        Console.WriteLine(false); 
                    }
                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }


        /// <summary>
        /// Get all employee record.
        /// </summary>
        public void GetAllEmployee()
        {
            try
            {
                using (this.connection)
                {
                    employeeDisplayModel displayModel = new employeeDisplayModel();
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand("spGetAllEmployee", this.connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    this.connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            displayModel.EmployeeId = Convert.ToInt32(dr["EmpId"]);
                            displayModel.EmployeeName = dr["ENAME"].ToString();
                            displayModel.JobDiscription = dr["JOB"].ToString();
                            displayModel.Email = dr["EMAIL"].ToString();
                            displayModel.BirthDate = Convert.ToDateTime(dr["BIRTHdATE"]);
                            displayModel.HireDate = Convert.ToDateTime(dr["HIREDATE"]);
                            displayModel.DepartmentId = Convert.ToInt32(dr["DEPTNo"]);
                            displayModel.departmentName = dr["DNAME"].ToString();
                            displayModel.Location = dr["LOC"].ToString();

                            //display retrieved record
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6}", displayModel.EmployeeId, displayModel.EmployeeName, displayModel.JobDiscription,
                                displayModel.Email, displayModel.departmentName, displayModel.HireDate, displayModel.Location);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    //close data reader
                    dr.Close();

                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <summary>
        /// Get employee by id.
        /// </summary>
        /// <param name="id"></param>
        public void GetEmployee(int id)
        {
            try
            {
                using (this.connection)
                {
                    employeeDisplayModel displayModel = new employeeDisplayModel();
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand("spGetEmployee", this.connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    this.connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            displayModel.EmployeeId = Convert.ToInt32(dr["EmpId"]);
                            displayModel.EmployeeName = dr["ENAME"].ToString();
                            displayModel.JobDiscription = dr["JOB"].ToString();
                            displayModel.Email = dr["EMAIL"].ToString();
                            displayModel.BirthDate = Convert.ToDateTime(dr["BIRTHdATE"]);
                            displayModel.HireDate = Convert.ToDateTime(dr["HIREDATE"]);
                            displayModel.DepartmentId = Convert.ToInt32(dr["DEPTNo"]);
                            displayModel.departmentName = dr["DNAME"].ToString();
                            displayModel.Location = dr["LOC"].ToString();

                            //display retrieved record
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6}", displayModel.EmployeeId, displayModel.EmployeeName, displayModel.JobDiscription,
                                displayModel.Email, displayModel.departmentName, displayModel.HireDate, displayModel.Location);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    //close data reader
                    dr.Close();

                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        public List<employeeDisplayModel> GetAllEmployeeAsPerDate(DateTime initialDate,DateTime EndDate)
        {
            List<employeeDisplayModel> list = new List<employeeDisplayModel>();
            try
            {
                using (this.connection)
                {
                    employeeDisplayModel displayModel = new employeeDisplayModel();
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand("spGetAllEmployeeAsPerDate", this.connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intialDate", initialDate);
                    cmd.Parameters.AddWithValue("@endDate", EndDate);
                    this.connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            displayModel.EmployeeId = Convert.ToInt32(dr["EmpId"]);
                            displayModel.EmployeeName = dr["ENAME"].ToString();
                            displayModel.JobDiscription = dr["JOB"].ToString();
                            displayModel.Email = dr["EMAIL"].ToString();
                            displayModel.BirthDate = Convert.ToDateTime(dr["BIRTHdATE"]);
                            displayModel.HireDate = Convert.ToDateTime(dr["HIREDATE"]);
                            displayModel.DepartmentId = Convert.ToInt32(dr["DEPTNo"]);
                            displayModel.departmentName = dr["DNAME"].ToString();
                            displayModel.Location = dr["LOC"].ToString();

                            //display retrieved record
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6}", displayModel.EmployeeId, displayModel.EmployeeName, displayModel.JobDiscription,
                                displayModel.Email, displayModel.departmentName, displayModel.HireDate, displayModel.Location);
                            Console.WriteLine("\n");
                            list.Add(displayModel);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    //close data reader
                    dr.Close();

                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return list;
        }

        /// <summary>
        /// update employee record.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool updateEmployee(EmployeeModel model,int id)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spUpdateEmployee", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", model.EmployeeName);
                    command.Parameters.AddWithValue("@job", model.JobDiscription);
                    command.Parameters.AddWithValue("@email", model.Email);
                    command.Parameters.AddWithValue("@birthdate", model.BirthDate);
                    command.Parameters.AddWithValue("@hiredate", model.HireDate);
                    command.Parameters.AddWithValue("@DeptNo", model.DepartmentId);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteEmployee(int id)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spDeleteEmployee", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
