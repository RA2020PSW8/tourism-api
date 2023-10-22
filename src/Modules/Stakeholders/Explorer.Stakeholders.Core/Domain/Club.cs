using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
  
    public class Club: Entity 
    {
        public string Name { get; init; }
        public string ?Description { get; init; }

        public string ?Image { get; init; }

        public int UserId { get; init; }

        public List<int> MemberIds { get; init; }

        public Club()
        {
            MemberIds = new List<int>();
        }

        public Club(string name, string? description, string? image, int userId, List<int> memberIds)
        {

            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("Invalid name.");
          

            Name = name; 
            Description = description;
            Image = image;  
            UserId = userId;
            MemberIds = memberIds;
        }


    }
}
