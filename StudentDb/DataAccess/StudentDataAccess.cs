using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentDb.Model;
using StudentDb.Controllers;
using StudentDb.BusinessLogic;
using Npgsql;

namespace StudentDb.DataAccess
{
    public class StudentDataAccess
    {
        private string connectionString = "Server=localhost; User Id=postgres;" +
            "Password= ram0978;Database=customerdb";
        //function to fetch all data from stuudent Table
        public List<Student> getAllStudents()
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"select * from student";
            NpgsqlDataReader dataReader = command.ExecuteReader();
            List<Student> StudentList = new List<Student>();
            while (dataReader.Read())
            {
                Student student = new Student();

                student.RollNumber = Int32.Parse(dataReader[0].ToString());
                student.Name = dataReader[1].ToString();
                student.Branch = dataReader[2].ToString();
                student.IsActive = Convert.ToBoolean(dataReader[3].ToString());
                StudentList.Add(student);

            }
            connection.Close();
            return StudentList;

        }
        public Student GetByID(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"select * from student where rollnumber = " + id + "";
            NpgsqlDataReader dataReader = command.ExecuteReader();
            Student student = new Student();
            while (dataReader.Read())
            {

                student.RollNumber = Int32.Parse(dataReader[0].ToString());
                student.Name = dataReader[1].ToString();
                student.Branch = dataReader[2].ToString();
                student.IsActive = Convert.ToBoolean(dataReader[3].ToString());

            }
            return student;

        }
        public List<Student> GetByObject(Student student)
        {
            List<Student> studentList = new List<Student>();
            int id = student.RollNumber;
             if(id > 0)
            {
                var result = GetByID(id);
                studentList.Add(result);
                return studentList;
            }
            if (student.Name != null && student.Branch != null)
            {
                studentList = GetByNameAndBranch(student.Name, student.Branch);
                return studentList;
            }
            if(student.Name != null)
            {
                studentList = GetDataByNmae(student.Name);
            }
            if(student.Branch != null)
            {
                studentList = GetByBranch(student.Branch);
            }
            if (student.IsActive == true)
            {
                studentList = getAllStudents();
                
            }
            return studentList;


        }
        public List<Student> GetDataByNmae(string name)
        {

            var result = getAllStudents();
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
        public List<Student> GetByBranch(string branch)
        {

            var result = getAllStudents();
            List<Student> StudentList = new List<Student>();
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].Branch.ToLower() == branch.ToLower())
                {
                    StudentList.Add(result[i]);
                }
            }
            return StudentList;
        }
        public List<Student> GetByNameAndBranch(string name, string branch)
        {
            List<Student> studentList = new List<Student>();
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var commmand = connection.CreateCommand();
            commmand.CommandText = @" select * from student where (name = '" + name + "' And branch = '" + branch + "') ";
            NpgsqlDataReader dataReader = commmand.ExecuteReader();
            if(dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    Student student = new Student();
                    student.RollNumber = Int32.Parse(dataReader[0].ToString());
                    student.Name = dataReader[1].ToString();
                    student.Branch = dataReader[2].ToString();
                    student.IsActive = Convert.ToBoolean(dataReader[3].ToString());
                    studentList.Add(student);
                }
                
            }

            return studentList;

        }
        //insert new data into student table
        public int Insert(List<Student> student)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            int Number = count();
            bool Active = true;
            int result = 0;
            for (int i = 0; i < student.Count; i++)
            {
                command.CommandText = @"insert into student(RollNumber,Name, Branch, IsActive)values
                               (" + (Number + i) + ",'" + student[i].Name.ToLower() + "', '" + student[i].Branch.ToLower() + "'," +
                              " " + Active + ")";
                result = command.ExecuteNonQuery();

            }
            return result;
        }
        // return the total row count
        public int count()
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var commandCount = connection.CreateCommand();
            commandCount.CommandText = @"select count(*) from student";
            int totalRows = Convert.ToInt32(commandCount.ExecuteScalar());
            return totalRows + 1;
        }

        // updatec the existing record with refrence to id
        public int UpdateData(List<Student> student)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            int result = 0;
            var command = connection.CreateCommand();
            for (int i = 0; i < student.Count; i++)
            {
                command.CommandText = @" update student set name='" + student[i].Name + "', branch='" + student[i].Branch + "', isActive = " + student[i].IsActive + " where rollnumber = " + student[i].RollNumber + "";
                result = command.ExecuteNonQuery();

            }
            return result;
        }
        //makes a soft delete
        public int deleteData(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = " update student set isactive = false  where rollnumber = " + id + "";
            return command.ExecuteNonQuery();
        }
        public List<int> DeleteMultiple(List<int> list)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            List<int> nonDeletedRecords = new List<int>();
            List<int> deletedRecords = new List<int>();
            var command = connection.CreateCommand();
            for(int i =0; i<list.Count;i++)
            {
                command.CommandText = @" update student set isactive= false where rollnumber = " + list[i] + "";
                int  result =  command.ExecuteNonQuery();
                if(result == 0)
                {
                    nonDeletedRecords.Add(list[i]);
                }
                else
                {
                    deletedRecords.Add(list[i]);
                }
            }
            return  deletedRecords;
        }
    }
}
