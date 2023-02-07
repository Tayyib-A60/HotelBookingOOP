using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OopCourseWork.Models;

namespace OopCourseWork.Interfaces
{
    public interface IHotelManager
    {
        public bool AddRoom(Room room);
        public bool DeleteRoom(int roomNumber);
        public void ListRooms();
        public void ListRoomsOrderedByPrice();
        public void GenerateReport(string fileName);
    }
}