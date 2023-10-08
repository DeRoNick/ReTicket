using Moq;
using ReTicket.Application.Abstractions;
using ReTicket.Application.Events.Queries;
using ReTicket.Domain.Models;

namespace ReTicket.Application.Tests.Events.Queries;

public class GetEventsQueryTests
{

    [Fact]
    public async Task GetEventsQuery_WhenEverythingFine_ShouldReturnCorrectResult()
    {
        var command = new GetEvents.Query();
        
        var mock = new Mock<IEventRepository>();
        var domainEvents = new List<Event>
        {
            new()
            {
                Name = "name",
                Location = "tbilisi",
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            },
            new()
            {
                Name = "name2",
                Location = "tbilisi2",
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            }
        };
        
        mock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(domainEvents);
        
        var handler = new GetEvents.Handler(mock.Object);
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(domainEvents.Count, result!.Count);
        Assert.Equal(domainEvents[0].Name, result[0].Name);
        Assert.Equal(domainEvents[0].StartDate, result[0].StartDate);
        Assert.Equal(domainEvents[0].EndDate, result[0].EndDate);
        Assert.Equal(domainEvents[0].Location, result[0].Location);
    }
}