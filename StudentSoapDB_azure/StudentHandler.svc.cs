using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StudentSoapDB_azure
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StudentHandler" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select StudentHandler.svc or StudentHandler.svc.cs at the Solution Explorer and start debugging.
    public class StudentHandler : IStudent
    {
        private const string connectionString =
                "Server=tcp:annesazure.database.windows.net,1433;Initial Catalog=EasjDBasw;Persist Security Info=False;User ID=anne55x9;Password=Easj2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";



        public IList<Student> GetAllStudents()
        {
            const string selectAllStudent = "select * from student order by studentid";

            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectAllStudent,databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Student> studentList = new List<Student>();
                        while (reader.Read())
                        {
                            Student student = ReadStudent(reader);
                            studentList.Add(student);
                        }
                        return studentList;
                    }
                }
            }
        }

        private static Student ReadStudent(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            Student student = new Student
            {
                StudentId = id,
                StudentName = name,
            };
            return student;
        }

        public Student GetStudentById(int id)
        {
            const string selectStudent = "select * from student where studentid=@studentid";
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectStudent,databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@studentid", id);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }
                        reader.Read();
                        Student student = ReadStudent((reader));
                        return student;
                    }
                }
            }
        }


        public Student GetStudentByName(string name)
        {
            string selectStr = "select * from student where studentname LIKE @studentname";
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectStr,databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@studentname", name);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }
                        reader.Read();
                        Student st = ReadStudent(reader);
                        return st;


                    }
                }
            }
        }

        public void AddStudent(int id, string name)
        {
            const string insertStudent = "insert into student (studentid, studentname) values (@studentid, @studentname)";
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertStudent, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@studentid", id);
                    insertCommand.Parameters.AddWithValue("@studentname", name);

                    using (SqlDataReader reader = insertCommand.ExecuteReader())
                    {
                        List<Student> studentList = new List<Student>();
                        while (reader.Read())
                        {
                            Student st = ReadStudent(reader);
                            studentList.Add(st);
                        }
                    }
                }
            }
        }
    }
}
