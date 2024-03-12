using Project.Repository;
using Project.Repository.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Microsoft.Data.SqlClient;
using Project.Models;
using Dapper;

namespace Project
{
    internal class Program
    {
        private static readonly Context context = new Context();

        public static IStudentRepository studentRepository;
        public static ILectureRepository lectureRepository;
        public static IDepartmentRepository departmentRepository;
        static void Main(string[] args)
        {
            IStudentRepository studentRepository = new StudentRepository(context);
            ILectureRepository lectureRepository = new LectureRepository(context);
            IDepartmentRepository departmentRepository = new DepartmentRepository(context);

            MainMenu(studentRepository, lectureRepository, departmentRepository);
        }

        public static void MainMenu(IStudentRepository studentRepository, ILectureRepository lectureRepository, IDepartmentRepository departmentRepository)
        {
            do
            {
                var selection = MenuInteraction([
                    "0. Exit",
                    "1. Department menu",
                    "2. Lecture menu",
                    "3. Student menu"
                ]);

                switch (selection)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        DepartmentMenu(departmentRepository, lectureRepository);
                        continue;
                    case 2:
                        LectureMenu(lectureRepository);
                        continue;
                    case 3:
                        StudentMenu(studentRepository, lectureRepository);
                        continue;
                }
            }
            while (true);
        }
        public static int MenuInteraction(List<string> menuOptions)
        {
            var option = 0;
            var selected = false;
            var cursorPositionColumn = 0;
            var cursorPositionRow = 1;

            while (!selected)
            {
                Console.Clear();

                Console.WriteLine("Select:");
                for (var i = 0; i < menuOptions.Count; i++)
                {
                    Console.WriteLine($"{(option == i ? "> " : "  ")}{menuOptions[i]}");
                }

                var initialCursorPosition = Console.GetCursorPosition();
                Console.SetCursorPosition(cursorPositionColumn, cursorPositionRow);
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        Console.WriteLine($"{(option == 0 ? "> " : menuOptions[option])}");
                        if (option == menuOptions.Count - 1)
                        {
                            option = 0;
                            cursorPositionRow = 1;
                        }
                        else
                        {
                            option++;
                            cursorPositionRow++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        Console.WriteLine($"{(option > 0 ? "> " : menuOptions[option])}");
                        if (option == 0)
                        {
                            option = menuOptions.Count - 1;
                            cursorPositionRow = cursorPositionRow + menuOptions.Count - 1;
                        }
                        else
                        {
                            option--;
                            cursorPositionRow--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        selected = true;
                        Console.SetCursorPosition(initialCursorPosition.Left, initialCursorPosition.Top);
                        break;
                }
            }
            return option;
        }
        public static void StudentMenu(IStudentRepository studentRepository, ILectureRepository lectureRepository)
        {
            do
            {
                var selection = MenuInteraction([
                    "0. Return",
                    "1. Add a student",
                    "2. List all students",
                    "3. Update a student by name",
                    "4. Delete a student by name",
                    "5. Delete all students",
                    "6. Display all lectures for a student"
                ]);

                switch (selection)
                {
                    case 0:
                        return;
                    case 1:
                        Console.Write("Student name: ");
                        string newStudentName = Console.ReadLine();
                        try
                        {
                            var newStudent = new Student
                            {
                                Id = Guid.NewGuid(),
                                Name = newStudentName
                            };
                            studentRepository.CreateStudent(newStudent);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            throw;
                        }
                        continue;
                    case 2:
                        Console.Clear();
                        foreach (var student in studentRepository.GetAllStudents())
                        {
                            Console.WriteLine($"({student.Id}) {student.Name}");
                        }
                        ReturnToMainMenu();
                        continue;
                    case 3:
                        Console.Write("New student name: ");
                        string updatedStudentName = Console.ReadLine();
                        studentRepository.UpdateStudent(updatedStudentName);
                        continue;
                    case 4:
                        List<string> studentNames = studentRepository.GetAllStudents().Select(d => d.Name).ToList();
                        var studentToDelete = MenuInteraction(studentNames);
                        studentRepository.DeleteStudentByName(studentNames[studentToDelete]);
                        continue;
                    case 5:
                        studentRepository.DeleteAllStudents();
                        continue;
                    case 6:
                        List<string> studentNamesAgain = studentRepository.GetAllStudents().Select(s => s.Name).ToList();
                        var studentToLookFor = MenuInteraction(studentNamesAgain);
                        studentRepository.DisplayLecturesForStudent(studentNamesAgain[studentToLookFor]);
                        continue;
                }
            }
            while (true);
        }
        public static void LectureMenu(ILectureRepository lectureRepository)
        {
            do
            {
                var selection = MenuInteraction([
                    "0. Return",
                    "1. Add a lecture",
                    "2. List all lectures",
                    "3. Update a lecture by name",
                    "4. Delete a lecture by name",
                    "5. Delete all lectures"
                ]);

                switch (selection)
                {
                    case 0:
                        return;
                    case 1:
                        Console.Write("Lecture name: ");
                        string newLectureName = Console.ReadLine();
                        try
                        {
                            var newLecture = new Lecture
                            {
                                Id = Guid.NewGuid(),
                                Name = newLectureName
                            };
                            lectureRepository.CreateLecture(newLecture);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            throw;
                        }
                        continue;
                    case 2:
                        Console.Clear();
                        foreach (var lecture in lectureRepository.GetAllLectures())
                        {
                            Console.WriteLine($"({lecture.Id}) {lecture.Name}");
                        }
                        ReturnToMainMenu();
                        continue;
                    case 3:
                        Console.Write("New lecture name: ");
                        string updatedLectureName = Console.ReadLine();
                        lectureRepository.UpdateLecture(updatedLectureName);
                        continue;
                    case 4:
                        List<string> lectureNames = lectureRepository.GetAllLectures().Select(l => l.Name).ToList();
                        var lectureToDelete = MenuInteraction(lectureNames);
                        lectureRepository.DeleteLectureByName(lectureNames[lectureToDelete]);
                        continue;
                    case 5:
                        lectureRepository.DeleteAllLectures();
                        continue;
                }
            }
            while (true);
        }
        public static void DepartmentMenu(IDepartmentRepository departmentRepository, ILectureRepository lectureRepository)
        {
            do
            {
                var selection = MenuInteraction([
                    "0. Return",
                    "1. Add a department",
                    "2. List all departments",
                    "3. Update a department by name",
                    "4. Delete a department by name",
                    "5. Delete all departments",
                    "6. Display all students in the department",
                    "7. Display all lectures in the department"
                ]);

                switch (selection)
                {
                    case 0:
                        return;
                    case 1:
                        Console.Write("Department name: ");
                        string newDepartmentName = Console.ReadLine();
                        Console.Write("How many lectures: ");
                        int.TryParse(Console.ReadLine(), out int amount);
                        List<Lecture> newLectures = new List<Lecture>();
                        List<Lecture> alreadyAddedLectures = new List<Lecture>();
                        if (amount > 0)
                        {
                            List<string> lectures = lectureRepository.GetAllLectures().Select(l => l.Name).ToList();
                            for (int i = 1; i <= amount; i++)
                            {
                                int lectureSelected = -1;
                                Console.Clear();
                                do
                                {
                                    lectureSelected = MenuInteraction(lectures);
                                    if (!alreadyAddedLectures.Contains(lectureRepository.GetLectureByName(lectures[lectureSelected])))
                                    {
                                        alreadyAddedLectures.Add(lectureRepository.GetLectureByName(lectures[lectureSelected]));
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("The lecture already exists for this department! Select again...");
                                    }
                                }
                                while (alreadyAddedLectures.Contains(lectureRepository.GetLectureByName(lectures[lectureSelected])));
                                newLectures.Add(lectureRepository.GetLectureByName(lectures[lectureSelected]));
                            }
                        }
                       
                        try
                        {
                            var newDepartment = new Department
                            {
                                Id = Guid.NewGuid(),
                                Name = newDepartmentName,
                                Lectures = newLectures
                            };
                            departmentRepository.CreateDepartment(newDepartment);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            throw;
                        }
                        continue;
                    case 2:
                        Console.Clear();
                        foreach (var department in departmentRepository.GetAllDepartments())
                        {
                            Console.WriteLine($"({department.Id}) {department.Name}");
                        }
                        ReturnToMainMenu();
                        continue;
                    case 3:
                        Console.Write("New department name: ");
                        string updatedDepartmentName = Console.ReadLine();
                        departmentRepository.UpdateDepartment(updatedDepartmentName);
                        continue;
                    case 4:
                        List<string> departmentNames = departmentRepository.GetAllDepartments().Select(d => d.Name).ToList();
                        var departmentToDelete = MenuInteraction(departmentNames);
                        departmentRepository.DeleteDepartmentByName(departmentNames[departmentToDelete]);
                        continue;
                    case 5:
                        departmentRepository.DeleteAllDepartments();
                        continue;
                    case 6:
                        List<string> departmentNamesForStudents = departmentRepository.GetAllDepartments().Select(d => d.Name).ToList();
                        var departmentToLookInStudents = MenuInteraction(departmentNamesForStudents);
                        Console.Clear();
                        Console.WriteLine($"Students for {departmentNamesForStudents[departmentToLookInStudents]}:\n");
                        foreach (var student in departmentRepository.GetStudentsInDepartment(departmentNamesForStudents[departmentToLookInStudents]))
                        {
                            Console.WriteLine($"{student.Name}");
                        }
                        ReturnToMainMenu();
                        continue;
                    case 7:
                        List<string> departmentNamesForLectures = departmentRepository.GetAllDepartments().Select(d => d.Name).ToList();
                        var departmentToLookInLectures = MenuInteraction(departmentNamesForLectures);
                        Console.Clear();
                        Console.WriteLine($"Lectures for {departmentNamesForLectures[departmentToLookInLectures]}:\n");
                        foreach (var lecture in departmentRepository.GetLecturesInDepartment(departmentNamesForLectures[departmentToLookInLectures]))
                        {
                            Console.WriteLine($"{lecture.Name}");
                        }
                        ReturnToMainMenu();
                        continue;
                }
            }
            while (true);
        }
        public static void ReturnToMainMenu()
        {
            Console.WriteLine("\nTo return press 'q'");
            while (true)
            {
                var i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Q)
                {
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Incorrect input!");
                    Console.ResetColor();
                    Console.Write(" Try again...\n");
                }
            }
        }
    }
}
