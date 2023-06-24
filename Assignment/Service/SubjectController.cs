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
    internal class SubjectController
    {
        private DBContext dbContext = new DBContext();

        public void Add(Subject subject)
        {
            dbContext.Subjects.Add(subject);
            dbContext.SaveChanges();
        }
        public void Update(Subject subject)
        {
            var existingSubject = dbContext.Subjects.Find(subject.SubjectId);
            if (existingSubject != null)
            {
                existingSubject.Name = subject.Name;
                existingSubject.Description = subject.Description;
                dbContext.SaveChanges();
            }
        }

        public bool SearchByID(int id)
        {
            return dbContext.Subjects.Find(id) != null;
        }
        public void Delete(Subject subject)
        {
            var existingSubject = dbContext.Subjects.Find(subject.SubjectId);
            if (existingSubject != null)
            {
                dbContext.Subjects.Remove(existingSubject);
                dbContext.SaveChanges();
            }
        }

        public List<Subject> GetBySubjectName(string subjectName)
        {
            return dbContext.Subjects.Where(s => s.Name == subjectName).ToList();
        }

        public List<Subject> GetAll()
        {
            return dbContext.Subjects.ToList();
        }
    }  
}
