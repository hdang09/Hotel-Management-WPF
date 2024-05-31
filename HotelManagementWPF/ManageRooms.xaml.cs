using BusinessObjects;
using Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TranHaiDangWPF
{
    /// <summary>
    /// Interaction logic for ManageRooms.xaml
    /// </summary>
    public partial class ManageRooms : Window
    {
        public RoomInformation? room {  get; set; }
        RoomService roomService;

        public ManageRooms()
        {
            InitializeComponent();
            roomService = new RoomService();
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
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
            dgvRooms.ItemsSource = roomService.GetRooms();
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
            roomForm.room = room;
            roomForm.isEdit = true;
            roomForm.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (room != null)
            {
                MessageBoxResult result = MessageBox.Show($"Do you want to delete {room.RoomNumber}?", "Delete Room", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    MessageBox.Show($"{room.RoomNumber} will be deleted.");
                    roomService.DeleteRoom(room.RoomId);
                    dgvRooms.ItemsSource = roomService.GetRooms();
                }
            }
        }

        private void dgvRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvRooms.SelectedItems.Count > 0)
            {
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;

                room = dgvRooms.SelectedItem as RoomInformation;
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
            if (!String.IsNullOrEmpty(tbSearch.Text))
            {
                try
                {
                    var customers = roomService.GetRooms().Where(r => r.RoomNumber.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
                    dgvRooms.ItemsSource = customers;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                dgvRooms.ItemsSource = roomService.GetRooms();
            }
        }
    }
}
