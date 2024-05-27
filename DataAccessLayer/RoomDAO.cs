using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class RoomDAO
    {
        public static List<RoomInformation> GetRooms()
        {
            var rooms = new List<RoomInformation>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                rooms = context.RoomInformations.ToList();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rooms;
        } 

        public static void SaveRoom(RoomInformation room)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.RoomInformations.Add(room);
                context.SaveChanges();
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateRoom(RoomInformation room)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry<RoomInformation>(room).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteRoom(int id)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var deletedRoom = context.RoomInformations.SingleOrDefault(room => room.RoomId == id);
                if (deletedRoom != null)
                {
                    context.RoomInformations.Remove(deletedRoom);
                }
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static RoomInformation GetRoomById(int id)
        {
            using var context = new FuminiHotelManagementContext();
            return context.RoomInformations.FirstOrDefault(room => room.RoomId.Equals(id));
        }

        public static List<BookingHistoryDTO>? GetBookingByCusId(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingDetails
                .Include(bd => bd.BookingReservation)
                    .ThenInclude(br => br.Customer)
                .Include(bd => bd.Room)
                .Where(bd => bd.BookingReservation.CustomerId == id)
                .Select(bd => new BookingHistoryDTO
                {
                    BookingReservationId = bd.BookingReservationId,
                    RoomId = bd.RoomId,
                    RoomNumber = bd.Room.RoomNumber,
                    StartDate = bd.StartDate,
                    EndDate = bd.EndDate,
                    ActualPrice = bd.ActualPrice,
                    BookingDate = bd.BookingReservation.BookingDate,
                    TotalPrice = bd.BookingReservation.TotalPrice,
                    CustomerId = bd.BookingReservation.CustomerId,
                    BookingStatus = bd.BookingReservation.BookingStatus
                })
                .ToList();
        }

    }
}
