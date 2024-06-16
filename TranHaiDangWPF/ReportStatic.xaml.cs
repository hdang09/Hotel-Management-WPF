using BusinessObjects.DTO;
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
using Services;
using System.Windows.Threading;

namespace TranHaiDangWPF
{
    /// <summary>
    /// Interaction logic for ReportStatic.xaml
    /// </summary>
    public partial class ReportStatic : Window
    {
        RoomService roomService;
        CustomerService customerService;
        BookingHistoryService bookingHistoryService;
        private DispatcherTimer timer;

        public ReportStatic()
        {
            InitializeComponent();
            roomService = new RoomService();
            customerService = new CustomerService();
            bookingHistoryService = new BookingHistoryService();
        }

        private void lbMC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Hide();
        }

        private void lbMR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ManageRooms manageRooms = new ManageRooms();
            manageRooms.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<BookingHistoryDTO> bookingDetails = roomService.GetBooking();
            dgBookingHistory.ItemsSource = bookingDetails;
            lbCustomer.Content = customerService.GetCustomers().Count();
            lbRoom.Content = roomService.GetRooms().Count();
            lbHistory.Content = roomService.GetBooking().Count();
            lbReservation.Content = bookingHistoryService.GetBookings().Count();
            DisplayCurrentDateTime();
            SetupTimer();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (dpStartDate == null || dpEndDate == null)
            {
                MessageBox.Show("Please select start date and end date");
                return;
            }
            DateOnly startDate = DateOnly.FromDateTime((DateTime)dpStartDate.SelectedDate) ;
            DateOnly endDate = DateOnly.FromDateTime((DateTime)dpEndDate.SelectedDate);
            
            List<BookingHistoryDTO> bookingDetails = roomService.GetBooking().Where(room => room.BookingDate >= startDate && room.BookingDate <= endDate).ToList();
            dgBookingHistory.ItemsSource = bookingDetails;
        }

        private void DisplayCurrentDateTime()
        {
            DateTime now = DateTime.Now;
            lbDate.Content = now.ToString("d");
            lbTime.Content = now.ToString("HH:mm:ss");
        }

        private void SetupTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DisplayCurrentDateTime();
        }
    }
}
