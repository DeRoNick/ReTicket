using Moq;
using ReTicket.Application.Abstractions;
using ReTicket.Application.TicketListings.Queries;
using ApplicationException = ReTicket.Application.Infrastructure.Exceptions.ApplicationException;

namespace ReTicket.Application.Tests.TicketListing.Queries;

public class GetListingByIdQueryTests
{
    
    [Fact]
    public async Task GetListingById_WhenEverythingFine_ShouldReturnCorrectResult()
    {
        var command = new GetListingById.Query(1);
        
        var ticketListingRepoMock = new Mock<ITicketListingRepository>();
        var returns = new Domain.Models.TicketListing
        {
            Id = 1,
            EventId = 1,
            Price = 10,
        };
        
        ticketListingRepoMock.Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(returns);
        
        var handler = new GetListingById.Handler(ticketListingRepoMock.Object);
        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(returns.Id, result.Id);
        Assert.Equal(returns.EventId, result.EventId);
        Assert.Equal(returns.Price, result.Price);
    }
    
    [Fact]
    public async Task GetListingById_WhenIdNotCorrect_ShouldThrowExceptionWithErrorMessage()
    {
        var command = new GetListingById.Query(0);
        
        var ticketListingRepoMock = new Mock<ITicketListingRepository>();
        var handler = new GetListingById.Handler(ticketListingRepoMock.Object);
        
        var exception = await Assert.ThrowsAsync<ApplicationException>(() => handler.Handle(command, CancellationToken.None));
        
        Assert.Equal("No Ticket Listing with given Id found", exception.Message);
    }
}