using System;
using Assignment.DBcontext;
using Assignment.Model;
using Assignment.Service;
using Microsoft.EntityFrameworkCore;

namespace Assignment
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DBContext dbContext = new DBContext();
            //dbcontext.createdb();
            StudentController studentController = new StudentController(); 
            SubjectController subjectController = new SubjectController();  
            bool isChoice = true;
            while (isChoice)
            {
                Console.WriteLine("1. ADD");
                Console.WriteLine("2. UPDATE");
                Console.WriteLine("3. SEARCH");
                Console.WriteLine("4. DELETE");
                Console.WriteLine("0. EXIT");

                Console.Write("ENTER CHOICE: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine();
                            Console.Write("Your name: ");
                            string studentName = Console.ReadLine();
                            Console.Write("Your email: ");
                            string email = Console.ReadLine();
                            Console.Write("Subject Name: ");
                            string subjectName = Console.ReadLine();
                            Console.Write("Description: ");
                            string description = Console.ReadLine();

                            var student = new Student { Name = studentName, Email = email };
                            var subject = new Subject { Name = subjectName, Description = description };

                            student.StudentSubjects = new List<StudentSubject>
                            {
                                new StudentSubject { Student = student, Subject = subject }
                            };

                            dbContext.Students.Add(student);
                            dbContext.SaveChanges();

                            Console.WriteLine("SUCCESS!!.");
                        }
                        break;
                    case "2":
                        {
                            Console.WriteLine();
                            Console.Write("Enter your ID: ");
                            string input = Console.ReadLine();
                            int studentId;
                            if (int.TryParse(input, out studentId))
                            {
                                var existingStudent = dbContext.Students.Find(studentId);
                                if (existingStudent != null)
                                {
                                    Console.Write("New name: ");
                                    string studentName = Console.ReadLine();
                                    Console.Write("New email: ");
                                    string email = Console.ReadLine();
                                    Console.Write("New subject name: ");
                                    string subjectName = Console.ReadLine();
                                    Console.Write("New description: ");
                                    string description = Console.ReadLine();

                                    existingStudent.Name = studentName;
                                    existingStudent.Email = email;

                                    var existingSubject = dbContext.Subjects.FirstOrDefault(s => s.SubjectId == existingStudent.StudentId);
                                    if (existingSubject != null)
                                    {
                                        existingSubject.Name = subjectName;
                                        existingSubject.Description = description;
                                    }
                                    else
                                    {
                                        var subject = new Subject { Name = subjectName, Description = description };
                                        existingStudent.StudentSubjects.Add(new StudentSubject { Student = existingStudent, Subject = subject });
                                    }


                                    dbContext.SaveChanges();
                                    Console.WriteLine("Success!");
                                }
                                else
                                {
                                    Console.WriteLine("Found!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("ID fail!");
                            }
                        }
                        break;
                    case "3":
                        {
                            Console.WriteLine();
                            Console.Write("Enter student name: ");
                            string studentName = Console.ReadLine();
                            Console.WriteLine();

                            var students = studentController.GetStudentsByName(studentName);
                            if (students.Count > 0)
                            {
                                foreach (var student in students)
                                {
                                    Console.WriteLine($"Student ID: {student.StudentId}");
                                    Console.WriteLine($"Name: {student.Name}");
                                    Console.WriteLine($"Email: {student.Email}");
                       
                                    var subjects = subjectController.GetBySubjectName(student.Name);
                                    foreach (var subject in subjects)
                                    {
                                        Console.WriteLine($"Subject name: {subject.Name}");
                                        Console.WriteLine($"Description: {subject.Name}");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("No student found!");
                            }
                        }

                        break;
                    case "4":
                       
                        break;
                    case "0":
                        isChoice = false;
                        break;
                    default:
                        Console.WriteLine("CHOICE AGAIN.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}

