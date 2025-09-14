using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentManagement
{
    enum CourseType
    {
        Java,
        DotNet,
        C_Cpp
    }

    class Student
    {
        public string Name { get; set; }
        public CourseType CourseType { get; set; }
        public int Semester { get; set; }
    }

    class StudentManagementSystem
    {
        private List<Student> StudentList = new List<Student>();

        // 🔹 Đọc dữ liệu từ file
        public void ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File không tồn tại!");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var s = line.Split(',');
                if (s.Length == 3)
                {
                    if (!TryParseCourseType(s[1], out CourseType courseType))
                    {
                        Console.WriteLine($"Không nhận diện được khóa học: {s[1]}");
                        continue;
                    }

                    Student student = new Student
                    {
                        Name = s[0],
                        CourseType = courseType,
                        Semester = int.Parse(s[2])
                    };
                    StudentList.Add(student);
                }
            }
        }

        // 🔹 Ghi danh sách sinh viên ra file
        public void WriteFile(string filePath)
        {
            var lines = StudentList.Select(s => $"{s.Name},{s.CourseType},{s.Semester}");
            File.WriteAllLines(filePath, lines);
        }

        // 🔹 Tìm kiếm sinh viên theo tên
        public List<Student> SearchByName(string name)
        {
            return StudentList.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // 🔹 Hiển thị báo cáo
        public void GenerateReport()
        {
            var report = StudentList
                .GroupBy(s => new { s.Name, s.CourseType })
                .Select(g => new
                {
                    Name = g.Key.Name,
                    Course = g.Key.CourseType,
                    Total = g.Count()
                });

            Console.WriteLine("Student Name | Course | Total of Course");
            foreach (var item in report)
            {
                Console.WriteLine($"{item.Name} | {item.Course} | {item.Total}");
            }
        }

        // 🔹 Thêm sinh viên
        public void AddStudent(Student student)
        {
            StudentList.Add(student);
        }

        // 🔹 Cập nhật thông tin sinh viên
        public void UpdateStudent(string name, CourseType course, int semester)
        {
            var student = StudentList.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (student != null)
            {
                student.CourseType = course;
                student.Semester = semester;
                Console.WriteLine("Cập nhật thành công!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên!");
            }
        }

        // 🔹 Hiển thị danh sách sinh viên
        public void PrintAllStudents()
        {
            foreach (var s in StudentList)
            {
                Console.WriteLine($"{s.Name} | {s.CourseType} | {s.Semester}");
            }
        }

        // 🔹 Map chuỗi trong file sang enum CourseType
        private bool TryParseCourseType(string input, out CourseType courseType)
        {
            courseType = default;
            if (string.IsNullOrWhiteSpace(input)) return false;

            string val = input.Trim().ToLower();
            if (val == "java") { courseType = CourseType.Java; return true; }
            if (val == ".net" || val == "dotnet" || val == "c#") { courseType = CourseType.DotNet; return true; }
            if (val == "c/c++" || val == "c_cpp" || val == "c++") { courseType = CourseType.C_Cpp; return true; }

            return Enum.TryParse<CourseType>(input, true, out courseType);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StudentManagementSystem sms = new StudentManagementSystem();
            string filePath = "students.txt";

            sms.ReadFile(filePath);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- MENU ---");
                Console.WriteLine("1. Hiển thị danh sách sinh viên");
                Console.WriteLine("2. Tìm kiếm sinh viên theo tên");
                Console.WriteLine("3. Thêm sinh viên");
                Console.WriteLine("4. Cập nhật sinh viên");
                Console.WriteLine("5. Xuất báo cáo");
                Console.WriteLine("6. Lưu danh sách ra file");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        sms.PrintAllStudents();
                        break;

                    case "2":
                        Console.Write("Nhập tên cần tìm: ");
                        var name = Console.ReadLine();
                        var results = sms.SearchByName(name);
                        foreach (var s in results)
                            Console.WriteLine($"{s.Name} | {s.CourseType} | {s.Semester}");
                        break;

                    case "3":
                        Console.Write("Nhập tên sinh viên: ");
                        string studentName = Console.ReadLine();
                        Console.Write("Chọn khóa học (0=Java, 1=DotNet, 2=C_Cpp): ");
                        if (int.TryParse(Console.ReadLine(), out int courseChoice) && Enum.IsDefined(typeof(CourseType), courseChoice))
                        {
                            CourseType course = (CourseType)courseChoice;
                            Console.Write("Nhập học kỳ: ");
                            int semester = int.Parse(Console.ReadLine());

                            sms.AddStudent(new Student { Name = studentName, CourseType = course, Semester = semester });
                        }
                        else
                        {
                            Console.WriteLine("Khóa học không hợp lệ!");
                        }
                        break;

                    case "4":
                        Console.Write("Nhập tên sinh viên cần cập nhật: ");
                        string updateName = Console.ReadLine();
                        Console.Write("Chọn khóa học mới (0=Java, 1=DotNet, 2=C_Cpp): ");
                        int newCourseChoice = int.Parse(Console.ReadLine());
                        Console.Write("Nhập học kỳ mới: ");
                        int newSemester = int.Parse(Console.ReadLine());
                        sms.UpdateStudent(updateName, (CourseType)newCourseChoice, newSemester);
                        break;

                    case "5":
                        sms.GenerateReport();
                        break;

                    case "6":
                        sms.WriteFile(filePath);
                        Console.WriteLine("Đã lưu danh sách ra file.");
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }
    }
}


