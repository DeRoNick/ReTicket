using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReTicket.Domain.Enums;
using ReTicket.Domain.Models;

namespace ReTicket.Persistence.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets", "dbo");
            _ = builder.HasKey(x => x.Id);

            _ = builder.HasOne<Event>(s => s.Event)
                .WithMany(g => g.Tickets)
                .HasForeignKey(s => s.EventId).OnDelete(DeleteBehavior.NoAction);

            _ = builder.HasOne<AppUser>(s => s.User)
                .WithMany(g => g.Tickets)
                .HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Code).ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(TicketStatus.ForSale);
        }
    }
}
