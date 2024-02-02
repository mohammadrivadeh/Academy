using AcademyEFCore_NET7.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Course.xaml
    /// </summary>
    public partial class Course : RadWindow
    {
        public Course()
        {
            InitializeComponent();
            ApplicationDB.Current = new ApplicationDB();
            loadTable();

        }

        void loadTable()
        {
            var db = ApplicationDB.Current;
            var result = db.Courses.Select(s => new { s.CID, s.CName, s.Category });
            GridViewCourse.ItemsSource = result;
        }
        void ClearTexBox()
        {
            TextBoxCID.Clear();
            TextBoxCourseCategory.Clear();
            TextBoxCourseName.Clear();
        }

        private void RegisterCourseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var db = ApplicationDB.Current;
                db.Courses.Add(new Models.Course { CName = TextBoxCourseName.Text, Category = TextBoxCourseCategory.Text });
                db.SaveChanges();
                loadTable();
                ClearTexBox();
                RadWindow.Alert("Course Added");
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);

            }



        }

        private void GridViewCourse_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            try
            {
                if (GridViewCourse.SelectedItem != null)
                {
                    var selectedItemType = GridViewCourse.SelectedItem.GetType();
                    var cidProperty = selectedItemType.GetProperty("CID");
                    var courseNameProperty = selectedItemType.GetProperty("CName");
                    var categoryProperty = selectedItemType.GetProperty("Category");

                    var cidValue = cidProperty.GetValue(GridViewCourse.SelectedItem);
                    var firstNameValue = courseNameProperty.GetValue(GridViewCourse.SelectedItem);
                    var lastNameValue = categoryProperty.GetValue(GridViewCourse.SelectedItem);

                    TextBoxCourseName.Text = firstNameValue.ToString();
                    TextBoxCourseCategory.Text = lastNameValue.ToString();
                    TextBoxCID.Text = cidValue.ToString();



                }
            }
            catch (Exception ex)
            {

                RadWindow.Alert(ex.Message);
            }


        }

        private void UpdateCourseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var db = new ApplicationDB();
                var course = db.Courses.SingleOrDefault(c => c.CID == Convert.ToInt32(TextBoxCID.Text));
                course.CName = TextBoxCourseName.Name;
                course.Category = TextBoxCourseName.Text;
                db.Update(course);
                db.SaveChanges();
                loadTable();
                ClearTexBox();
                RadWindow.Alert("Information Updated");

            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }


        }

        private void DeleteCourseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var db = new ApplicationDB();
                var cousre = db.Courses.SingleOrDefault(c => c.CID == Convert.ToInt32(TextBoxCID.Text));
                db.Remove(cousre);
                db.SaveChanges();
                loadTable();
                ClearTexBox();
                RadWindow.Alert("Course Deleted");
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);     
            }



        }

        private void TextBoxCID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
