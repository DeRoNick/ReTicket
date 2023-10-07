﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReTicket.Domain.Models;

namespace ReTicket.Persistence.Configurations
{
    public class EventsConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events", "dbo");

            _ = builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Location).HasMaxLength(500);
            builder.Property(x => x.TicketPrice).HasColumnName("money");
            builder.Property(x => x.TicketPrice).HasColumnName("money");


            builder.Property(x => x.StartDate).HasColumnName("datetime");
            builder.Property(x => x.EndDate).HasColumnName("datetime");

        }
    }
}
