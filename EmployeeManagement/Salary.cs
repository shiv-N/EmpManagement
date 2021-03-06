﻿using EmployeeManagement.Model.SalaryModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeeManagement
{
    public class Salary
    {
        /// <summary>
        /// Add Emp Salary
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddSalary(SalaryRquestModel model)
        {
            SqlConnection SalaryConnection = ConnectionSetup();
            try
            {
                using (SalaryConnection)
                {
                    SqlCommand command = new SqlCommand("spRecordSalary", SalaryConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@month", model.Month);
                    command.Parameters.AddWithValue("@Salary", model.EmployeeSalary);
                    command.Parameters.AddWithValue("@empId", model.EmployeeId);
                    SalaryConnection.Open();
                    var result = command.ExecuteNonQuery();
                    SalaryConnection.Close();
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
                SalaryConnection.Close();
            }
        }

        /// <summary>
        /// Display all emp Salary
        /// </summary>
        public void DisplayAllSalaryDetail()
        {
            SqlConnection SalaryConnection = ConnectionSetup();
            try
            {
                using (SalaryConnection)
                {
                    SalaryDetailModel displayModel = new SalaryDetailModel();
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand("spGetAllSalaryDetail", SalaryConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SalaryConnection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            displayModel.EmployeeId = Convert.ToInt32(dr["EmpId"]);
                            displayModel.EmployeeName = dr["ENAME"].ToString();
                            displayModel.JobDiscription = dr["JOB"].ToString();
                            displayModel.EmployeeSalary = Convert.ToInt32(dr["EMPSAL"]);
                            displayModel.Month = dr["SAL_MONTH"].ToString();
                            displayModel.SalaryId = Convert.ToInt32(dr["SALARYId"]);

                            //display retrieved record
                            Console.WriteLine("{0},{1},{2}", displayModel.EmployeeName, displayModel.EmployeeSalary, displayModel.Month);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    //close data reader
                    dr.Close();

                    SalaryConnection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                SalaryConnection.Close();
            }
        }

        public double DisplayAverageSalaryDetail()
        {
            double AverageSalary = 0;
            SqlConnection SalaryConnection = ConnectionSetup();
            try
            {
                using (SalaryConnection)
                {
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand("spGetAvgSalary", SalaryConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SalaryConnection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            AverageSalary = Convert.ToDouble(dr["AvgSalary"]);

                            //display retrieved record
                            Console.WriteLine("Average salary = {0}",AverageSalary);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    //close data reader
                    dr.Close();

                    SalaryConnection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                SalaryConnection.Close();
            }
            return AverageSalary;
        }

        public int DisplayMinimumSalaryDetail()
        {
            int minimumSalary = 0;
            SqlConnection SalaryConnection = ConnectionSetup();
            try
            {
                using (SalaryConnection)
                {
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand("spGetMINSalary", SalaryConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SalaryConnection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            minimumSalary = Convert.ToInt32(dr["MinimumSalary"]);

                            //display retrieved record
                            Console.WriteLine("Minimum salary = {0}", minimumSalary);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    //close data reader
                    dr.Close();

                    SalaryConnection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                SalaryConnection.Close();
            }
            return minimumSalary;
        }

        public int UpdateEmployeeSalary(SalaryUpdateModel model)
        {
            SqlConnection SalaryConnection = ConnectionSetup();
            int salary = 0;
            try
            {
                using (SalaryConnection)
                {
                    SalaryDetailModel displayModel = new SalaryDetailModel();
                    //define the SqlCommand object
                    SqlCommand command = new SqlCommand("spUpdateEmployeeSalary", SalaryConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", model.SalaryId);
                    command.Parameters.AddWithValue("@month", model.Month);
                    command.Parameters.AddWithValue("@salary", model.EmployeeSalary);
                    command.Parameters.AddWithValue("@EmpId", model.EmployeeId);
                    SalaryConnection.Open();

                    SqlDataReader dr = command.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            displayModel.EmployeeId = Convert.ToInt32(dr["EmpId"]);
                            displayModel.EmployeeName = dr["ENAME"].ToString();
                            displayModel.JobDiscription = dr["JOB"].ToString();
                            displayModel.EmployeeSalary = Convert.ToInt32(dr["EMPSAL"]);
                            displayModel.Month = dr["SAL_MONTH"].ToString();
                            displayModel.SalaryId = Convert.ToInt32(dr["SALARYId"]);

                            //display retrieved record
                            Console.WriteLine("{0},{1},{2}", displayModel.EmployeeName, displayModel.EmployeeSalary, displayModel.Month);
                            Console.WriteLine("\n");
                            salary = displayModel.EmployeeSalary;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    //close data reader
                    dr.Close();

                    SalaryConnection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                SalaryConnection.Close();
                
            }
            return salary;
        }

        private static SqlConnection ConnectionSetup()
        {
            return new SqlConnection("Data Source=DESKTOP-G47OV5I\\SQLSERVER;Initial Catalog=CompanyDB;Integrated Security=True");
        }
    }

    
}
