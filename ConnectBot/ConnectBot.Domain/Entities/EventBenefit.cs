using ConnectBot.Domain.Enums;

namespace ConnectBot.Domain.Entities;

    public class EventBenefit
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public EventBenefitType EventBenefitType { get; set; }
        public bool IsOneTimeBenefit { get; set; }
    }
