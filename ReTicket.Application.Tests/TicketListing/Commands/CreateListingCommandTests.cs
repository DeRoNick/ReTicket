using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Options;
using Moq;
using ReTicket.Application.Abstractions;
using ReTicket.Application.Rules;
using ReTicket.Application.TicketListings.Commands;
using ReTicket.Domain.Models;
using ApplicationException = ReTicket.Application.Infrastructure.Exceptions.ApplicationException;

namespace ReTicket.Application.Tests.TicketListing.Commands;

public class CreateListingCommandTests
{

    [Fact]
    public async Task CreateListing_WhenEverythingRight_ShouldReturnInsertId()
    {
        var command = new CreateListing.Command(1, 20, "1");
        
        var listingRepoMock = new Mock<ITicketListingRepository>();
        var ticketRepoMock = new Mock<ITicketRepository>();
        var userRepoMock = new Mock<IUserRepository>();

        userRepoMock.Setup(x => x.GetByIdAsync(command.UserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AppUser());

        var ticket = new Ticket
        {
            Event = new Event
            {
                TicketPrice = 20
            }
        };

        var ruleOptions = new PriceRuleOptions
        {
            MarginCommissionPercentage = 20,
            MaximumMarginPercentage = 20
        };
        ticketRepoMock.Setup(x => x.GetByIdAsync(command.TicketId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ticket);

        listingRepoMock
            .Setup(x => x.InsertAsync(It.IsAny<Domain.Models.TicketListing>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        
        var handler = new CreateListing.Handler(listingRepoMock.Object, ticketRepoMock.Object, userRepoMock.Object, ruleOptions);
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task CreateListing_WhenUserDoesntExist_ShouldThrowException()
    {
        var command = new CreateListing.Command(1, 20, "1");
        
        var listingRepoMock = new Mock<ITicketListingRepository>();
        var ticketRepoMock = new Mock<ITicketRepository>();
        var userRepoMock = new Mock<IUserRepository>();

        userRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AppUser)null);

        var ruleOptions = new PriceRuleOptions();
        
        var handler = new CreateListing.Handler(listingRepoMock.Object, ticketRepoMock.Object, userRepoMock.Object, ruleOptions);
        
        await Assert.ThrowsAsync<ApplicationException>(() => handler.Handle(command, CancellationToken.None));
    }
    
    [Fact]
    public async Task CreateListing_WhenTicketDoesntExist_ShouldThrowException()
    {
        var command = new CreateListing.Command(1, 20, "1");
        
        var listingRepoMock = new Mock<ITicketListingRepository>();
        var ticketRepoMock = new Mock<ITicketRepository>();
        var userRepoMock = new Mock<IUserRepository>();

        userRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AppUser());

        ticketRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Ticket)null);

        var ruleOptions = new PriceRuleOptions();
        
        var handler = new CreateListing.Handler(listingRepoMock.Object, ticketRepoMock.Object, userRepoMock.Object, ruleOptions);
        
        await Assert.ThrowsAsync<Infrastructure.Exceptions.ApplicationException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task CreateListing_WhenMarginIsIncorrect_ShouldThrowException()
    {
        var command = new CreateListing.Command(1, 20, "1");
        
        var listingRepoMock = new Mock<ITicketListingRepository>();
        var ticketRepoMock = new Mock<ITicketRepository>();
        var userRepoMock = new Mock<IUserRepository>();

        userRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AppUser());

        ticketRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Ticket
            {
                Event = new Event
                {
                    TicketPrice = 20
                }
            });

        var ruleOptions = new PriceRuleOptions
        {
            MaximumMarginPercentage = -1
        };
        
        var handler = new CreateListing.Handler(listingRepoMock.Object, ticketRepoMock.Object, userRepoMock.Object, ruleOptions);
        
        await Assert.ThrowsAsync<Infrastructure.Exceptions.ApplicationException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task CreateListing_WhenCommandIsIncorrect_ShouldThrowValidationException()
    {
        var command = new CreateListing.Command(-1, -1, "");
        var validator = new CreateListing.CommandValidator();

        var result = validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.TicketId);
        result.ShouldHaveValidationErrorFor(x => x.Price);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }
}