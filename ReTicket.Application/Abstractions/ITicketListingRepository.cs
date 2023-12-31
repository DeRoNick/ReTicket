﻿using ReTicket.Domain.Models;

namespace ReTicket.Application.Abstractions
{
    public interface ITicketListingRepository
    {
        Task<TicketListing?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<TicketListing>> GetAllForEventAsync(int eventId, CancellationToken cancellationToken);
        Task<int> InsertAsync(TicketListing ticket, CancellationToken cancellationToken);
        Task UpdateAsync(TicketListing ticket, CancellationToken cancellationToken);
    }
}
