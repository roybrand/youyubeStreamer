namespace matrixYT.Entities
{
    public class Song
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public Category Category { get; set; }

        public User User {get; set;}
    }
}   