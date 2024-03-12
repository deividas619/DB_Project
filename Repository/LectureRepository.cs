﻿using System;
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
    internal class LectureRepository : ILectureRepository
    {
        private readonly Context _context;
        public LectureRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void CreateLecture(Lecture lecture)
        {
            _context.Lectures.Add(lecture);
            _context.SaveChanges();
        }
        public List<Lecture> GetAllLectures()
        {
            return _context.Lectures.ToList();
        }
        public Lecture GetLectureByName(string Name)
        {
            return _context.Lectures.FirstOrDefault(l => l.Name == Name);
        }
        public void UpdateLecture(string Name)
        {
            var lecture = _context.Lectures.FirstOrDefault(l => l.Name == Name);
            if (lecture != null)
            {
                lecture.Name = Name;
                _context.SaveChanges();
            }
        }
        public void DeleteLectureByName(string Name)
        {
            var lecture = _context.Lectures.FirstOrDefault(l => l.Name == Name);
            if (lecture != null)
            {
                _context.Lectures.Remove(lecture);
                _context.SaveChanges();
            }
        }
        public void DeleteAllLectures()
        {
            _context.Lectures.RemoveRange(_context.Lectures);
            _context.SaveChanges();
        }
    }
}