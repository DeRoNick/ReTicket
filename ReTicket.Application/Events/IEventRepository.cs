using ReTicket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTicket.Application.Events
{
    public interface IEventRepository
    {
        Task<Event> GetAsync(string title, CancellationToken cancellationToken);
        Task<Event> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Event>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(Event book, CancellationToken cancellationToken);
        Task<int> CreateAsync(Event book, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
