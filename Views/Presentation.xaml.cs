using AcademyEFCore_NET7.Helper;
using AcademyEFCore_NET7.Models;
using Microsoft.EntityFrameworkCore;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using Convert = System.Convert;

namespace AcademyEFCore_NET7.Views
{
    /// <summary>
    /// Interaction logic for Presentation.xaml
    /// </summary>
    public partial class Presentation : RadWindow
    {
        public Presentation()
        {

            InitializeComponent();
            ApplicationDB.Current = new ApplicationDB();
            loadtable2();

        }

        void loadtable2()
        {
            var db = new ApplicationDB();
            var presentationsWithTeachersAndCourses = db.Presentations.Join(db.Teachers, p => p.TID, t => t.TID, (presentation, teacher) => new { Presentation = presentation, Teacher = teacher })
      .Join(db.Courses, pt => pt.Presentation.CID, c => c.CID, (pt, course) => new { Presentation = pt.Presentation, Teacher = pt.Teacher, Course = course })
      .Select(joinedData => new
      {
          PID = joinedData.Presentation.PID,
          PresentationName = joinedData.Presentation.Name,
          CourseName = joinedData.Course.CName,
          Category = joinedData.Course.Category,
          TeacherFirstName = joinedData.Teacher.FirstName,
          TeacherLastName = joinedData.Teacher.LastName,
          StartDate = joinedData.Presentation.StartDate,
          EndDate = joinedData.Presentation.EndDate,
          Capacity = joinedData.Presentation.Capacity,
          CID = joinedData.Course.CID,
          TID = joinedData.Teacher.TID

      }).ToList();

            GridViewPresentation.ItemsSource = presentationsWithTeachersAndCourses;
        }

        int GetSelectedId(RadComboBox comboBox)
        {
            if (comboBox.SelectedItem != null)
            {
                var selectedValue = comboBox.SelectedValue;
                if (selectedValue is int)
                {
                    return (int)selectedValue;
                }
            }
            return 0;
        }


        private void TeacherComboBox_Initialized(object sender, EventArgs e)

        {

            var db = new ApplicationDB();
            var result = db.Teachers
                .Select(t => new
                {
                    DisplayText = $"{t.TID} {t.FirstName} {t.LastName}",
                    ID = t.TID
                })
                .ToList();
            result.Insert(0, new { DisplayText = "Select a Teacher", ID = -1 });
            TeacherComboBox.ItemsSource = result;
            TeacherComboBox.DisplayMemberPath = "DisplayText";
            TeacherComboBox.SelectedValuePath = "ID";
            TeacherComboBox.SelectedIndex = 0;
        }

        private void CoursecComboBox_Initialized(object sender, EventArgs e)
        {
            var db = new ApplicationDB();
            var result = db.Courses
                .Select(c => new
                {
                    DisplayText = $"{c.CID} {c.CName} {c.Category}",
                    ID = c.CID
                })
                .ToList();
            result.Insert(0, new { DisplayText = "Select a Course", ID = -1 });
            CoursecComboBox.ItemsSource = result;
            CoursecComboBox.DisplayMemberPath = "DisplayText";
            CoursecComboBox.SelectedValuePath = "ID";
            CoursecComboBox.SelectedIndex = 0;
        }


        private void UpdatePresentationButton_Click(object sender, RoutedEventArgs e)
        {
            GetComboboxID getComboboxID = new GetComboboxID();
           
            try
            {
                
                int Courseid = getComboboxID.GetSelectedId(CoursecComboBox);
                int Teacherid = getComboboxID.GetSelectedId(TeacherComboBox);
                var db = new ApplicationDB();
                var Presentations = db.Presentations.SingleOrDefault(p => p.PID == Convert.ToInt32(TextBoxPID.Text));
                Presentations.TID = Teacherid;
                Presentations.CID = Courseid;
                Presentations.StartDate = StartDatePicker.DisplayDate;
                Presentations.EndDate = EndDatePicker.DisplayDate;
                Presentations.Capacity = Convert.ToInt32(TextBoxCapecity.Text);
                db.Update(Presentations);
                db.SaveChanges();
                loadtable2();
                RadWindow.Alert("Inforamtion Updated");
            }
            catch (Exception ex)
            {

                RadWindow.Alert(ex.Message);
            }




        }

            private void DeletePresentationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var db = new ApplicationDB();
                var Presentation = db.Presentations.SingleOrDefault(p => p.PID == Convert.ToInt32(TextBoxPID.Text));
                db.Remove(Presentation);
                db.SaveChanges();
                RadWindow.Alert("Presentation Deleted");
                loadtable2();
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }

        }

        private void RegisterPresentationButton_Click(object sender, RoutedEventArgs e)
        {
            GetComboboxID getComboboxID = new GetComboboxID();

            try
            {
                int Courseid = getComboboxID.GetSelectedId(CoursecComboBox);
                int Teacherid = getComboboxID.GetSelectedId(TeacherComboBox);

                var db = new ApplicationDB();
                db.Presentations.Add(new Models.Presentation {Name=TextBoxPresentionName.Text, CID = Courseid, TID = Teacherid, StartDate = StartDatePicker.DisplayDate, EndDate = EndDatePicker.DisplayDate, Capacity = Convert.ToInt32(TextBoxCapecity.Text) });
                db.SaveChanges();
                loadtable2();
                RadWindow.Alert("Presentation Registered.");
            }
            catch (Exception ex)
            {

                RadWindow.Alert(ex.Message);
            }

        }

        private void GridViewPresentation_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {

            if (GridViewPresentation.SelectedItem != null)
            {
                var db = new ApplicationDB();
                dynamic selectedItem = GridViewPresentation.SelectedItem;

                int selectedPID = selectedItem.PID;
                int selectedCID = selectedItem.CID;
                int selectedTID = selectedItem.TID;

                DateTime selectedStartDate = selectedItem.StartDate;
                DateTime selectedEndDate = selectedItem.EndDate;
                int selectedCapacity = selectedItem.Capacity;
                string selectedName = selectedItem.PresentationName;
                var Teacher = db.Teachers.Select(t => new { DisplayText = $"{t.TID} {t.FirstName} {t.LastName}", ID = t.TID }).FirstOrDefault(r => r.ID == selectedTID);
                var Course = db.Courses.Select(c => new { DisplayText = $"{c.CID} {c.CName} {c.Category}", ID = c.CID }).FirstOrDefault(r => r.ID == selectedCID);
                TextBoxPresentionName.Text = selectedName;
                TeacherComboBox.SelectedItem = Teacher;
                CoursecComboBox.SelectedItem = Course;
                StartDatePicker.SelectedDate = selectedStartDate;
                EndDatePicker.SelectedDate = selectedEndDate;
                TextBoxCapecity.Text = selectedCapacity.ToString();
                TextBoxPID.Text = selectedPID.ToString();
            }

        }
    }
}
