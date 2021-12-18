using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlanesRabbitMQ.Components.Helpers;
using PlanesRabbitMQ.Contracts.Enums;

namespace PlanesRabbitMQ.Components
{
    class BatchStateEntityConfiguration :
        IEntityTypeConfiguration<BatchState>
    {
        public void Configure(EntityTypeBuilder<BatchState> builder)
        {
            builder.HasKey(c => c.CorrelationId);

            builder.Property(c => c.CorrelationId)
                .ValueGeneratedNever()
                .HasColumnName("BatchId");

            builder.Property(c => c.CurrentState).IsRequired();

            builder.Property(c => c.Action)
                .HasConversion<string>(
                    v => v.ToString(),
                    v => (BatchAction)Enum.Parse(typeof(BatchAction), v));

            builder.Property(c => c.UnprocessedPlaneIds)
                .HasConversion(new JsonValueConverter<Stack<Guid>>());

            builder.Property(c => c.ProcessingPlanesIds)
                .HasConversion(new JsonValueConverter<Dictionary<Guid, Guid>>());
        }
    }
}