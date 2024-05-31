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

namespace TranHaiDangWPF
{
    /// <summary>
    /// Interaction logic for ReportStatic.xaml
    /// </summary>
    public partial class ReportStatic : Window
    {
        public ReportStatic()
        {
            InitializeComponent();
        }

        private void lbMC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Hide();
        }

        private void lbMR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ManageRooms manageRooms = new ManageRooms();
            manageRooms.Show();
            this.Hide();
        }
    }
}
