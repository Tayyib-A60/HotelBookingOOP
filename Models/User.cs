using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OopCourseWork.Models
{
    public abstract class User
    {
        private string _name;
        private string _address;
        private string _email;
        private int _id;
        private static int _userCount;
        public User(string name, string address, string email)
        {
            _address = address;
            _name = name;
            _email = email;
            _userCount++;
            _id = _userCount;
        }

        public string GetEmail()
        {
            return _email;
        }

        public string GetUserName()
        {
            return _name;
        }

        public int GetUserId()
        {
            return _id;
        }
        protected abstract string GetUserDetails();
    }

    public class Customer : User
    {
        private static List<Booking> _userBookings = new List<Booking>();
        public Customer(string name, string address, string email) : base(name, address, email)
        {
            
        }
        protected override string GetUserDetails()
        {
            return $"Customer: Name - ${this.GetUserName()}, Email - {this.GetEmail()}";
        }

        public void AddBooking(Booking userBooking)
        {
            _userBookings.Add(userBooking);
        }
    }

    public class Admin : User
    {
        public Admin(string name, string address, string email): base(name, address, email)
        {

        }
        protected override string GetUserDetails()
        {
            return $"Admin: Name - ${this.GetUserName()}, Email - {this.GetEmail()}";
        }
    }

}