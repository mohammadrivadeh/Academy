using AcademyEFCore_NET7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for Teacher.xaml
    /// </summary>
    public partial class Teacher : RadWindow
    {
        public Teacher()
        {
            InitializeComponent();
            ApplicationDB.Current = new ApplicationDB();
            LoadTable();
        }

        void LoadTable()
        {
            var db = new ApplicationDB();
            var result = db.Teachers.Select(s => new { s.TID, s.FirstName, s.LastName });
            GridViewTeacher.ItemsSource = result;
        }
        void ClearTexbox()
        {
            TextBoxFirstName.Clear();
            TextBoxLastName.Clear();
            TextBoxTID.Clear();
        }

        private void TeacherRigesterButton_Click(object sender, RoutedEventArgs e)
        {
                try
            {
                var db = new ApplicationDB();
                db.Teachers.Add(new Models.Teacher { FirstName = TextBoxFirstName.Text, LastName = TextBoxLastName.Text });
                db.SaveChanges();
                ClearTexbox();
                LoadTable();
                RadWindow.Alert("Teacher Registerd Successfuly");

            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }



        }

        private void TeacherUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var db = new ApplicationDB();
                var teacher = db.Teachers.SingleOrDefault(t => t.TID == Convert.ToInt32(TextBoxTID.Text));
                teacher.FirstName = TextBoxFirstName.Text;
                teacher.LastName = TextBoxLastName.Text;
                db.Update(teacher);
                db.SaveChanges();
                ClearTexbox();
                LoadTable();
                RadWindow.Alert("Information  Upadted");
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);

            }
        }

        private void GridViewTeacher_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            if (GridViewTeacher.SelectedItem != null)
            {
                var selectedItemType = GridViewTeacher.SelectedItem.GetType();
                var tidProperty = selectedItemType.GetProperty("TID");
                var firstNameProperty = selectedItemType.GetProperty("FirstName");
                var lastNameProperty = selectedItemType.GetProperty("LastName");

                var tidValue = tidProperty.GetValue(GridViewTeacher.SelectedItem);
                var firstNameValue = firstNameProperty.GetValue(GridViewTeacher.SelectedItem);
                var lastNameValue = lastNameProperty.GetValue(GridViewTeacher.SelectedItem);

                TextBoxFirstName.Text = firstNameValue.ToString();
                TextBoxLastName.Text = lastNameValue.ToString();
                TextBoxTID.Text = tidValue.ToString();



            }
        }

        private void TeacherDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var db = new ApplicationDB();
                var teacher = db.Teachers.SingleOrDefault(s => s.TID == Convert.ToInt32(TextBoxTID.Text));
                db.Remove(teacher);
                db.SaveChanges();
                LoadTable();
                ClearTexbox();
                RadWindow.Alert("Teacher Deleted");
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);    
                
            }

        }
    }
}
