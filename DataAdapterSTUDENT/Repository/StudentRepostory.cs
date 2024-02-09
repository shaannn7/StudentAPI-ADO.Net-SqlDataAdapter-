using DataAdapterSTUDENT.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;

namespace DataAdapterSTUDENT.Repository
{
    public class StudentRepostory : IStudents
    {
        private readonly IConfiguration _configuration;
        public string CString { get; set; }
        public StudentRepostory(IConfiguration configuration)
        {
            _configuration = configuration;
            CString = _configuration["ConnectionStrings:DefaultConnection"];
        }

        public List<Students> GetAllStudents()
        {
            using(SqlConnection conn = new SqlConnection(CString)) 
            {
                SqlDataAdapter DAdapter = new SqlDataAdapter("SELECT * FROM STUDENT7c", conn);
                DataSet DSet = new DataSet();
                DAdapter.Fill(DSet,"STUDENTS");
                List<Students> student = new List<Students>();
                foreach(DataRow dr in DSet.Tables["STUDENTS"].Rows)
                {
                    student.Add
                        (
                            new Students 
                                        {
                                           Id = Convert.ToInt32(dr["STUDENT_ID"]),
                                           Name = dr["STUDENT_NAME"].ToString(),
                                           Mark =Convert.ToInt32(dr["MARK"]),
                                           Status = dr["STUDENT_STATUS"].ToString()
                                        }
                        );
                }
                return student;
            }
        }

        public Students GetStudents(int id)
        {
            using(SqlConnection conn = new SqlConnection(CString))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter($"SELECT * FROM STUDENT7c WHERE STUDENT_ID = {id}", conn);
                DataSet DSet = new DataSet();
                sqlDataAdapter.Fill(DSet,"STUDENTS");

                foreach(DataRow dr in DSet.Tables["STUDENTS"].Rows)
                {
                    if (Convert.ToInt32(dr["STUDENT_ID"]) == id)
                    {
                        return new Students { Id = Convert.ToInt32(dr["STUDENT_ID"]), Name = dr["STUDENT_NAME"].ToString(), Mark = Convert.ToInt32(dr["MARK"]), Status = dr["STUDENT_STATUS"].ToString() };
                    } 
                }

                return null;
            }
        }

        public void UpdateStudents(int id ,Students students)
        {
            using(SqlConnection conn = new SqlConnection(CString))
            {

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter($"SELECT * FROM STUDENT7c WHERE STUDENT_ID = {id}", conn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                DataSet DSet = new DataSet();
                sqlDataAdapter.Fill(DSet, "STUDENTS");

                foreach (DataRow dr in DSet.Tables["STUDENTS"].Rows)
                {
                        dr["STUDENT_NAME"]=students.Name.ToString();
                        dr["MARK"]=Convert.ToInt32(students.Mark);
                        dr["STUDENT_STATUS"]=students.Status.ToString();   
                }
                sqlDataAdapter.Update(DSet,"STUDENTS");
            }
        }
        public void AddStudents(Students students)
        {
            using(SqlConnection conn = new SqlConnection(CString))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM STUDENT7c",conn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "STUDENTS");
                DataRow dr =dataSet.Tables["STUDENTS"].NewRow();
                dr["STUDENT_NAME"]=students.Name.ToString();
                dr["MARK"]=Convert.ToInt32(students.Mark);
                dr["STUDENT_STATUS"]=students.Status.ToString();
                dataSet.Tables["STUDENTS"].Rows.Add(dr);    
                sqlDataAdapter.Update(dataSet,"STUDENTS");
            };
        }

        public void DeleteStudents(int id)
        {
            using (SqlConnection conn = new SqlConnection(CString))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter($"SELECT * FROM STUDENT7c WHERE STUDENT_ID = {id}", conn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet,"STUDENTS");
                DataTable dataTable = dataSet.Tables["STUDENTS"];
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];

                    dataRow.Delete();

                    sqlDataAdapter.Update(dataSet, "STUDENTS");

                    dataSet.AcceptChanges();

                }
            }
            
        }



    }
}
