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
    }
}
