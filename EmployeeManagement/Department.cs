using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeeManagement
{
    public class Department
    {
        /// <summary>
        /// Get all info of all department.
        /// </summary>
        public void GetAllDepartment()
        {
            SqlConnection departmentConnection = ConnectionSetup();
            try
            {
                using (departmentConnection)
                {
                    DepartmentInfoModel displayModel = new DepartmentInfoModel();
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand("spGetAllDept", departmentConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    departmentConnection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            displayModel.DepartmentId = Convert.ToInt32(dr["DEPTNO"]);
                            displayModel.DNAME = dr["DNAME"].ToString();
                            displayModel.Location = dr["LOC"].ToString();

                            //display retrieved record
                            Console.WriteLine("{0},{1},{2}", displayModel.DepartmentId,displayModel.DNAME,displayModel.Location);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    //close data reader
                    dr.Close();

                    departmentConnection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                departmentConnection.Close();
            }
        }

        /// <summary>
        ///  Get Department info By id.
        /// </summary>
        /// <param name="id"></param>
        public void GetDepartment(int id)
        {
            SqlConnection departmentConnection = ConnectionSetup(); 
            try
            {
                using (departmentConnection)
                {
                    DepartmentInfoModel displayModel = new DepartmentInfoModel();
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand("spGetDepartment", departmentConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    departmentConnection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            displayModel.DepartmentId = Convert.ToInt32(dr["DEPTNO"]);
                            displayModel.DNAME = dr["DNAME"].ToString();
                            displayModel.Location = dr["LOC"].ToString();

                            //display retrieved record
                            Console.WriteLine("{0},{1},{2}", displayModel.DepartmentId, displayModel.DNAME, displayModel.Location);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    //close data reader
                    dr.Close();

                    departmentConnection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                departmentConnection.Close();
            }
        }

        /// <summary>
        /// Add new Department.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddDepartment(DepartmentRquestModel model)
        {
            SqlConnection departmentConnection = ConnectionSetup();
            try
            {
                using (departmentConnection)
                {
                    SqlCommand command = new SqlCommand("spRegisterDept", departmentConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DNAME", model.DNAME);
                    command.Parameters.AddWithValue("@LOC", model.Location);
                    departmentConnection.Open();
                    var result = command.ExecuteNonQuery();
                    departmentConnection.Close();
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
                departmentConnection.Close();
            }
        }

        private static SqlConnection ConnectionSetup()
        {
            return new SqlConnection("Data Source=DESKTOP-G47OV5I\\SQLSERVER;Initial Catalog=CompanyDB;Integrated Security=True");
        }
    }
}
