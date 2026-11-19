using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBetter.Stations.Infrastructure.Messaging;

public class OutboxConfiguration: IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");

        builder.HasKey(m => m.Id);

        builder.HasIndex(m => new { m.ProcessedAt, m.FailedAt, m.OccurredAt });
        
        builder.Property(m => m.Id)
            .ValueGeneratedNever();

        builder.Property(m => m.EventType)
            .HasMaxLength(512);

        builder.Property(m => m.Payload)
            .HasColumnType("jsonb");
    }
}