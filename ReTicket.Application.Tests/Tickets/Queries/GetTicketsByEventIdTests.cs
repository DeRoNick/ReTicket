using Moq;
using ReTicket.Application.Abstractions;
using ReTicket.Application.Tickets.Query;
using ReTicket.Domain.Enums;
using ReTicket.Domain.Models;

namespace ReTicket.Application.Tests.Tickets.Queries;

public class GetTicketsByEventIdTests
{

    [Fact]
    public async Task GetTicketsByEventId_WhenEverythingRight_ShouldReturnCorrectResult()
    {
        var query = new GetTicketsByEventId.Query(1);
        
        var ticketRepoMock = new Mock<ITicketRepository>();
        
        var returns = new List<Ticket>()
        {
            new Ticket()
            {
                Id = 1,
                EventId = 1,
                Status = TicketStatus.Reserved
            },
            new Ticket()
            {
                Id = 2,
                EventId = 1,
                Status = TicketStatus.Sold
            },
            new Ticket()
            {
                Id = 3,
                EventId = 1,
                Status = TicketStatus.ForSale
            }
        };
        
        ticketRepoMock.Setup(x => x.GetAllForEventAsync(query.EventId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(returns);
        
        var handler = new GetTicketsByEventId.Handler(ticketRepoMock.Object);
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.Equal(returns.Count, result.Count);
        Assert.Equal(returns[0].Id, result[0]!.Id);
        Assert.Equal(returns[0].EventId, result[0]!.EventId);
        Assert.Equal(returns[0].Status, result[0]!.Status);
        Assert.Equal(returns[1].Id, result[1]!.Id);
        Assert.Equal(returns[1].EventId, result[1]!.EventId);
        Assert.Equal(returns[1].Status, result[1]!.Status);
        Assert.Equal(returns[2].Id, result[2]!.Id);
        Assert.Equal(returns[2].EventId, result[2]!.EventId);
        Assert.Equal(returns[2].Status, result[2]!.Status);
    }
}