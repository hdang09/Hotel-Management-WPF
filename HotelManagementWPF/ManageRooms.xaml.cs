using Service;
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

namespace HotelManagementWPF
{
    /// <summary>
    /// Interaction logic for ManageRooms.xaml
    /// </summary>
    public partial class ManageRooms : Window
    {
        public ManageRooms()
        {
            InitializeComponent();
        }

        private void lbMC_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void lbMC_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RoomService roomService = new RoomService();
            dgvRooms.ItemsSource = roomService.GetRooms();
        }
    }
}
