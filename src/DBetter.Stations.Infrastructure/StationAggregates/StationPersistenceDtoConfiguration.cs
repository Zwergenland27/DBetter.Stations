using DBetter.Stations.Domain.StationAggregates.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBetter.Stations.Infrastructure.StationAggregates;

internal class StationPersistenceDtoConfiguration: IEntityTypeConfiguration<StationPersistenceDto>
{
    public void Configure(EntityTypeBuilder<StationPersistenceDto> builder)
    {
        builder.ToTable("Stations");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.EvaNumber)
            .IsUnique();

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Ril100Code)
            .HasMaxLength(Ril100Code.MaxLength);
    }
}