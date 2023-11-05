using Explorer.Tours.API.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.API.Dtos
{
    public class PublicEntityRequestDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EntityId {  get; set; }
        public EntityType EntityType {  get; set; }
        public PublicEntityRequestStatus Status { get; set; }
        public string? Comment { get; init; }
    }
}
