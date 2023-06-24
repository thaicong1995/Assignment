using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Model
{
    internal class StudentSubject
    {
        [Key]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Key]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
