using BusinessObjects;
using Microsoft.Extensions.Primitives;
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
    /// Interaction logic for RoomForm.xaml
    /// </summary>
    public partial class RoomForm : Window
    {
        public RoomInformation? room { get; set; }
        public bool isEdit { get; set; }

        public RoomForm()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            RoomInformation newRoom = new RoomInformation()
            {
                RoomNumber = txtRoomNumber.Text,
                RoomMaxCapacity = Int32.Parse(txtCapacity.Text),
                RoomDetailDescription = txtDescription.Text,
                RoomPricePerDay = (int)decimal.Parse(txtPrice.Text),
                RoomTypeId = 1,
                BookingDetails = new List<BookingDetail>(),
            };

            RoomService roomService = new RoomService();
            if (isEdit)
            {
                newRoom.RoomId = room.RoomId;
                roomService.UpdateRoom(newRoom);
            }
            else
            {
                
                roomService.CreateRoom(newRoom);
            }
            handleGoBack();
        }

        private void btnSubmit_Copy_Click(object sender, RoutedEventArgs e)
        {
            handleGoBack();
        }

        private void handleGoBack()
        {
            ManageRooms manageRooms = new ManageRooms();
            manageRooms.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (room != null)
            {
                txtRoomId.Text = room.RoomId.ToString();
                txtRoomNumber.Text = room.RoomNumber;
                txtCapacity.Text = room.RoomNumber.ToString();
                txtDescription.Text = room.RoomDetailDescription;
                txtPrice.Text = room.RoomPricePerDay.ToString();
            }
        }
    }
}
