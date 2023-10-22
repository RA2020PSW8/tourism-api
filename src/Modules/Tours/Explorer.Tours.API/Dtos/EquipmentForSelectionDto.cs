using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class EquipmentForSelectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsSelected { get; set; }

        public EquipmentForSelectionDto(int id, string name, string? description, bool isSelected)
        {
            Id = id;
            Name = name;
            Description = description;
            IsSelected = isSelected;
        }
    }
}
