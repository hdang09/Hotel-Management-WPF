using BusinessObjects;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagementWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public Customer customer { get; set; }

        public CustomerWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (customer != null)
            {
                txtFullname.Text = customer.CustomerFullName;
                txtTelephone.Text = customer.Telephone;
                txtBirthday.Text = customer.CustomerBirthday.ToString();
                txtEmail.Text = customer.EmailAddress;
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BookingHistory bookingHistory = new BookingHistory();
            bookingHistory.customer = customer;
            bookingHistory.Show();
            this.Hide();
        }
    }
}