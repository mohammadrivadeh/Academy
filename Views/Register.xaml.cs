using AcademyEFCore_NET7.Helper;
using AcademyEFCore_NET7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using System.Reflection.PortableExecutable;
using Microsoft.EntityFrameworkCore;


namespace AcademyEFCore_NET7.Views
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : RadWindow
    {
        public Register()
        {
            InitializeComponent();
            ApplicationDB.Current = new ApplicationDB();
            LoadTable();
        }

        void ClearForm()
        {
            StudentCombobox.SelectedIndex = 0;
            PresentionCombobox.SelectedIndex = 0;
            StudentScoreTextbox.Clear();
            TextBoxRID.Clear();
        }
        void LoadTable()
        {
            var db = new ApplicationDB();
            var presentationsWithTeachersAndCoursesAndStudents = db.Registers
                .Join(db.Presentations, r => r.PID, p => p.PID, (register, presentation) => new { Register = register, Presentation = presentation })
                .Join(db.Teachers, rp => rp.Presentation.TID, t => t.TID, (rp, teacher) => new { Register = rp.Register, Presentation = rp.Presentation, Teacher = teacher })
                .Join(db.Courses, rpt => rpt.Presentation.CID, c => c.CID, (rpt, course) => new { Register = rpt.Register, Presentation = rpt.Presentation, Teacher = rpt.Teacher, Course = course })
                .Join(db.Students, rptc => rptc.Register.SID, s => s.SID, (rptc, student) => new { Register = rptc.Register, Presentation = rptc.Presentation, Teacher = rptc.Teacher, Course = rptc.Course, Student = student })
                .Select(joinedData => new
                {
                    RID = joinedData.Register.RID,
                    StudentFirstName = joinedData.Student.FirstName,
                    StudentLastName = joinedData.Student.LastName,
                    PresentationName = joinedData.Presentation.Name,
                    CourseName = joinedData.Course.CName,
                    Category = joinedData.Course.Category,
                    TeacherFirstName = joinedData.Teacher.FirstName,
                    TeacherLastName = joinedData.Teacher.LastName,
                    StartDate = joinedData.Presentation.StartDate,
                    EndDate = joinedData.Presentation.EndDate,
                    Score = joinedData.Register.Score,
                    CID = joinedData.Course.CID,
                    TID = joinedData.Teacher.TID,
                    PID = joinedData.Presentation.PID,
                    SID = joinedData.Student.SID
                }).ToList();

            GridViewRegister.ItemsSource = presentationsWithTeachersAndCoursesAndStudents;


        }






        private void StudentCombobox_Initialized(object sender, EventArgs e)
        {
            var db = new ApplicationDB();
            var result = db.Students
                .Select(t => new
                {
                    DisplayText = $"{t.SID} {t.FirstName} {t.LastName}",
                    ID = t.SID
                })
                .ToList();
            result.Insert(0, new { DisplayText = "Select a Student", ID = -1 });
            StudentCombobox.ItemsSource = result;
            StudentCombobox.DisplayMemberPath = "DisplayText";
            StudentCombobox.SelectedValuePath = "ID";
            StudentCombobox.SelectedIndex = 0;
        }

        private void PresentionCombobox_Initialized(object sender, EventArgs e)
        {
            var db = new ApplicationDB();
            var result = db.Presentations
                .Select(t => new
                {
                    DisplayText = $"{t.PID} {t.Name} {t.Capacity}",
                    ID = t.PID
                })
                .ToList();
            result.Insert(0, new { DisplayText = "Select a Student", ID = -1 });
            PresentionCombobox.ItemsSource = result;
            PresentionCombobox.DisplayMemberPath = "DisplayText";
            PresentionCombobox.SelectedValuePath = "ID";
            PresentionCombobox.SelectedIndex = 0;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            GetComboboxID getComboboxID = new GetComboboxID();
            var db = new ApplicationDB();
            try
            {
                int StudentId = getComboboxID.GetSelectedId(StudentCombobox);
                int PresentionId = getComboboxID.GetSelectedId(PresentionCombobox);
                bool isDuplicateEntry = db.Registers.Any(r => r.PID == PresentionId && r.SID == StudentId);
                if (isDuplicateEntry)
                {

                    RadWindow.Alert("You Can not Register The Student And Presention Beacuse Already Exist ");
                    return;

                }
                else
                {

                    db.Registers.Add(new Models.Register { SID = StudentId, PID = PresentionId, Score = Convert.ToInt32(StudentScoreTextbox.Text) });
                    db.SaveChanges();
                    RadWindow.Alert("Presentation Registration Registered.");
                    ClearForm();
                    LoadTable();


                }

            }
            catch (Exception ex)
            {

                RadWindow.Alert(ex.Message);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            GetComboboxID getComboboxID = new GetComboboxID();
            try
            {
                var db = new ApplicationDB();
                dynamic selectedItem = GridViewRegister.SelectedItem;
                int selectedPID = selectedItem.PID;
                int selectedSID = selectedItem.SID;
                int StudentId = getComboboxID.GetSelectedId(StudentCombobox);
                int PresentionId = getComboboxID.GetSelectedId(PresentionCombobox);
                var Registers = db.Registers.SingleOrDefault(r => r.RID == Convert.ToInt32(TextBoxRID.Text));
                bool isDuplicateEntry = selectedPID == PresentionId && selectedSID == StudentId;
                if (isDuplicateEntry)
                {
                    Registers.Score = Convert.ToInt32(StudentScoreTextbox.Text);
                    db.Update(Registers);
                    db.SaveChanges();
                    RadWindow.Alert("Inforamtion Updated");
                    LoadTable();
                    ClearForm();


                }
                else
                {
                    RadWindow.Alert("Inforamtion Cant Be Updated Check Your Inputs.");
                    return;
                }

            }
            catch (Exception ex)
            {

                RadWindow.Alert(ex.Message);
            }
        }

        private void GridViewRegister_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            if (GridViewRegister.SelectedItem != null)
            {
                var db = new ApplicationDB();
                dynamic selectedItem = GridViewRegister.SelectedItem;

                int selectedPID = selectedItem.PID;
                int selectedRID = selectedItem.RID;
                int selectedSID = selectedItem.SID;
                int selectedScore = selectedItem.Score;
                var Presentations = db.Presentations.Select(t => new { DisplayText = $"{t.PID} {t.Name} {t.Capacity}", ID = t.TID }).FirstOrDefault(r => r.ID == selectedPID);
                var Student = db.Students.Select(c => new { DisplayText = $"{c.SID} {c.FirstName} {c.LastName}", ID = c.SID }).FirstOrDefault(r => r.ID == selectedSID);
                TextBoxRID.Text = selectedRID.ToString();
                StudentCombobox.SelectedItem = Student;
                PresentionCombobox.SelectedItem = Presentations;
                StudentScoreTextbox.Text = selectedScore.ToString();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var db = new ApplicationDB();
                var Register = db.Registers.SingleOrDefault(r => r.RID == Convert.ToInt32(TextBoxRID.Text));
                db.Remove(Register);
                db.SaveChanges();
                RadWindow.Alert("Presentation Registration Deleted");
                LoadTable();
                ClearForm();
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }
        }



    }
}
