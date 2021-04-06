using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;


namespace Exercise_1
{
    public partial class MainWindow : Window
    {
        private string _filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        List<Student> Students = new List<Student>();
        List<Teacher> Teachers = new List<Teacher>();
        public MainWindow()
        {
            _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
            Student.ReadFile(_filePath + "/data/students.txt", Students);
            Teacher.ReadFile(_filePath + "/data/teachers.txt", Teachers);
            InitializeComponent();
            LoadWPFExtra();
        }
        private void LoadWPFExtra()
        {
            CourseBox.Items.Add("OOP");
            CourseBox.Items.Add("Javascript");
            CourseBox.Items.Add("Python");
            CourseBox.Items.Add("Agile");
        }
        private void StudentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            // If the checkbox for student is checked, we send all student information to listWindow to be visable to user
            try
            {
                if ((bool)studentCheckbox.IsChecked == true)
                {
                    for (int i = 0; i < Students.Count; i++)
                    {
                        ListWindow.Items.Add(Students[i]);
                    }
                }
            }
            catch (InvalidDataException err)
            {
                MessageBox.Show(err.ToString());
            }
        }
        private void StudentCheckbox_UnChecked(object sender, RoutedEventArgs e)
        {
            // Clear the listWindow that shows current students, checking the box again will show them again
            ListWindow.Items.Clear();
        }
        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            string fname = firstname.Text;
            string lname = lastname.Text;
            string em = email.Text;
            string pn = phonenumber.Text;
            string courseSelected = CourseBox.Text;

            // This should be rewritten so we can flag multiple errors and not take one error at a time instead of 
            // taking one parameter at a time
            if(Student.ValidateFirstName(fname))
            {
                if(Person.ValidateLastName(lname))
                {
                    if (Student.ValidateEmail(em))
                    {
                        if(Student.ValidatePhoneNumber(pn))
                        {
                            if(Student.ValidateCourse(courseSelected))
                            {
                                Student student = new Student(fname, lname, em, pn, courseSelected);
                                Students.Add(student);
                                ListWindow.Items.Add(student.ToString());
                                ClearTextbox();
                                Student.SaveCurrentUsersToFile(_filePath + "/data/students.txt", Students);
                            }
                            else
                            {
                                MessageBox.Show("You need to select course to add new student");
                            }
                        }
                        else 
                        {
                            MessageBox.Show("Not an valid phone number!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not an valid email adress!");
                    }
                }
                else
                {
                    MessageBox.Show("Not a valid last name");
                }
            }
            else
            {
                MessageBox.Show("Not a valid first name");
            }
        }
        private void BtnAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not added yet!");
        }
        private void BtnRemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Student.RemoveOneStudentFromFile(_filePath + "/data/students.txt", Students);
                ListWindow.Items.RemoveAt(ListWindow.Items.Count - 1);
                Students.RemoveAt(Students.Count - 1);
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show("No more students or teachers to remove\n\n" + error);
            }
        }
        private void BtnRemoveTeacher_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not added yet!");
        }
        private void ClearTextbox()
        {
            firstname.Text = "";
            lastname.Text = "";
            email.Text = "";
            phonenumber.Text = "";
        }
    }
}
