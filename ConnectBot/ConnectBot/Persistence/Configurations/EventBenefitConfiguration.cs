using ConnectBot.Domain.Entities;
using ConnectBot.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectBot.Persistence.Configurations
{
    public class EventBenefitConfiguration : IEntityTypeConfiguration<EventBenefit>
    {
        public void Configure(EntityTypeBuilder<EventBenefit> builder)
        {
            builder.ToTable("EventBenefits", "meta");
            builder.Property(e => e.Content).IsRequired();
            builder.HasData(new List<EventBenefit>
            {
                new ()
                {
                    Id = 1,
                    EventBenefitType = EventBenefitType.Bonus,
                    Content = "Cпециальное блюдо только для тебя! Просто подойди на кухню и покажи это сообщение."
                },
                new ()
                {
                    Id = 2,
                    EventBenefitType = EventBenefitType.Bonus,
                    Content = "Отдельный напиток только для тебя! Просто подойди на кухню и покажи это сообщение."
                },
                new ()
                {
                    Id = 3,
                    EventBenefitType = EventBenefitType.Bonus,
                    Content = "Минута славы! Мы официально тебя представим как главного гостя нашего Коннекта"
                },
                new ()
                {
                    Id = 4,
                    EventBenefitType = EventBenefitType.Bonus,
                    Content = "Мы отметим тебя в инстаграмме нашего Коннекта!"
                },
                new ()
                {
                    Id = 5,
                    EventBenefitType = EventBenefitType.Bonus,
                    Content = "Ты получишь оригинальный комплимент от молодежной команды!"
                },
                new ()
                {
                    Id = 6,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Помочь убраться команде после Коннекта. Спасибо наперед!"
                },
                new ()
                {
                    Id = 7,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Сказать комплимент кухне в микрофон."
                },
                new ()
                {
                    Id = 8,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Весь Коннект называть себя другим именем."
                },
                new ()
                {
                    Id = 9,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Сделать незаметно смешную фотку кого-то из молодежной команды."
                },
                new ()
                {
                    Id = 10,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Крикнуть «вот это кринж» после какой-то игры."
                },
                new ()
                {
                    Id = 11,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Называть и вести себя как человек паук ввесь Коннект."
                },
                new ()
                {
                    Id = 12,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Через каждые 20 минут говорить «я люблю вас!»."
                },
                new ()
                {
                    Id = 13,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Через каждые 20 минут говорить «я люблю вас!»."
                },
                new ()
                {
                    Id = 14,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "После звука сирены \ud83d\udea8 говорить громко «Окак»."
                },
                new ()
                {
                    Id = 15,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "После звука сирены \ud83d\udea8 говорить громко «Абаюдна»."
                },
                new ()
                {
                    Id = 16,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "После звука сирены \ud83d\udea8 говорить громко «это фиаско братан»."
                }
            });
        }
    }
}
