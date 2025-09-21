using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentApp
{
    public enum CourseType
    {
        Java,
        DotNet,
        C_Cpp
    }

    public class Student
    {
        public string Name { get; set; }
        public int Semester { get; set; }
        public CourseType CourseType { get; set; }

        public Student(string name, int semester, CourseType courseType)
        {
            Name = name;
            Semester = semester;
            CourseType = courseType;
        }
    }

    public class StudentManager
    {
        public List<Student> StudentList { get; set; }
        public string FileName { get; set; }

        public StudentManager(string fileName)
        {
            FileName = fileName;
            StudentList = new List<Student>();
        }

        // Đọc file
        public void ReadFile()
        {
            if (!File.Exists(FileName)) return;
            StudentList.Clear();
            foreach (var line in File.ReadAllLines(FileName))
            {
                var parts = line.Split(',');
                if (parts.Length == 3)
                {
                    string name = parts[0];
                    int semester = int.Parse(parts[1]);
                    CourseType course = parts[2] switch
                    {
                        "Java" => CourseType.Java,
                        ".Net" => CourseType.DotNet,
                        "C/C++" => CourseType.C_Cpp,
                        _ => throw new Exception("Invalid course name")
                    };
                    StudentList.Add(new Student(name, semester, course));
                }
            }
        }

        // Ghi file
        public void SaveToFile()
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                foreach (var s in StudentList)
                {
                    sw.WriteLine($"{s.Name},{s.Semester},{s.CourseType}");
                }
            }
        }

        // Thêm
        public void AddStudent(Student s)
        {
            StudentList.Add(s);
        }

        // Xóa
        public bool RemoveStudent(string name)
        {
            var sv = StudentList.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (sv != null)
            {
                StudentList.Remove(sv);
                return true;
            }
            return false;
        }

        // Tìm
        public List<Student> FindStudentByName(string name)
        {
            return StudentList.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Sửa
        public bool UpdateStudent(string oldName, string newName, int semester, CourseType course)
        {
            var sv = StudentList.FirstOrDefault(s => s.Name.Equals(oldName, StringComparison.OrdinalIgnoreCase));
            if (sv != null)
            {
                sv.Name = newName;
                sv.Semester = semester;
                sv.CourseType = course;
                return true;
            }
            return false;
        }

        // Thống kê
        public void GenerateReport()
        {
            var report = StudentList
                .GroupBy(s => new { s.Name, s.CourseType })
                .Select(g => new { g.Key.Name, g.Key.CourseType, Count = g.Count() });

            Console.WriteLine("Student Name | Course | Total of Course");
            foreach (var r in report)
            {
                Console.WriteLine($"{r.Name} | {r.CourseType} | {r.Count}");
            }
        }

        public void Display()
        {
            foreach (var s in StudentList)
            {
                Console.WriteLine($"{s.Name}\t{s.Semester}\t{s.CourseType}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StudentManager manager = new StudentManager("students.txt");

            // Đọc dữ liệu từ file
            manager.ReadFile();
            Console.WriteLine("Danh sách sinh viên:");
            manager.Display();

            // Thêm sinh viên
            manager.AddStudent(new Student("Nguyen Van A", 1, CourseType.Java));
            manager.AddStudent(new Student("Nguyen Van B", 2, CourseType.DotNet));
            manager.SaveToFile();

            // Tìm kiếm
            var result = manager.FindStudentByName("Nguyen Van A");
            Console.WriteLine("\nKết quả tìm kiếm:");
            foreach (var s in result)
            {
                Console.WriteLine($"{s.Name}\t{s.Semester}\t{s.CourseType}");
            }

            // Xóa sinh viên
            manager.RemoveStudent("Nguyen Van B");

            // Sửa sinh viên
            manager.UpdateStudent("Nguyen Van A", "Nguyen Van A Updated", 3, CourseType.C_Cpp);

            // Thống kê
            Console.WriteLine("\nBáo cáo:");
            manager.GenerateReport();

            manager.SaveToFile();
        }
    }
}
