
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ado.netConnectionOrientedExample
{
    public class EmployeeRepository : IEmployeeRepository
    {
        string connectionString = "data source=DESKTOP-AAO14OC;integrated security=yes;Encrypt=True;TrustServerCertificate=True;initial catalog=hotelmanagement";
        public async Task<bool> AddEmployee(Employee empdetail)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Usp_AddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empname", empdetail.empname);
                cmd.Parameters.AddWithValue("@empsalary", empdetail.empsalary);
                con.Open();//we must open the connection manualay
                await cmd.ExecuteNonQueryAsync();
                con.Close();//we must close the connection.
            }
            return true;
        }

        public async Task<bool> DeleteEmployeeByEmpid(int empid)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                SqlCommand cmd = new SqlCommand("Usp_DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", empid);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            return true;
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            List<Employee> lstemp = new List<Employee>();
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Usp_GetEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();//will return results of select statement
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.empid = Convert.ToInt32(reader["empid"]);
                        emp.empname = Convert.ToString(reader["empname"]);
                        emp.empsalary = Convert.ToInt32(reader["empsalary"]);
                        lstemp.Add(emp);
                    }
                    con.Close();
                }
                return lstemp;
            }
        }

        public async Task<Employee> GetEmployeeByEmpid(int empid)
        {
            Employee emp = new Employee();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Usp_GetEmployeeId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", empid);
                SqlDataReader dr = await cmd.ExecuteReaderAsync();
                while (dr.Read())
                {
                    emp.empid = Convert.ToInt32(dr["empid"]);
                    emp.empname = Convert.ToString(dr["empname"]);
                    emp.empsalary = Convert.ToInt32(dr["empsalary"]);
                }
            }
            return emp;
        }

        public async Task<bool> UpdateEmployee(Employee empdetail)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Usp_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", empdetail.empid);
                cmd.Parameters.AddWithValue("@empsalary", empdetail.empsalary);
                cmd.Parameters.AddWithValue("@empname", empdetail.empname);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            return true;
        }
    }
}
