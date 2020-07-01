using matrixYT.Entities;

namespace WebApi.Models.Songs
{
  public class SongModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Category Category { get; set; }
        public User User {get; set;}
    }
}