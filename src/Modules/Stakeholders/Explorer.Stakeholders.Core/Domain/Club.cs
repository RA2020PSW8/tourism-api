using System.Threading.Tasks.Dataflow;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class Club : Entity
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string? Image { get; init; }
    public long OwnerId { get; init; }
    public Person Owner { get; init; }
    public ICollection<Person> Members { get; set; }
    
    public Club(){ }

    public Club(string name, string? description, string? image, int userId, List<int> memberIds)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Invalid name.");
        
        Name = name;
        Description = description;
        Image = image;
        OwnerId = userId;
    }
}