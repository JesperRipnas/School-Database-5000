using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Threading;


namespace Exercise_1
{
    public partial class MainWindow : Window
    {
        private string _filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        List<Student> Students = new List<Student>();
        List<Student> CurrentSortedStudents = new List<Student>();
        List<Teacher> Teachers = new List<Teacher>();
        public MainWindow()
        {
            CurrentSortedStudents = Students;
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
            AddStudentBox.Visibility = Visibility.Hidden;
        }
        private void StudentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            // If the checkbox for student is checked, we send all student information to listWindow to be visable to user
            try
            {
                if ((bool)studentCheckbox.IsChecked == true)
                {
                    for (int i = 0; i < CurrentSortedStudents.Count; i++)
                    {
                        ListRole.Items.Add(CurrentSortedStudents[i].Role);
                        ListFirstName.Items.Add(CurrentSortedStudents[i].FirstName);
                        ListLastName.Items.Add(CurrentSortedStudents[i].LastName);
                        ListEmail.Items.Add(CurrentSortedStudents[i].Email);
                        ListPhoneNumber.Items.Add(CurrentSortedStudents[i].PhoneNumber);
                        ListCourse.Items.Add(CurrentSortedStudents[i].Course);
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
            ClearAllListBoxes();
        }
        private void ToggleStudentBox()
        {
            if (AddStudentBox.Visibility == Visibility.Hidden)
            {
                AddStudentBox.Visibility = Visibility.Visible;
                label_FirstName.Visibility = Visibility.Visible;
                label_LastName.Visibility = Visibility.Visible;
                label_Email.Visibility = Visibility.Visible;
                label_PhoneNumber.Visibility = Visibility.Visible;
                label_Course.Visibility = Visibility.Visible;

                firstname.Visibility = Visibility.Visible;
                lastname.Visibility = Visibility.Visible;
                email.Visibility = Visibility.Visible;
                phonenumber.Visibility = Visibility.Visible;
                CourseBox.Visibility = Visibility.Visible;

                BtnConfirmAddStudent.Visibility = Visibility.Visible;
                BtnCancelAddStudent.Visibility = Visibility.Visible;
            }
            else
            {
                AddStudentBox.Visibility = Visibility.Hidden;
                label_FirstName.Visibility = Visibility.Hidden;
                label_LastName.Visibility = Visibility.Hidden;
                label_Email.Visibility = Visibility.Hidden;
                label_PhoneNumber.Visibility = Visibility.Hidden;
                label_Course.Visibility = Visibility.Hidden;

                firstname.Visibility = Visibility.Hidden;
                lastname.Visibility = Visibility.Hidden;
                email.Visibility = Visibility.Hidden;
                phonenumber.Visibility = Visibility.Hidden;
                CourseBox.Visibility = Visibility.Hidden;

                BtnConfirmAddStudent.Visibility = Visibility.Hidden;
                BtnCancelAddStudent.Visibility = Visibility.Hidden;
            }
        }
        private void ClearAllListBoxes()
        {
            ListRole.Items.Clear();
            ListFirstName.Items.Clear();
            ListLastName.Items.Clear();
            ListEmail.Items.Clear();
            ListPhoneNumber.Items.Clear();
            ListCourse.Items.Clear();
        }
        private void UpdateAllStudentsListBox(List<Student> sortedList)
        {
            CurrentSortedStudents = sortedList;
            ClearAllListBoxes();
            foreach (var sortedStudent in sortedList)
            {
                ListRole.Items.Add(sortedStudent.Role.ToString());
                ListFirstName.Items.Add(sortedStudent.FirstName.ToString());
                ListLastName.Items.Add(sortedStudent.LastName.ToString());
                ListEmail.Items.Add(sortedStudent.Email.ToString());
                ListPhoneNumber.Items.Add(sortedStudent.PhoneNumber.ToString());
                ListCourse.Items.Add(sortedStudent.Course.ToString());
            }
        }
        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            ToggleStudentBox();
        }
        private void BtnAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not added yet!");
        }
        private void BtnRemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not added yet!");
            //try
            //{
            //    Student.RemoveOneStudentFromFile(_filePath + "/data/students.txt", Students);
            //    ListRole.Items.RemoveAt(ListRole.Items.Count - 1);
            //    ListFirstName.Items.RemoveAt(ListFirstName.Items.Count - 1);
            //    ListLastName.Items.RemoveAt(ListLastName.Items.Count - 1);
            //    ListEmail.Items.RemoveAt(ListEmail.Items.Count - 1);
            //    ListPhoneNumber.Items.RemoveAt(ListPhoneNumber.Items.Count - 1);
            //    ListCourse.Items.RemoveAt(ListCourse.Items.Count - 1);
            //    Students.RemoveAt(Students.Count - 1);
            //    CurrentSortedStudents.RemoveAt(CurrentSortedStudents.Count - 1);
            //}
            //catch (ArgumentOutOfRangeException error)
            //{
            //    MessageBox.Show("No more students or teachers to remove\n\n" + error);
            //}
        }
        private void BtnRemoveTeacher_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not added yet!");
        }
        private void BtnTopRole_Click(object sender, RoutedEventArgs e)
        {
            var returnList = Student.SortData("sortRole", Students);
            UpdateAllStudentsListBox(returnList);
        }
        private void BtnTopFirstName_Click(object sender, RoutedEventArgs e)
        {
            var returnList = Student.SortData("sortFirstName", Students);
            UpdateAllStudentsListBox(returnList);
        }
        private void BtnTopLastName_Click(object sender, RoutedEventArgs e)
        {
            var returnList = Student.SortData("sortLastName", Students);
            UpdateAllStudentsListBox(returnList);
        }
        private void BtnTopEmail_Click(object sender, RoutedEventArgs e)
        {
            var returnList = Student.SortData("sortEmail", Students);
            UpdateAllStudentsListBox(returnList);
        }
        private void BtnTopPhoneNumber_Click(object sender, RoutedEventArgs e)
        {
            var returnList = Student.SortData("sortPhoneNumber", Students);
            UpdateAllStudentsListBox(returnList);
        }
        private void BtnTopCourse_Click(object sender, RoutedEventArgs e)
        {
            var returnList = Student.SortData("sortCourse", Students);
            UpdateAllStudentsListBox(returnList);
        }
        private void BtnConfirmAddStudent_Click(object sender, RoutedEventArgs e)
        {
            string fname = firstname.Text;
            string lname = lastname.Text;
            string em = email.Text;
            string pn = phonenumber.Text;
            string courseSelected = CourseBox.Text;

            //This should be rewritten so we can flag multiple errors and not take one error at a time instead of
            // taking one parameter at a time
            if (Student.ValidateFirstName(fname))
            {
                if (Person.ValidateLastName(lname))
                {
                    if (Student.ValidateEmail(em))
                    {
                        if (Student.ValidatePhoneNumber(pn))
                        {
                            if (Student.ValidateCourse(courseSelected))
                            {
                                Student student = new Student(fname, lname, em, pn, courseSelected);
                                Students.Add(student);
                                ListRole.Items.Add(student.Role.ToString());
                                ListFirstName.Items.Add(student.FirstName.ToString());
                                ListLastName.Items.Add(student.LastName.ToString());
                                ListEmail.Items.Add(student.Email.ToString());
                                ListPhoneNumber.Items.Add(student.PhoneNumber.ToString());
                                ListCourse.Items.Add(student.Course.ToString());
                                ClearTextbox();
                                Student.SaveCurrentUsersToFile(_filePath + "/data/students.txt", Students);
                                ToggleStudentBox();
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
        private void BtnCancelAddStudent_Click(object sender, RoutedEventArgs e)
        {
            ToggleStudentBox();
        }
        private void ClearTextbox()
        {
            firstname.Text = "";
            lastname.Text = "";
            email.Text = "";
            phonenumber.Text = "";
        }

        //private void BtnTopFirstName_MouseEnter(object sender, RoutedEventArgs e)
        //{
        //    BtnTopFirstName.Background = Brushes.DarkGray;
        //}
        //private void BtnTopLastName_MouseEnter(object sender, RoutedEventArgs e)
        //{
        //    BtnTopFirstName.Background = Brushes.DarkGray;
        //}
    }
}
