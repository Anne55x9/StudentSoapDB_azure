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
            const string selectAllStudent = "select * from student order by name";

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
                Id = id,
                Name = name,
            };
            return student;
        }

        public Student GetStudentById(int id)
        {
            throw new NotImplementedException();
        }


        public IList<Student> GetStudentsByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
