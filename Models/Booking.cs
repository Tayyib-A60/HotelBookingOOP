using System;
using OopCourseWork.Interfaces;

namespace OopCourseWork.Models
{
    public class Booking : IOverlappable
    {
        private DateTime _checkIn;
        private DateTime _checkOut;
        private int _customerId;

        public Booking(DateTime checkIn, DateTime checkOut, int customerId = 0)
        {
            _checkIn = checkIn;
            _checkOut = checkOut;
            _customerId = customerId;
        }

        public bool Overlaps(Booking booking)
        {
            var overlaps = booking.GetCheckIn() == _checkIn || booking.GetCheckIn() > _checkIn && booking.GetCheckIn() < _checkOut;
            return overlaps;
        }

        public DateTime GetCheckIn()
        {
            return _checkIn;
        }

        public DateTime GetCheckOut()
        {
            return _checkOut;
        }

        public void ShowBookingDetails()
        {
            Console.WriteLine($"Check In: {_checkIn.ToString()}, Check Out: {_checkOut.ToString()}");
        }
    }
}