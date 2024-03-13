using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Interfaces
{
    internal interface ILectureRepository
    {
        public void CreateLecture(Lecture lecture);
        public List<Lecture> GetAllLectures();
        public Lecture GetLectureByName(string Name);
        public void UpdateLecture(string oldName, string newName, List<Student> students);
        public void DeleteLectureByName(string Name);
        public void DeleteAllLectures();
    }
}
