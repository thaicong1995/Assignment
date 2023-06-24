using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.DBcontext;
using Assignment.Model;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Service
{
    internal class StudentController
    {
        private DBContext dbContext = new DBContext();

        public void Add(Student student)
        {
            dbContext.Students.Add(student);
            dbContext.SaveChanges();
        }
        public void Update(Student student)
        {
            var existingStudent = dbContext.Students.Find(student.StudentId);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
                dbContext.SaveChanges();
            }
        }
        public void Delete(Student student)
        {
            var existingStudent = dbContext.Students.Find(student.StudentId);
            if (existingStudent != null)
            {
                dbContext.Students.Remove(existingStudent);
                dbContext.SaveChanges();
            }
        }

        public bool SearchByID(int id) 
        {
            return dbContext.Students.Find(id) != null;
        }

        public List<Student> GetStudentsByName(string studentName)
        {
            return dbContext.Students.Where(s => s.Name == studentName).ToList();
        }

        public List<Student> GetAll()
        {
            return dbContext.Students.ToList();
        }

    }
}
