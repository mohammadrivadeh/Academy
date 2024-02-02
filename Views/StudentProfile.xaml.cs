using AcademyEFCore_NET7.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace AcademyEFCore_NET7.Views
{
    /// <summary>
    /// Interaction logic for StudentProfile.xaml
    /// </summary>
    public partial class StudentProfile : RadWindow
    {

       
        public StudentProfile()
        {
            InitializeComponent();
            ApplicationDB.Current = new ApplicationDB();
            LoadTable();
        }
        void LoadTable()
        {
            var db = ApplicationDB.Current;
            var result = db.Students.Select(s=> new {s.SID,s.FirstName,s.LastName,s.Email,s.Phone,s.Age,s.StudentCode,s.CreateDate}).ToList();
            GridViewStudent.ItemsSource = result;
        }
        void ClearTextBox()
        {
            TextBoxFirstName.Clear();
            TextBoxLastName.Clear();
            TextBoxEmail.Clear();
            TextBoxPhone.Clear();
            TextBoxSID.Clear();
            TextBoxStudentCode.Clear();
            TextBoxAge.Clear();
        }
        private void RegisterStudentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var db = new ApplicationDB();
                db.Students.Add(new Student 
                { 
                    FirstName = TextBoxFirstName.Text,
                    LastName = TextBoxLastName.Text,
                    Email = TextBoxEmail.Text,
                    Phone = TextBoxPhone.Text,
                    Age = Convert.ToInt32(TextBoxAge.Text), 
                    StudentCode = Convert.ToInt32(TextBoxStudentCode.Text) 
                });
                db.SaveChanges();
                RadWindow.Alert("Student Registerd Successfuly.");
                LoadTable();
                ClearTextBox();


            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }


        }

        private void GridViewStudent_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            if (GridViewStudent.SelectedItem != null)
            {
                var selectedItemType = GridViewStudent.SelectedItem.GetType();
                var sidProperty=selectedItemType.GetProperty("SID");
                var firstNameProperty = selectedItemType.GetProperty("FirstName");
                var lastNameProperty = selectedItemType.GetProperty("LastName");
                var emailProperty = selectedItemType.GetProperty("Email");
                var phoneProperty = selectedItemType.GetProperty("Phone");
                var ageProperty = selectedItemType.GetProperty("Age");
                var studentCodeProperty = selectedItemType.GetProperty("StudentCode");

                var sidValue = sidProperty.GetValue(GridViewStudent.SelectedItem);
                var firstNameValue = firstNameProperty.GetValue(GridViewStudent.SelectedItem);
                var lastNameValue = lastNameProperty.GetValue(GridViewStudent.SelectedItem);
                var emailValue = emailProperty.GetValue(GridViewStudent.SelectedItem);
                var phoneValue = phoneProperty.GetValue(GridViewStudent.SelectedItem);
                var ageValue = ageProperty.GetValue(GridViewStudent.SelectedItem);
                var studentcodevalue = studentCodeProperty.GetValue(GridViewStudent.SelectedItem);

                TextBoxFirstName.Text = firstNameValue.ToString();
                TextBoxLastName.Text = lastNameValue.ToString();
                TextBoxEmail.Text = emailValue.ToString();
                TextBoxPhone.Text = phoneValue.ToString();
                TextBoxAge.Text = ageValue.ToString();
                TextBoxStudentCode.Text = studentcodevalue.ToString();
                TextBoxSID.Text = sidValue.ToString();

                

            }
        }

        private void UpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var db = new ApplicationDB();
                var student = db.Students.SingleOrDefault(s => s.SID == Convert.ToInt32(TextBoxSID.Text));
                student.FirstName = TextBoxFirstName.Text;
                student.LastName = TextBoxLastName.Text;
                student.Email = TextBoxEmail.Text;
                student.Phone = TextBoxPhone.Text;
                student.StudentCode = Convert.ToInt32(TextBoxStudentCode.Text);
                student.Age = Convert.ToInt32(TextBoxAge.Text);
                db.Update(student);
                db.SaveChanges();
                ClearTextBox();
                RadWindow.Alert("Information Updated");
                LoadTable();
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
                
            }

        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var db = new ApplicationDB();
                var student = db.Students.SingleOrDefault(s => s.SID == Convert.ToInt32(TextBoxSID.Text));
                db.Remove(student);
                db.SaveChanges();
                ClearTextBox();
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
                throw;
            }
           
        }
    }
}
