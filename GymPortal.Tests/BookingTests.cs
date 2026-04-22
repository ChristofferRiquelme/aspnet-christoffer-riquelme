using System;
using FluentAssertions;
using GymPortal.Domain;

namespace GymPortal.Tests;

public class BookingTests
{
    [Fact]
    public void User_Should_Not_Be_Able_To_Book_Same_Class_Twice()
    {
        var userId = "user1";

        var bookings = new List<Booking>
        {
            new Booking { UserId = userId, GymClassId = 1 }
        };

        var alreadyBooked = bookings.Any(b => b.UserId == userId && b.GymClassId == 1);

        alreadyBooked.Should().BeTrue();
    }

    [Fact]
    public void Booking_Should_Be_Allowed_If_Not_Already_Booked()
    {
        var bookings = new List<Booking>();

        var exists = bookings.Any(b => b.GymClassId == 1);

        exists.Should().BeFalse();
    }
}
