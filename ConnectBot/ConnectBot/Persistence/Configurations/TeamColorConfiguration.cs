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
                    Name = "Blue",
                    ColorSymbol = "\U0001F7E6"
                },
                new ()
                {
                    Id = 2,
                    Name = "Red",
                    ColorSymbol = "\U0001F7E5"
                },
                new ()
                {
                    Id = 3,
                    Name = "Yellow",
                    ColorSymbol = "\U0001F7E8"
                },
                new ()
                {
                    Id = 4,
                    Name = "Green",
                    ColorSymbol = "\U0001F7E9"
                },
                new ()
                {
                    Id = 5,
                    Name = "Orange",
                    ColorSymbol = "\U0001F7E7"
                },
                new ()
                {
                    Id = 6,
                    Name = "Purple",
                    ColorSymbol = "\U0001F7EA"
                },
                new ()
                {
                    Id = 7,
                    Name = "White",
                    ColorSymbol = "\U00002B1C"
                },
                new ()
                {
                    Id = 8,
                    Name = "Black",
                    ColorSymbol = "\U00002B1B"
                }
            });
        }
    }
}
