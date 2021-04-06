using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;

namespace Exercise_1
{
    class Teacher : Person
    {
        public string Role { get; set; }
        public Teacher(string FirstName, string LastName, string Email, string PhoneNumber ) : base (FirstName, LastName, Email, PhoneNumber)
        {
            this.Role = "Teacher";
        }
        public override string ToString()
        {
            return $"{Role} {FullName} {Email} {PhoneNumber}";
        }
        public static void ReadFile(string path, List<Teacher> teacherList)
        {
            try
            {
                // Check if file exist, if not, create one
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
                                    teacherList.Add(new Teacher(userInfo[0], userInfo[1], userInfo[2], userInfo[3]));
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
