using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentSoapDB_azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSoapDB_azure.Tests
{
    [TestClass()]
    public class StudentHandlerTests
    {
        [TestMethod()]
        public void GetAllStudentsTest()
        {
            //Arrange

            StudentHandler sh = new StudentHandler();

            //Act

            IList<Student> sList = sh.GetAllStudents();
            string actual;
            if (sList.Count > 0)
            {
                actual = "Ja";
            }
            else
            {
                actual = "Nej";
            }

            //Assert
            Assert.AreEqual("Ja", actual);
        }

        [TestMethod()]
        public void AddStudentTest()
        {
            //Arrange

            StudentHandler sh = new StudentHandler();

            //var newStudent = new Student{StudentId = 100, StudentName = "TONNY"};
            sh.AddStudent(101, "Tonny");

            IList<Student> testStudetnList = sh.GetAllStudents();
            string actual;
            string excepted = "Tonny";

            //Act&Assert
            foreach (var s in testStudetnList)
            {
                if (s.StudentName == "Tonny")
                {
                    actual = s.StudentName;
                    Assert.AreEqual(excepted, actual);
                }

            }

        }

        [TestMethod()]
        public void DeleteStudentByIdTest()
        {
            //Arrange

            StudentHandler sh = new StudentHandler();
            string actual;

            //Act

            sh.DeleteStudentById(5);

            IList<Student> testsList = sh.GetAllStudents();

            //Assert

            foreach (var student in testsList)
            {
                if (student.StudentId == 5)
                {
                    actual = "Denne findes";
                }
                else
                {
                    actual = "Findes ikke";
                }
                Assert.AreEqual("Findes ikke", actual);

            }
        }

        [TestMethod()]
        public void UpdateStudentTest()
        {
            //Arrange

            StudentHandler sh = new StudentHandler();

            sh.UpdateStudent("2","KONNY");

            IList<Student> studentList = sh.GetAllStudents();

            string actual;
            string expected = "KONNY";

            //Act&Assert
            foreach (var stu in studentList)
            {
                if (stu.StudentId== 2)
                {
                    actual = stu.StudentName;
                    Assert.AreEqual(expected,actual);
                }
            }

        }
    }


}