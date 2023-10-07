using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReTicket.Domain.Models;

namespace ReTicket.Persistence.Configurations
{
    public class TicketListingsConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            //builder.Property(x => x.FullName).HasMaxLength(150);
            //builder.HasKey(x => x.Id);
        }
    }
}
