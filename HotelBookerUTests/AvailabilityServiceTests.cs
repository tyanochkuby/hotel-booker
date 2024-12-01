using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooker.Models;
using HotelBooker.Services;
using NUnit.Framework;

namespace HotelBookerUTests
{
    [TestFixture]
    public class AvailabilityServiceTests
    {
        private List<Hotel> GetTestHotels()
        {
            return new List<Hotel>
            {
                new Hotel
                {
                    Id = "H1",
                    Name = "Hotel One",
                    RoomTypes = new List<RoomType>
                    {
                        new RoomType { Code = "Single", Description = "Single Room", Amenities = new List<string>(), Features = new List<string>() },
                        new RoomType { Code = "Double", Description = "Double Room", Amenities = new List<string>(), Features = new List<string>() }
                    },
                    Rooms = new List<Room>
                    {
                        new Room { RoomType = "Single", RoomId = "R1" },
                        new Room { RoomType = "Single", RoomId = "R2" },
                        new Room { RoomType = "Double", RoomId = "R3" },
                        new Room { RoomType = "Double", RoomId = "R4" }
                    }
                },
                new Hotel
                {
                    Id = "H2",
                    Name = "Hotel Two",
                    RoomTypes = new List<RoomType>
                    {
                        new RoomType { Code = "Single", Description = "Single Room", Amenities = new List<string>(), Features = new List<string>() },
                        new RoomType { Code = "Double", Description = "Double Room", Amenities = new List<string>(), Features = new List<string>() }
                    },
                    Rooms = new List<Room>
                    {
                        new Room { RoomType = "Single", RoomId = "R5" },
                        new Room { RoomType = "Single", RoomId = "R6" },
                        new Room { RoomType = "Double", RoomId = "R7" },
                        new Room { RoomType = "Double", RoomId = "R8" }
                    }
                }
            };
        }

        private List<Booking> GetTestBookings()
        {
            return new List<Booking>
            {
                new Booking { HotelId = "H1", RoomType = "Single", Arrival = DateTime.Today, Departure = DateTime.Today.AddDays(2) },
                new Booking { HotelId = "H1", RoomType = "Double", Arrival = DateTime.Today, Departure = DateTime.Today.AddDays(1) },
                new Booking { HotelId = "H2", RoomType = "Single", Arrival = DateTime.Today.AddDays(-1), Departure = DateTime.Today.AddDays(1) },
                new Booking { HotelId = "H2", RoomType = "Single", Arrival = DateTime.Today, Departure = DateTime.Today.AddDays(3) }
            };
        }

        [Test]
        public void GetDetailedAvailability_ShouldReturnCorrectAvailability()
        {
            // Arrange
            var hotels = GetTestHotels();
            var bookings = GetTestBookings();
            var service = new AvailabilityService(hotels, bookings);

            var availability = new Availability(
                code: "H1",
                roomType: "Single",
                dateRange: new DateRange(start: DateTime.Today, end: DateTime.Today.AddDays(2))
            );

            // Act
            var detailedAvailability = service.GetDetailedAvailability(availability);

            // Assert
            Assert.That(detailedAvailability[DateTime.Today], Is.EqualTo(1));
            Assert.That(detailedAvailability[DateTime.Today.AddDays(1)], Is.EqualTo(1));
        }

        [Test]
        public void GetDetailedAvailability_ShouldReturnFullAvailabilityWhenNoBookings()
        {
            // Arrange
            var hotels = GetTestHotels();
            var bookings = new List<Booking>(); // No bookings
            var service = new AvailabilityService(hotels, bookings);

            var availability = new Availability(
                code: "H1",
                roomType: "Single",
                dateRange: new DateRange(start: DateTime.Today, end: DateTime.Today.AddDays(2))
            );

            // Act
            var detailedAvailability = service.GetDetailedAvailability(availability);

            // Assert
            Assert.That(detailedAvailability[DateTime.Today], Is.EqualTo(2));
            Assert.That(detailedAvailability[DateTime.Today.AddDays(1)], Is.EqualTo(2));
        }

        [Test]
        public void GetDetailedAvailability_ShouldThrowExceptionForInvalidHotelId()
        {
            // Arrange
            var hotels = GetTestHotels();
            var bookings = GetTestBookings();
            var service = new AvailabilityService(hotels, bookings);

            var availability = new Availability(
                code: "InvalidHotel",
                roomType: "Single",
                dateRange: new DateRange(start: DateTime.Today, end: DateTime.Today.AddDays(2))
            );

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.GetDetailedAvailability(availability));
        }

        [Test]
        public void GetDetailedAvailability_ShouldReturnFullAvailabilityForInvalidRoomType()
        {
            // Arrange
            var hotels = GetTestHotels();
            var bookings = GetTestBookings();
            var service = new AvailabilityService(hotels, bookings);

            var availability = new Availability(
                code: "H1",
                roomType: "InvalidRoomType",
                dateRange: new DateRange(start: DateTime.Today, end: DateTime.Today.AddDays(2))
            );

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => service.GetDetailedAvailability(availability));
        }

    }
}
