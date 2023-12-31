using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using ReTicket.Application.Abstractions;
using ReTicket.Application.Events.Queries;
using ReTicket.Domain.Models;

namespace ReTicket.Application.Tests.Events.Queries;

public class GetEventQueryTests
{

    [Fact]
    public async Task GetEventQueryHandler_WhenEverythingFine_ShouldReturnCorrectResult()
    {
        var id = 1;
        var command = new GetEvent.Query(id);

        var mock = new Mock<IEventRepository>();
        var domainEvent = new Event
        {
            Name = "name",
            Location = "tbilisi",
            StartDate = DateTime.MinValue,
            EndDate = DateTime.MaxValue,
        };
        mock.Setup(x => x.GetByIdAsync(It.Is<int>(x => x == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(domainEvent);

        var handler = new GetEvent.Handler(mock.Object);

        var result = await handler.Handle(new GetEvent.Query(id), CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(domainEvent.Name, result!.Name);
        Assert.Equal(domainEvent.StartDate, result.StartDate);
        Assert.Equal(domainEvent.EndDate, result.EndDate);
        Assert.Equal(domainEvent.Location, result.Location);
    }

    [Fact]
    public async Task GetEventQueryHandler_WhenIdNotPositive_ShouldThrowExceptionWithErrorMessage()
    {
        var command = new GetEvent.Query(-1);
        var validator = new GetEvent.QueryValidator();

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}