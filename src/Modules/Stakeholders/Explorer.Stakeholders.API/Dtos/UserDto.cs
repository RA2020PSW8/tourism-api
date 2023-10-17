using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.API.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public string Mail {  get; set; }
        public bool IsBlocked {  get; set; }
    }
    public enum UserRole
    {
        Administrator,
        Author,
        Tourist
    }

}
