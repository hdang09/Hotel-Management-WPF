using BusinessObjects;
using Service;
using System.Windows;
using System.Windows.Input;

namespace TranHaiDangWPF
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public Customer? customer {  get; set; }
        CustomerService customerService;

        public AdminWindow()
        {
            InitializeComponent();
            customerService = new CustomerService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgvCustomers.ItemsSource = customerService.GetCustomers();
        }

        private void lbMR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ManageRooms manageRoomsWindow = new ManageRooms();
            manageRoomsWindow.Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RoomForm roomForm = new RoomForm();
            roomForm.isEdit = false;
            roomForm.Show();
            this.Hide();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            RoomForm roomForm = new RoomForm();
            //roomForm.room = room;
            roomForm.isEdit = true;
            roomForm.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (customer != null)
            {
                MessageBoxResult result = MessageBox.Show($"Do you want to delete {customer.CustomerFullName}?", "Delete Room", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    MessageBox.Show($"{customer.CustomerFullName} will be deleted.");
                    customerService.DeleteCustomer(customer.CustomerId);
                    dgvCustomers.ItemsSource = customerService.GetCustomers();
                }
            }
        }

        private void dgvCustomers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgvCustomers.SelectedItems.Count > 0)
            {
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;

                customer = dgvCustomers.SelectedItem as Customer;
            }
        }

        private void lbRS_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReportStatic reportStatic = new ReportStatic();
            reportStatic.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbSearch.Text))
            {
                try
                {
                    var customers = customerService.GetCustomers().Where(c => c.CustomerFullName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
                    dgvCustomers.ItemsSource = customers;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                dgvCustomers.ItemsSource = customerService.GetCustomers();
            }
        }
    }
}
