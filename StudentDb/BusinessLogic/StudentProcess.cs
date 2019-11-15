using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentDb.Controllers;
using StudentDb.DataAccess;
using StudentDb.Model;

namespace StudentDb.BusinessLogic
{
    public class StudentProcess
    {
        public List<Student> GetStudents()
        {
            StudentDataAccess dataAccess = new StudentDataAccess();
            var result = dataAccess.getAllStudents();
            List<Student> StudentList = new List<Student>();
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].IsActive == true)
                {
                    StudentList.Add(result[i]);
                }

            }
            return StudentList;
        }
        public Student getById(int id)
        {
            StudentDataAccess dataAccess = new StudentDataAccess();

            return dataAccess.GetByID(id);

        }
        public List<Student> GetDataByNmae(string name)
        {

            var result = GetStudents();
            List<Student> StudentList = new List<Student>();
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].Name.ToLower() == name.ToLower())
                {
                    StudentList.Add(result[i]);
                }
            }
            return StudentList;
        }
        public List<Student> GetByObject(Student student)
        {
            StudentDataAccess dataAccess = new StudentDataAccess();
            return dataAccess.GetByObject(student);
        }

        public int Create(List<Student> student)
        {
            StudentDataAccess dataAccess = new StudentDataAccess();
            return dataAccess.Insert(student);
        }

        public int update(List<Student> student)
        {
            StudentDataAccess dataAccess = new StudentDataAccess();
            return dataAccess.UpdateData(student);
        }
        public int delete(int id)
        {
            StudentDataAccess dataAccess = new StudentDataAccess();
            return dataAccess.deleteData(id);
        }
        public List<int> DeleteMultiple(List<int> list)
        {
            StudentDataAccess dataAccess = new StudentDataAccess();
            return dataAccess.DeleteMultiple(list);
        }
    }
}
