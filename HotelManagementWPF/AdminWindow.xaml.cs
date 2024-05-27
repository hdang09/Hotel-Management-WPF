using BusinessObjects;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public Customer customer {  get; set; }
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CustomerService service = new();
            dgvCustomers.ItemsSource = service.GetCustomers();
        }

        private void lbMR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ManageRooms manageRoomsWindow = new ManageRooms();
            manageRoomsWindow.Show();
            this.Hide();
        }
    }
}
