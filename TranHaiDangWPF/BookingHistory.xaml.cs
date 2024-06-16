using BusinessObjects;
using BusinessObjects.DTO;
using Service;
using System.Windows;
using System.Windows.Input;

namespace TranHaiDangWPF
{
    /// <summary>
    /// Interaction logic for BookingHistory.xaml
    /// </summary>
    public partial class BookingHistory : Window
    {
        public Customer customer {  get; set; }
        RoomService roomService;


        public BookingHistory()
        {
            InitializeComponent();
            roomService = new RoomService();
            //btnUpdate.IsEnabled = false;
            //btnDelete.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<BookingHistoryDTO> bookingDetails = roomService.GetBookingByCusId(customer.CustomerId);

            dgBookingHistory.ItemsSource = bookingDetails;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.customer = customer;
            customerWindow.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BookingForm bookingForm = new BookingForm();
            bookingForm.Customer = customer;
            //bookingForm.Booking = new BookingDTO();
            bookingForm.Show();
            this.Hide();
        }
    }
}
