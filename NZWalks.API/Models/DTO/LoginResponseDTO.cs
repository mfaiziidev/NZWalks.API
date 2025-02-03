namespace NZWalks.API.Models.DTO
{
    public class LoginResponseDTO
    {
        public string JwtToken { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}


//public class LoginResponseDTO
//{
//    public string JwtToken { get; set; }
//    public int UserId { get; set; }
//    public string Username { get; set; }
//    public string Email { get; set; }
//    public List<string> Roles { get; set; }  // Include roles as a list of strings
//}
