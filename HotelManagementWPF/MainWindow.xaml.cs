using BusinessObjects;
using Microsoft.Extensions.Configuration;
using Service;
using System.IO;
using System.Windows;

namespace TranHaiDangWPF
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

        private bool IsAdmin(string email, string password)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();

            return email.Equals(configuration["AdminAccount:Email"]) && password.Equals(configuration["AdminAccount:Password"]);
        }
    }
}