using ReTicket.Application.Events;
using ReTicket.Application.Tickets;
using ReTicket.Domain.Models;
using ReTicket.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTicket.Infrastructure.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(ReTicketDbContext context) : base(context)
        {
        }

        public Task<int> CreateAsync(Event book, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetAsync(string title, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Event book, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
