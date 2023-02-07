using OopCourseWork.Models;

namespace OopCourseWork.Interfaces
{
    public interface IHotelCustomer
    {
        public void ListAvailableRooms(Booking wantedBooking, RoomSize roomSize);
        public void ListAvailableRooms(Booking wantedBooking, RoomSize roomSize, double maxPrice);
        public bool BookRoom(int roomNumber, Booking wantedBooking);
    }
}