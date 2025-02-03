﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public RoleDTO Role { get; set; }
    }
}
