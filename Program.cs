using System;
using OopCourseWork.Implementations;
using OopCourseWork.Interfaces;
using OopCourseWork.Models;

namespace OopCourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            var hotel = new WestminsterHotel();

            var admin = new Admin("Hotel Admin", "121 Cavendish Street, Westminster", "admin@westminsterhotel.co.uk");

            hotel.AddRoom(new Room(RoomType.Standard, RoomSize.Single, 2, 1, 80, 1));
            hotel.AddRoom(new Room(RoomType.Standard, RoomSize.Double, 1, 2, 99, 2));
            hotel.AddRoom(new Room(RoomType.Standard, RoomSize.Single, 3, 1, 70, 3));
            hotel.AddRoom(new Room(RoomType.Deluxe, RoomSize.Double, 4, 5, 180, 4));
            hotel.AddRoom(new Room(RoomType.Standard, RoomSize.Double, 1, 2, 79, 5));
            hotel.AddRoom(new Room(RoomType.Deluxe, RoomSize.Single, 3, 2, 170, 6));

            while(true) 
            {

                Console.WriteLine("**Hello, Welcome to Westminster Hotel.**\n\nPlease enter a corresponding number to select one of the options below.\n");
                Console.WriteLine($"1. List Available Rooms. \n2. List Available Rooms by Max Price. \n3. Book room. \n4. Show Admin Menu.");

                var selectedOptionValid = Int32.TryParse(Console.ReadLine(), out int selectedOption);

                if(!selectedOptionValid || selectedOption > 5 || selectedOption < 1) {
                    Console.WriteLine("Invalid option selected");
                } else {
                    switch (selectedOption)
                    {
                        case 1:
                            Console.WriteLine("Please enter the start date of your intended booking in the format dd/mm/yyyy (e.g. 21/01/2023)");
                            var dateIsValid1 = DateTime.TryParse(Console.ReadLine(), out DateTime SD1);

                            if(dateIsValid1) {
                                Console.WriteLine("Please enter the end date of your intended booking in the format dd/mm/yyyy (e.g. 23/01/2023)");
                                dateIsValid1 = DateTime.TryParse(Console.ReadLine(), out DateTime ED1);

                                if(SD1 < DateTime.UtcNow) {
                                    Console.WriteLine("Start date must be today or beyond\n");
                                    break;
                                }

                                if(dateIsValid1) {
                                    if(ED1 <= SD1) {
                                        Console.WriteLine("End date must be after start date\n");
                                         break;
                                    }
                                    var wantedBooking = new Booking(SD1, ED1);
                                    Console.WriteLine("Please select a room size.\n1. Single\n2. Double\n3. Triple");
                                    var roomSizeValid = Int32.TryParse(Console.ReadLine(), out int roomSize);

                                    if(roomSize > 3 || roomSize < 1) {
                                        Console.WriteLine("Invalid room size selected\n");
                                        break;
                                    }

                                    hotel.ListAvailableRooms(wantedBooking, Enum.Parse<RoomSize>(roomSize.ToString()));
                                } else {
                                    Console.WriteLine("Invalid date\n");
                                }
                            } else {
                                Console.WriteLine("Invalid date\n");
                            }
                            break;
                        case 2:
                            Console.WriteLine("Please enter the start date of your intended booking in the format dd/mm/yyyy (e.g. 21/01/2023)\n");
                            var dateIsValid = DateTime.TryParse(Console.ReadLine(), out DateTime SD);

                            if(dateIsValid) {
                                if(SD < DateTime.UtcNow) {
                                    Console.WriteLine("Start date must be today or beyond\n");
                                    break;
                                }

                                Console.WriteLine("Please enter the end date of your intended booking in the format dd/mm/yyyy (e.g. 23/01/2023)\n");
                                dateIsValid = DateTime.TryParse(Console.ReadLine(), out DateTime ED);

                                if(dateIsValid) {
                                    if(ED <= SD) {
                                        Console.WriteLine("End date must be after start date\n");
                                         break;
                                    }
                                    var wantedBooking = new Booking(SD, ED);
                                    Console.WriteLine("Please select a room size.\n1. Single\n2. Double\n3. Triple");
                                    var roomSizeValid = Int32.TryParse(Console.ReadLine(), out int roomSize);

                                    if(roomSize > 3 || roomSize < 1) {
                                        Console.WriteLine("Invalid room size selected\n");
                                        break;
                                    }

                                    Console.WriteLine("Please enter the maximum amount per day. (e.g. 45)\n");
                                    var amountValid = Double.TryParse(Console.ReadLine(), out double maxAmount);

                                    if(!amountValid) {
                                        Console.WriteLine("Invalid amount entered\n");
                                        break;
                                    }

                                    hotel.ListAvailableRooms(wantedBooking, Enum.Parse<RoomSize>(roomSize.ToString()), maxAmount);
                                }
                                else {
                                    Console.WriteLine("Invalid date\n");
                                }
                            } else {
                                Console.WriteLine("Invalid date\n");
                            }
                            break;
                        case 3:
                                Console.WriteLine("Please enter the start date of your intended booking in the format dd/mm/yyyy (e.g. 21/01/2023)\n");
                                var validDate = DateTime.TryParse(Console.ReadLine(), out DateTime startDate);

                                if(validDate) {
                                    if(startDate < DateTime.UtcNow) {
                                        Console.WriteLine("Start date must be today or beyond\n");
                                         break;
                                    }

                                    Console.WriteLine("Please enter the end date of your intended booking in the format dd/mm/yyyy (e.g. 23/01/2023)\n");
                                    validDate = DateTime.TryParse(Console.ReadLine(), out DateTime endDate);
                                    
                                    if(validDate) {
                                        if(endDate <= startDate) {
                                            Console.WriteLine("End date must be after start date\n");
                                             break;
                                        }
                                        var booking = new Booking(startDate, endDate);
                                        Console.WriteLine("Please select a room from the available rooms shown above (e.g. 5)\n");
                                        var roomNumberValid = Int32.TryParse(Console.ReadLine(), out int roomNumber);

                                        if(!roomNumberValid) {
                                            Console.WriteLine("Invalid room number, please try again\n");
                                            break;
                                        }
                                        var booked = hotel.BookRoom(roomNumber, booking); 
                                        if(booked) {
                                            Console.WriteLine("Booking successful!\n");
                                        } else {
                                            Console.WriteLine("The room is currently unavailable for booking for the selected dates\n");
                                        }
                                    } else {
                                        Console.WriteLine("Your input is invalid, please try again.\n");
                                        break;
                                    }
                                } else {
                                    Console.WriteLine("Your input is invalid, please try again.\n");
                                    break;
                                }
                            break;
                        case 4:
                            AdminMenu(hotel, admin);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        static void AdminMenu(WestminsterHotel hotel, Admin admin) 
        {
            Console.WriteLine($"**Hello, {admin.GetUserName()}.**\n\nPlease enter a corresponding number to select one of the options below.\n");
            Console.WriteLine($"1. Add room.\n2. Delete room.\n3. List Rooms.\n4. List rooms ordered by price.\n5. Generate report.\n6. Go back to Customer Menu.\n");
            var selectedOptionValid = Int32.TryParse(Console.ReadLine(), out int selectedOption);

            switch (selectedOption)
            {
                case 1:
                    Console.WriteLine("Please enter the details of the room in the format  below.\nFloor number, Number of windows, Price per day, Room number e.g. (3, 2, 49.99, 1)\n");

                    var roomDetails = Console.ReadLine().Split(',');

                    if(roomDetails.Length < 4 || roomDetails.Length > 4) {
                        Console.WriteLine("Invalid room details\n");
                        break;
                    }

                    var floorNumberValid = Int32.TryParse(roomDetails[0], out int floorNumber);

                    var numberOfWindowsValid = Int32.TryParse(roomDetails[1], out int numberOfWindows);

                    var pricePerDayValid = Double.TryParse(roomDetails[2], out double pricePerDay);
                    
                    var roomNumberValid = Int32.TryParse(roomDetails[3], out int roomNumber);

                    if(!floorNumberValid || !numberOfWindowsValid || !pricePerDayValid || !roomNumberValid) {
                        Console.WriteLine("Invalid parameter types in room details\n");
                        break;
                    }

                    Console.WriteLine("Please select a room size.\n1. Single\n2. Double\n3. Triple\n");
                    var roomSizeValid = Int32.TryParse(Console.ReadLine(), out int roomSize);

                    if(roomSize > 3 || roomSize < 1) {
                        Console.WriteLine("Invalid room size selected\n");
                        break;
                    }

                    Console.WriteLine("Please select a room type.\n1. Standard\n2. Deluxe\n");
                    var roomTypeValid = Int32.TryParse(Console.ReadLine(), out int roomType);

                    if(roomType > 2 || roomType < 1) {
                        Console.WriteLine("Invalid room type selected\n");
                        break;
                    }

                    Balcony balcony = null;

                    if(roomType == 2) {
                        Console.WriteLine("Please select a balcony view type.\n1. Sea\n2. Landmark\n3. Mountain\n");
                        var balconyViewValid = Int32.TryParse(Console.ReadLine(), out int balconyView);

                        if(balconyView > 3 || balconyView < 1) {
                            Console.WriteLine("Invalid balcony view type selected\n");
                            break;
                        }

                        Console.WriteLine("Please enter the size of the balcony.");
                        var balconySizeValid = Double.TryParse(Console.ReadLine(), out double balconySize);

                        if(!balconySizeValid) {
                            Console.WriteLine("Invalid balcony size value supplied\n");
                            break;
                        }

                        balcony = new Balcony(balconySize, Enum.Parse<ViewType>(balconyView.ToString()));
                    }
                    Room room = null;
                    if(roomType != 2) {
                        room = new Room(Enum.Parse<RoomType>(roomType.ToString()), Enum.Parse<RoomSize>(roomSize.ToString()), floorNumber, numberOfWindows, pricePerDay, roomNumber);
                    } else {
                        room = new Room(Enum.Parse<RoomType>(roomType.ToString()), Enum.Parse<RoomSize>(roomSize.ToString()), floorNumber, numberOfWindows, pricePerDay, roomNumber, balcony);
                    }

                    hotel.AddRoom(room);
                    break;
                case 2:
                    Console.WriteLine("Please enter the room number.\n");
                    var roomNumberToDeleteValid = Int32.TryParse(Console.ReadLine(), out int roomNumberToDelete);
                    if(roomNumberToDeleteValid) {
                        var roomDeleted = hotel.DeleteRoom(roomNumberToDelete);
                        if(!roomDeleted) {
                            Console.WriteLine("Room not found\n");
                        }
                    } else Console.WriteLine("Invalid input\n");
                    Console.WriteLine("Room deleted found\n");
                    break;
                case 3:
                    hotel.ListRooms(); 
                    break;
                case 4:
                    hotel.ListRoomsOrderedByPrice();
                    break;
                case 5:
                    Console.WriteLine("Please enter a filename you would like the report to be stored in (e.g. report.json)\n");
                    var fileName = Console.ReadLine();
                    hotel.GenerateReport(fileName);
                    break;
                case 6:
                    break;
                default:
                    break;
            }
        }
    }
}
