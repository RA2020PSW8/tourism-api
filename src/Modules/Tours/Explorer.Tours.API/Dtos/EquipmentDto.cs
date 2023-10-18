namespace Explorer.Tours.API.Dtos;

public class EquipmentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsSelected { get; set; }
    public EquipmentDto(int id, string name, string? description, bool isSelected)
    {
        Id = id;
        Name = name;
        Description = description;
        IsSelected = isSelected;
    }
}