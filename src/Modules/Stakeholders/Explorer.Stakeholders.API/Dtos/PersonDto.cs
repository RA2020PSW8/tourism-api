﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class PersonDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfileImage { get; set; }
        public string Biography { get; set; }
        public string Quote { get; set; }
        public string Email { get; set; }
        //public ICollection<PersonDto> Followers { get; set; }
        //public ICollection<PersonDto> Following { get; set; }

    }

}
