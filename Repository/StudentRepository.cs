using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    internal class StudentRepository : IStudentRepository
    {
        private readonly Context _context;
        public StudentRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void CreateStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }
        public Student GetStudentByName(string Name)
        {
            return _context.Students.FirstOrDefault(s => s.Name == Name);
        }
        public void UpdateStudent(string oldName, string newName, Department department)
        {
            var student = _context.Students.Include(s => s.Department).FirstOrDefault(s => s.Name == oldName);
            if (student != null)
            {
                student.Name = newName;

                if (department != null)
                {
                    student.Department = department;
                }

                _context.SaveChanges();
            }
        }
        public void DeleteStudentByName(string Name)
        {
            var student = _context.Students.FirstOrDefault(s => s.Name == Name);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
        public void DeleteAllStudents()
        {
            _context.Students.RemoveRange(_context.Students);
            _context.SaveChanges();
        }
        public IEnumerable<Lecture> DisplayLecturesForStudent(string StudentName)
        {
            var student = _context.Students.Include(s => s.Lectures).FirstOrDefault(s => s.Name == StudentName);

            if (student != null && student.Lectures.Count > 0)
            {
                return student.Lectures;
            }
            else if (student != null && student.Lectures.Count == 0)
            {
                Console.WriteLine("No lectures found!");
                return Enumerable.Empty<Lecture>();
            }
            else
            {
                return Enumerable.Empty<Lecture>();
            }
        }
    }
}
