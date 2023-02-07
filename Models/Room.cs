using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OopCourseWork.Models
{
    public class Room
    {
        private RoomType _roomType;
        private RoomSize _roomSize;
        private int _roomNumber;
        private int _floor;
        private int _numberOfWindows;
        private double _pricePerDay;
        private Balcony _balcony;
        private List<Booking> _bookings;
        public Room(RoomType roomType, RoomSize roomSize, int floor, int numberOfWindows, double pricePerDay, int roomNumber)
        {
            _roomType = roomType;
            _roomSize = roomSize;
            _floor = floor;
            _pricePerDay = pricePerDay;
            _roomNumber = roomNumber;

            // A room should have at least one window
            if(numberOfWindows > 0) {
                _numberOfWindows = numberOfWindows;
            } else {
                _numberOfWindows = 1;
            }
            _balcony = new Balcony(0, 0);
            _bookings = new List<Booking>();
        }

        // Overloaded constructor to create Deluxe room (with Balcony)
        public Room(RoomType roomType, RoomSize roomSize, int floor, int numberOfWindows, double pricePerDay, int roomNumber, Balcony balcony)
        {
            _roomType = roomType;
            _roomSize = roomSize;
            _floor = floor;
            _balcony = balcony;
            _numberOfWindows = numberOfWindows;

            // A room should have at least one window
            if(numberOfWindows > 0) {
                _numberOfWindows = numberOfWindows;
            } else {
                _numberOfWindows = 1;
            }
            _bookings = new List<Booking>();
        }

        public int GetRoomNumber()
        {
            return _roomNumber;
        }
        public double GetRoomPrice()
        {
            return _pricePerDay;
        }
        public RoomSize GetRoomSize()
        {
            return _roomSize;
        }

        public List<Booking> GetRoomBookings()
        {
            return _bookings;
        }

        public void AddBooking(Booking booking) 
        {
            _bookings.Add(booking);
        }
        public void ShowRoomDetails()
        {
            Console.WriteLine($"{_roomNumber} \t {_roomType.ToString()} \t {_floor} \t {_roomSize} \t {_pricePerDay}");
        }
        public object GetRoomDetails()
        { 
            List<object> bookings = new List<object>();
            _bookings.ForEach((bk) => {
                bookings.Add(new {
                    CheckIn = bk.GetCheckIn(),
                    CheckOut = bk.GetCheckOut()
                }); 
            });

            return new {
                PricePerDay = _pricePerDay,
                RoomSize = _roomSize,
                FloorNumber = _floor,
                RoomNumber = _roomNumber,
                NumberOfWindows = _numberOfWindows,
                Balcony = new {
                    Size = _balcony.GetSize(),
                    View = _balcony.GetView()
                },
                bookings
            };
        }
    }

    public class Balcony {

        public Balcony(double size, ViewType viewType)
        {
            _size = size;
            _view = viewType;
        }
        private double _size;
        private ViewType _view;
        public double GetSize() 
        {
            return _size;
        }
        public string GetView() 
        {
            return _view.ToString();
        }

    }

    public enum RoomType {
        Standard = 1,
        Deluxe
    }
    
    public enum RoomSize {
        Single = 1,
        Double,
        Triple
    }
    public enum ViewType {
        Sea = 1,
        Landmark,
        Mountain
    }
}