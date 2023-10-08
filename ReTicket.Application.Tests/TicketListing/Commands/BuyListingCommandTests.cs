using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using ReTicket.Application.Abstractions;
using ReTicket.Application.Rules;
using ReTicket.Application.TicketListings.Commands;
using ReTicket.Domain.Models;

namespace ReTicket.Application.Tests.TicketListing.Commands;

public class BuyListingCommandTests
{
    
    [Fact]
    public async Task BuyListing_WhenEverythingFine_ShouldReturnCorrectResult()
    {
        var command = new BuyListing.Command(1, "1");
        
        var ticketListingRepoMock = new Mock<ITicketListingRepository>();
        var ticketListing = new Domain.Models.TicketListing
        {
            Id = 1,
            EventId = 1,
            Price = 10,
        };
        ticketListingRepoMock.Setup(x => x.GetByIdAsync(command.ListingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ticketListing);
        
        var ticketRepoMock = new Mock<ITicketRepository>();
        var ticket = new Domain.Models.Ticket
        {
            Id = 1,
            EventId = 1,
        };
        ticketRepoMock.Setup(x => x.GetByIdAsync(ticketListing.TicketId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ticket);

        var transactionProviderMock = new Mock<ITransactionProvider>();

        var transactionMock = new Mock<IDbContextTransaction>();
        
        transactionProviderMock.Setup(x => x.CreateTransactionScope(It.IsAny<CancellationToken>()))
            .ReturnsAsync(transactionMock.Object);

        var userRepoMock = new Mock<IUserRepository>();
        var user = new Domain.Models.AppUser
        {
            Id = "1",
        };
        userRepoMock.Setup(x => x.GetByIdAsync(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        
        var handler = new BuyListing.Handler(ticketListingRepoMock.Object, ticketRepoMock.Object, userRepoMock.Object, new PriceRuleOptions
        {
            MarginCommissionPercentage = 2,
            MaximumMarginPercentage = 5
        }, transactionProviderMock.Object);
        await handler.Handle(command, CancellationToken.None);
    }

    [Fact]
    public async Task BuyListing_WhenUserDoesntExist_ShouldThrowException()
    {
        var command = new BuyListing.Command(1, "1");
        
        var ticketListingRepoMock = new Mock<ITicketListingRepository>();
        
        var ticketRepoMock = new Mock<ITicketRepository>();

        var transactionProviderMock = new Mock<ITransactionProvider>();

        var userRepoMock = new Mock<IUserRepository>();
        var user = new Domain.Models.AppUser
        {
            Id = "1",
        };
        userRepoMock.Setup(x => x.GetByIdAsync(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Models.AppUser)null);
        
        var handler = new BuyListing.Handler(ticketListingRepoMock.Object, ticketRepoMock.Object, userRepoMock.Object, new PriceRuleOptions
        {
            MarginCommissionPercentage = 2,
            MaximumMarginPercentage = 5
        }, transactionProviderMock.Object);
        await Assert.ThrowsAsync<Infrastructure.Exceptions.ApplicationException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task BuyListing_WhenListingDoesntExist_ShouldThrowException()
    {
        var command = new BuyListing.Command(1, "1");
        
        var ticketListingRepoMock = new Mock<ITicketListingRepository>();
        ticketListingRepoMock.Setup(x => x.GetByIdAsync(command.ListingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Models.TicketListing)null);
        
        var ticketRepoMock = new Mock<ITicketRepository>();

        var transactionProviderMock = new Mock<ITransactionProvider>();

        var userRepoMock = new Mock<IUserRepository>();
        var user = new Domain.Models.AppUser
        {
            Id = "1",
        };
        userRepoMock.Setup(x => x.GetByIdAsync(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AppUser());
        
        var handler = new BuyListing.Handler(ticketListingRepoMock.Object, ticketRepoMock.Object, userRepoMock.Object, new PriceRuleOptions
        {
            MarginCommissionPercentage = 2,
            MaximumMarginPercentage = 5
        }, transactionProviderMock.Object);
        await Assert.ThrowsAsync<Infrastructure.Exceptions.ApplicationException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task BuyListing_WhenTicketDoesntExist_ShouldThrowException()
    {
        var command = new BuyListing.Command(1, "1");
        
        var ticketListingRepoMock = new Mock<ITicketListingRepository>();
        
        var ticketRepoMock = new Mock<ITicketRepository>();
        ticketRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Ticket)null);

        var transactionProviderMock = new Mock<ITransactionProvider>();

        var userRepoMock = new Mock<IUserRepository>();
        var user = new AppUser
        {
            Id = "1",
        };
        userRepoMock.Setup(x => x.GetByIdAsync(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AppUser());
        
        var handler = new BuyListing.Handler(ticketListingRepoMock.Object, ticketRepoMock.Object, userRepoMock.Object, new PriceRuleOptions
        {
            MarginCommissionPercentage = 2,
            MaximumMarginPercentage = 5
        }, transactionProviderMock.Object);
        await Assert.ThrowsAsync<Infrastructure.Exceptions.ApplicationException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task BuyListing_WhenCommandIsIncorrect_ShouldThrowException()
    {
        var command = new BuyListing.Command(-1, "");
        var validator = new BuyListing.CommandValidator();
        
        var result = validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.ListingId);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }
}