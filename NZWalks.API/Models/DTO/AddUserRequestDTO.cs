﻿namespace NZWalks.API.Models.DTO
{
    public class AddUserRequestDTO
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
