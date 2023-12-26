

using Explorer.Stakeholders.API.Dtos.Enums;

namespace Explorer.Stakeholders.API.Dtos
{
    public class AchievementDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AchievementType Type { get; set; }
    }
}
