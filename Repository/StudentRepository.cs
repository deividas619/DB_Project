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
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
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
        /*public Student GetStudentByName(string Name)
        {
            return _context.Students.FirstOrDefault(s => s.Name == Name);
        }*/
        public void UpdateStudent(string Name)
        {
            var student = _context.Students.FirstOrDefault(s => s.Name == Name);
            if (student != null)
            {
                student.Name = Name;
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

            if (student != null)
            {
                return student.Lectures;
            }
            else
            {
                Console.WriteLine("No lectures found!");
                return Enumerable.Empty<Lecture>();
            }
        }
    }
}
