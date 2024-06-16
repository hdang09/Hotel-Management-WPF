using BusinessObjects;
using Service;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace TranHaiDangWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public Customer customer { get; set; }
        private readonly ICustomerService _customerService;

        public CustomerWindow()
        {
            InitializeComponent();
            _customerService = new CustomerService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (customer != null)
            {
                txtFullname.Text = customer.CustomerFullName;
                txtTelephone.Text = customer.Telephone;
                dpBirthday.Text = customer.CustomerBirthday.ToString();
                txtEmail.Text = customer.EmailAddress;
                pwPassword.Password = customer.Password;
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BookingHistory bookingHistory = new BookingHistory();
            bookingHistory.customer = customer;
            bookingHistory.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dpBirthday.SelectedDate == null)
            {
                dpBirthday.SelectedDate = System.DateTime.Now;
            }

            var customer = new Customer()
            {
                CustomerId = this.customer.CustomerId,
                CustomerFullName = txtFullname.Text,
                Telephone = txtTelephone.Text,
                CustomerBirthday = DateOnly.FromDateTime((DateTime)dpBirthday.SelectedDate),
                EmailAddress = txtEmail.Text,
                Password = pwPassword.Password,
                CustomerStatus = 1
            };

            try
            {
                _customerService.UpdateCustomer(customer);
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Profile updated failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}