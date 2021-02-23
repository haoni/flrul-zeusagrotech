using System.Reflection.Metadata.Ecma335;
namespace FlurlPOC.Dto
{
    public class Credential {
        public User user {get; set; }
    }


    public class User {
        public string email { get; set; }
        public string name { get; set; }
        public int userId { get; set; }
        public string token { get; set; }
        public string role { get; set; }
    }
}