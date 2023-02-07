using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OopCourseWork.Interfaces;
using OopCourseWork.Models;

namespace OopCourseWork.Implementations
{
    public class WestminsterHotel : IHotelManager, IHotelCustomer
    {
        private static List<Room> _rooms = new List<Room>();
        public bool AddRoom(Room room)
        {
            if(!_rooms.Exists(r => r.GetRoomNumber() == room.GetRoomNumber())) {
                _rooms.Add(room);
                return true;
            } else {
                return false;
            }
        }

        public bool BookRoom(int roomNumber, Booking wantedBooking)
        {
            var room = _rooms.Find(r => r.GetRoomNumber() == roomNumber);

            if(!(room is null)) {
                var roomBookings = room.GetRoomBookings();
                var hasClashingBooking = roomBookings.Exists(booking => booking.Overlaps(wantedBooking));

                if(!hasClashingBooking) {
                    room.AddBooking(wantedBooking);
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        public bool DeleteRoom(int roomNumber)
        {
            int roomIndex = _rooms.FindIndex(r => r.GetRoomNumber() == roomNumber);

            if(roomIndex > -1) {
                _rooms.RemoveAt(roomIndex);
                return true;
            }
            return false;
        }

        public void GenerateReport(string fileName)
        {
            var roomsDetails = new List<object>();

            foreach (var room in _rooms)
            {
                roomsDetails.Add(room.GetRoomDetails());
            }

            if(!fileName.EndsWith(".json")) {
                fileName = $"{fileName}.json";
            }
            
            // Sources
            // https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions?view=net-5.0
            // https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializer?view=net-5.0
            // https://learn.microsoft.com/en-us/dotnet/api/system.io.file?view=net-5.0
            
            JsonSerializerOptions _options =  new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            var options = new JsonSerializerOptions(_options) { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(roomsDetails, options);
            File.WriteAllText(fileName, jsonString);
        }

        public void ListAvailableRooms(Booking wantedBooking, RoomSize roomSize)
        {
            var rooms = _rooms.Where((room) => room.GetRoomSize() == roomSize).ToList();

            var availableRooms = new List<Room>();
            rooms.ForEach(room => {
                var bookings = room.GetRoomBookings();
                var overlaps = bookings.Exists(booking => booking.Overlaps(wantedBooking));
                if(!overlaps) {
                    availableRooms.Add(room);
                }
            });

            Console.WriteLine($"RN \t RT \t\t FN \t RSize \t\t Price");

            availableRooms.ForEach((room) => {
                room.ShowRoomDetails();
            });
        }

        public void ListAvailableRooms(Booking wantedBooking, RoomSize roomSize, double maxPrice)
        {
            var rooms = _rooms.Where((room) => room.GetRoomSize() == roomSize && room.GetRoomPrice() <= (double)maxPrice).ToList();

            var availableRooms = new List<Room>();
            rooms.ForEach(room => {
                var bookings = room.GetRoomBookings();
                var overlaps = bookings.Exists(booking => booking.Overlaps(wantedBooking));
                if(!overlaps) {
                    availableRooms.Add(room);
                }
            });

            Console.WriteLine($"RN \t RT \t\t FN \t RSize \t\t Price");

            availableRooms.ForEach((room) => {
                room.ShowRoomDetails();
            });
        }

        public void ListRooms()
        {
            Console.WriteLine($"RN \t RT \t\t FN \t RSize \t\t Price");

             _rooms.ForEach((room) => {
                room.ShowRoomDetails();
            });
        }

        public void ListRoomsOrderedByPrice()
        {
            Console.WriteLine($"Room Number \t Room Type \t Floor Number \t Room Size \t Price Per Day");

            _rooms.OrderByDescending(r => r.GetRoomPrice()).ToList().ForEach((room) => {
                room.ShowRoomDetails();
            });
        }
    }
}