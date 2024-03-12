using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Repository.Interfaces;

namespace Project.Repository
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly Context _context;
        public DepartmentRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void CreateDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }
        public List<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }
        /*public Department GetDepartmentByName(string Name)
        {
            return _context.Departments.FirstOrDefault(d => d.Name == Name);
        }*/
        public void UpdateDepartment(string Name)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Name == Name);
            if (department != null)
            {
                department.Name = Name;
                _context.SaveChanges();
            }
        }
        public void DeleteDepartmentByName(string Name)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Name == Name);
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
            }
        }
        public void DeleteAllDepartments()
        {
            _context.Departments.RemoveRange(_context.Departments);
            _context.SaveChanges();
        }
        public IEnumerable<Student> GetStudentsInDepartment(string DepartmentName)
        {
            var department = _context.Departments.Include(d => d.Students).FirstOrDefault(d => d.Name == DepartmentName);

            if (department != null && department.Students.Count > 0)
            {
                return department.Students;
            }
            else if (department != null && department.Students.Count == 0)
            {
                Console.WriteLine("No students found!");
                return Enumerable.Empty<Student>();
            }
            else
            {
                return Enumerable.Empty<Student>();
            }
        }
        public IEnumerable<Lecture> GetLecturesInDepartment(string LectureName)
        {
            var department = _context.Departments.Include(d => d.Lectures).FirstOrDefault(d => d.Name == LectureName);

            if (department != null && department.Lectures.Count > 0)
            {
                return department.Lectures;
            }
            else if (department != null && department.Lectures.Count == 0)
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
