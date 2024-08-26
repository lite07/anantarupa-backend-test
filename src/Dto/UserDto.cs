using Anantarupa.Database.Models;

namespace Anantarupa.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }

        public UserDto(int id, string name, DateTime joinDate)
        {
            Id = id;
            Name = name;
            JoinDate = joinDate;
        }

        public static UserDto FromEntity(UserDatum user)
        {
            string dateString = System.Text.Encoding.UTF8.GetString(user.JoinDate);
            return new UserDto(user.UserId, user.Username, DateTime.Parse(dateString));
        }
    }
}