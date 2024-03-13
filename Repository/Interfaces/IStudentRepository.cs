using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Interfaces
{
    internal interface IStudentRepository
    {
        public void CreateStudent(Student student);
        public List<Student> GetAllStudents();
        public Student GetStudentByName(string Name);
        public void UpdateStudent(string Name, Department department);
        public void DeleteStudentByName(string Name);
        public void DeleteAllStudents();
        public IEnumerable<Lecture> DisplayLecturesForStudent(string Name);
    }
}
