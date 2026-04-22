using System;
using FluentAssertions;
using GymPortal.Domain;

namespace GymPortal.Tests;

public class MembershipTests
{
    [Fact]
    public void Membership_Should_Be_Active_When_EndDate_Is_In_Future()
    {
        var membership = new Membership
        {
            EndDate = DateTime.Now.AddDays(5)
        };

        var isActive = membership.IsActive;

        isActive.Should().BeTrue();
    }

    [Fact]
    public void Membership_Should_Not_Be_Active_When_EndDate_Is_Past()
    {
        var membership = new Membership
        {
            EndDate = DateTime.Now.AddDays(-1)
        };

        var isActive = membership.IsActive;

        isActive.Should().BeFalse();
    }
}
