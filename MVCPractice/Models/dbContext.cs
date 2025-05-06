using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace MVCPractice.Models
{
    public class dbContext
    {

        private readonly IConfiguration _configuration;
        private SqlConnection con;

        public dbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private void Connection()
        {
            string constr = _configuration.GetConnectionString("conn");
            con = new SqlConnection(constr);
        }


        public List<EmployeeModel> GetAllEmployee()
        {
            Connection();
            List<EmployeeModel> EmpList = new List<EmployeeModel>();
            SqlCommand cmd = new SqlCommand("Select_C", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            int i = cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            foreach(DataRow dr in dt.Rows)
            {
                EmpList.Add(
                    new EmployeeModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Age = Convert.ToInt32(dr["Age"]),
                        DOB = Convert.ToDateTime(dr["Dob"]),
                        State = Convert.ToString(dr["State"]),
                        Gender = Convert.ToString(dr["Gender"]),
                    }
                );
            }

            return EmpList;
        }

        public bool Create(EmployeeModel obj)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("insert_C", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
            cmd.Parameters.AddWithValue("@LastName", obj.LastName);
            cmd.Parameters.AddWithValue("@Age", obj.Age);
            cmd.Parameters.AddWithValue("@Dob", obj.DOB);
            cmd.Parameters.AddWithValue("@State", obj.State);
            cmd.Parameters.AddWithValue("@Gender", obj.Gender);

            if (con.State == ConnectionState.Closed) {
                con.Open();
            }
            int i = cmd.ExecuteNonQuery();

            if (i >= 1) { 
            
                return true;
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return false;
        }

        public bool Update(EmployeeModel obj)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("UpdateC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", obj.Id);
            cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
            cmd.Parameters.AddWithValue("@LastName", obj.LastName);
            cmd.Parameters.AddWithValue("@Age", obj.Age);
            cmd.Parameters.AddWithValue("@Dob", obj.DOB);
            cmd.Parameters.AddWithValue("@State", obj.State);
            cmd.Parameters.AddWithValue("@Gender", obj.Gender);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int i = cmd.ExecuteNonQuery();

            if (i >= 1)
            {

                return true;
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return false;
        }


        public bool Delete(int id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("Delete_C_Id", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int i = cmd.ExecuteNonQuery();

            if (i >= 1)
            {

                return true;
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return false;
        }
    }
}
