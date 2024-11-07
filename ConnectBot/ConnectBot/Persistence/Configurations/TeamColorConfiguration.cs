using ConnectBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectBot.Persistence.Configurations
{
    public class TeamColorConfiguration : IEntityTypeConfiguration<TeamColor>
    {
        public void Configure(EntityTypeBuilder<TeamColor> builder)
        {
            builder.HasData(new List<TeamColor>()
            {
                new ()
                {
                    Id = 1,
                    Name = "Blue"
                },
                new ()
                {
                    Id = 2,
                    Name = "Red"
                },
                new ()
                {
                    Id = 3,
                    Name = "Yellow"
                },
                new ()
                {
                    Id = 4,
                    Name = "Green"
                },
                new ()
                {
                    Id = 5,
                    Name = "Orange"
                },
                new ()
                {
                    Id = 6,
                    Name = "Purple "
                }
            });
        }
    }
}
