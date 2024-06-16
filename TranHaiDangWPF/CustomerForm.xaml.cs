using BusinessObjects;
using Service;
using System.Windows;

namespace TranHaiDangWPF
{
    /// <summary>
    /// Interaction logic for CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : Window
    {
        public Customer? customer { get; set; }
        public bool isEdit { get; set; }
        private readonly ICustomerService customerService = new CustomerService();

        public CustomerForm()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (dpBirthday.SelectedDate == null)
            {
                dpBirthday.SelectedDate = System.DateTime.Now;
            }

            Customer customer = new Customer()
            {
                CustomerFullName = txtFullname.Text,
                Telephone = txtTelephone.Text,
                CustomerBirthday = DateOnly.FromDateTime((DateTime) dpBirthday.SelectedDate),
                EmailAddress = txtEmail.Text,
                CustomerStatus = 1,
                Password = pwPassword.Password
            };

            RoomService roomService = new RoomService();
            if (isEdit)
            {
                customer.CustomerId = this.customer.CustomerId;
                customerService.UpdateCustomer(customer);
            }
            else
            {
                customerService.CreateCustomer(customer);
            }
            handleGoBack();
        }

        private void btnSubmit_Copy_Click(object sender, RoutedEventArgs e)
        {
            handleGoBack();
        }

        private void handleGoBack()
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (customer != null)
            {
                txtFullname.Text = customer.CustomerFullName;
                txtTelephone.Text = customer.Telephone;
                dpBirthday.SelectedDate = customer.CustomerBirthday?.ToDateTime(new TimeOnly(0, 0));
                txtEmail.Text = customer.EmailAddress;
                pwPassword.Password = customer.Password;
            }
        }
    }
}
