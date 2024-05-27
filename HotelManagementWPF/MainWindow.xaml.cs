using BusinessObjects;
using DataAccessLayer;
using Service;
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
    public partial class MainWindow : Window
    {
        CustomerService customerService;

        public MainWindow()
        {
            customerService = new CustomerService();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Password;

            // Admin role
            if (email.Equals("admin@FUMiniHotelSystem.com") && password.Equals("@@abc123@@"))
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.customer = new Customer()
                {
                    EmailAddress = email,
                    Password = password,

                };
                adminWindow.Show();
                this.Close();
            }
            else
            {
                Customer? customer = customerService.Login(email, password);

                if (customer == null)
                {
                    MessageBox.Show("Account does not exist");
                }
                else if (customer.EmailAddress.Equals(email) && customer.Password.Equals(password))
                {
                    var customerWindow = new CustomerWindow();
                    customerWindow.customer = customer;
                    customerWindow.Show();
                    this.Close();
                } 
                else
                {
                    MessageBox.Show("Invalid credentials");
                }
            }
        }
    }
}