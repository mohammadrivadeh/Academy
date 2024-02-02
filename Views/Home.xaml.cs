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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : RadWindow
    {
        public Home()
        {
            InitializeComponent();
        }

        private void RadNavigationViewItem_Click(object sender, RoutedEventArgs e)
        {
            new Views.StudentProfile().ShowDialog();
        }

        private void RadNavigationViewItem_Click_1(object sender, RoutedEventArgs e)
        {
            new Views.Teacher().ShowDialog();
        }

        private void RadNavigationViewItem_Click_2(object sender, RoutedEventArgs e)
        {
            new Views.Course().ShowDialog();
        }

        private void RadNavigationViewItem_Click_3(object sender, RoutedEventArgs e)
        {
            new Views.Presentation().ShowDialog();
        }

        private void RadNavigationViewItem_Click_4(object sender, RoutedEventArgs e)
        {
            new Views.Register().ShowDialog();
        }
    }
}
