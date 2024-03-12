﻿using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Interfaces
{
    internal interface IDepartmentRepository
    {
        public void CreateDepartment(Department department);
        public List<Department> GetAllDepartments();
        public void UpdateDepartment(string Name);
        public void DeleteDepartmentByName(string Name);
        public void DeleteAllDepartments();
        public IEnumerable<Student> GetStudentsInDepartment(string DepartmentName);
        public IEnumerable<Lecture> GetLecturesInDepartment(string DepartmentName);
    }
}