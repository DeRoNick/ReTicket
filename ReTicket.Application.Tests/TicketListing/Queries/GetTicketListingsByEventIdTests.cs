using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using ReTicket.Application.Abstractions;
using ReTicket.Application.TicketListings.Query;

namespace ReTicket.Application.Tests.TicketListing.Queries;

public class GetTicketListingsByEventIdTests
{

    [Fact]
    public async Task GetTicketListingsByEventId_WhenEverythingFine_ShouldReturnCorrectResult()
    {
        var command = new GetTicketListingsByEventId.Query(1);
        var ticketListingRepoMock = new Mock<ITicketListingRepository>();
        var returns = new List<Domain.Models.TicketListing>
        {
            new Domain.Models.TicketListing
            {
                Id = 1,
                EventId = 1,
                Price = 10,
            },
            new Domain.Models.TicketListing
            {
                Id = 2,
                EventId = 1,
                Price = 20,
            },
        };
        
        ticketListingRepoMock.Setup(x => x.GetAllForEventAsync(command.EventId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(returns);
        
        var handler = new GetTicketListingsByEventId.Handler(ticketListingRepoMock.Object);
        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(returns.Count, result.Count);
        Assert.Equal(returns[0].Id, result[0]!.Id);
        Assert.Equal(returns[0].EventId, result[0]!.EventId);
        Assert.Equal(returns[0].Price, result[0]!.Price);
        Assert.Equal(returns[1].Id, result[1]!.Id);
        Assert.Equal(returns[1].EventId, result[1]!.EventId);
        Assert.Equal(returns[1].Price, result[1]!.Price);
    }

    [Fact]
    public async Task GetTicketListingsByEventId_WhenIdNotCorrect_ShouldThrowExceptionWithErrorMessage()
    {
        var command = new GetTicketListingsByEventId.Query(-1);
        var validator = new GetTicketListingsByEventId.QueryValidator();

        var result = validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.EventId);
    }
}