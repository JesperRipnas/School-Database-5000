using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace Exercise_1
{
    class Student : Person
    {
        public string Role { get; set; }
        public string Course { get; set; }
        public Student(string FirstName, string LastName, string Email, string PhoneNumber, string Course) : base(FirstName, LastName, Email, PhoneNumber)
        {
            this.Role = "Student";
            this.Course = Course;
        }
        public override string ToString()
        {
            return $"{Role} {FullName} {Email} {PhoneNumber} {Course}";
        }
        public static void ReadFile(string path, List<Student> studentList)
        {
            try
            {
                // Check if file exist, if not, create one
                // TBA MAKE THIS A METHOD ITSELF (CheckIfFileExist) as we use it multiple times
                if (!File.Exists(path))
                {
                    using (FileStream fs = File.Create(path)) { }
                    MessageBox.Show($"Didn't find {path}.\nFile created");
                }
                else
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            while (!sr.EndOfStream)
                            {
                                try
                                {
                                    string[] userInfo = sr.ReadLine().Split(',');
                                    studentList.Add(new Student(userInfo[1], userInfo[2], userInfo[3], userInfo[4], userInfo[5]));
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.ToString());
                                }
                            }
                            sr.Close();
                        }
                        fs.Close();
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.ToString());
            }
            catch (FileFormatException e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public static void SaveCurrentUsersToFile(string path, List<Student> studentList)
        {
            // Double check that the file still exists in the data folder just to be safe
            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path)) {}
                MessageBox.Show($"Didn't find {path}.\nFile created");
            }
            else
            {
                try
                {
                    // Remove everything in the text file before saving current data from list to
                    // avoid writing data twice
                    using (FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.Write))
                    {
                        fs.Close();
                    }
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            // Write each student object to a line each in students.txt
                            for (int i = 0; i < studentList.Count; i++)
                            {
                                sw.WriteLine($"{studentList[i].Role},{studentList[i].FirstName},{studentList[i].LastName},{studentList[i].Email},{studentList[i].PhoneNumber},{studentList[i].Course}");
                            }
                            sw.Close();
                            fs.Close();
                        }
                    }
                }
                catch (FileLoadException e)
                {
                    MessageBox.Show(e.ToString());
                }
                catch (FileFormatException e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }
        public static void RemoveOneStudentFromFile(string path, List<Student> studentList)
        {
            // Double check that the file still exists in the data folder just to be safe
            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path)) { }
                MessageBox.Show($"Didn't find {path}.\nFile created");
            }
            else
            {
                try
                {
                    // Remove everything in the text file as we want to write current data from list
                    // this is made to avoid writing the same data twice
                    using (FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.Write))
                    {
                        fs.Close();
                    }
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            // Write out every student except the last on in the list to file (as we will remove the last index)
                            for (int i = 0; i < studentList.Count - 1; i++)
                            {
                                sw.WriteLine($"{studentList[i].Role},{studentList[i].FirstName},{studentList[i].LastName},{studentList[i].Email},{studentList[i].PhoneNumber},{studentList[i].Course}");
                            }
                            sw.Close();
                            fs.Close();
                        }
                    }
                }
                catch (FileLoadException e)
                {
                    MessageBox.Show(e.ToString());
                }
                catch (FileFormatException e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }
        public static List<Student> SortData(string btn, List<Student> studentList)
        {
            List<Student> tempList = studentList;
            switch (btn)
            {
                case "sortRole": tempList = studentList.OrderBy(student => student.Role).ToList(); break;
                case "sortFirstName": tempList = studentList.OrderBy(student => student.FirstName).ToList(); break;
                case "sortLastName": tempList = studentList.OrderBy(student => student.LastName).ToList(); break;
                case "sortEmail": tempList = studentList.OrderBy(student => student.Email).ToList(); break;
                case "sortPhoneNumber": tempList = studentList.OrderBy(student => student.Email).ToList(); break;
                case "sortCourse": tempList = studentList.OrderBy(student => student.Course).ToList(); break;
                default: break;
            }
            return tempList;

        }
    }
}
