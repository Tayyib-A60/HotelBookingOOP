using OopCourseWork.Models;

namespace OopCourseWork.Interfaces
{
    public interface IOverlappable
    {
        bool Overlaps(Booking booking);
    }
}