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
                    Content = "Cпециальное блюдо только для тебя! Просто подойди на кухню и покажи это сообщение.",
                    IsOneTimeBenefit = true
                },
                new ()
                {
                    Id = 2,
                    EventBenefitType = EventBenefitType.Bonus,
                    Content = "Отдельный напиток только для тебя! Просто подойди на кухню и покажи это сообщение.",
                    IsOneTimeBenefit = false
                },
                new ()
                {
                    Id = 3,
                    EventBenefitType = EventBenefitType.Bonus,
                    Content = "Минута славы! Мы официально тебя представим как главного гостя нашего Коннекта."
                },
                new ()
                {
                    Id = 4,
                    EventBenefitType = EventBenefitType.Bonus,
                    Content = "Мы отметим тебя в инстаграме нашего Коннекта! Подойди к Кате.",
                    IsActive = false
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
                    Content = "Сделать незаметно смешную фотку кого-то из молодежной команды до конца Коннекта и скинуть ее технической команде.",
                    IsActive = false
                },
                new ()
                {
                    Id = 10,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Крикнуть «вот это кринж!» после любой игры."
                },
                new ()
                {
                    Id = 11,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Называть и вести себя как человек-паук ввесь Коннект."
                },
                new ()
                {
                    Id = 12,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Через каждые 30 минут говорить «Я люблю вас, булочки!»."
                },
                new ()
                {
                    Id = 13,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "После звука дверного звонка говорить громко «Окак!».",
                    IsActive = false
                },
                new ()
                {
                    Id = 14,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "После звука сирены \ud83d\udea8 говорить громко «Абаюдна!».",
                    IsActive = false
                },
                new ()
                {
                    Id = 15,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "После звука барабана говорить громко «это фиаско братан!».",
                    IsActive = false
                },
                new ()
                {
                    Id = 16,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Носить странную кепку ввесь Коннект. Подойти к организаторам чтобы получить её.",
                    IsActive = false
                },
                new ()
                {
                    Id = 17,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Найти самого тихого человека и поддержать его."
                },
                new ()
                {
                    Id = 18,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Стать «амбассадором кухни» и рекламировать еду."
                },
                new ()
                {
                    Id = 19,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "Спросить у 3 людей: «Какой твой вайб сегодня?»"
                },
                new ()
                {
                    Id = 20,
                    EventBenefitType = EventBenefitType.Task,
                    Content = "До конца Коннекта здороваться со всеми как ведущий новостей."
                }
            });
        }
    }
}
