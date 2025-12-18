namespace GameStore.Models
{
    public class Game
    {
        public int Id;
        public string Name;
        public string Genre;
        public double Rating;
        public decimal Price;

        public Game(int id, string name, string genre, double rating, decimal price)
        {
            Id = id;
            Name = name;
            Genre = genre;
            Rating = rating;
            Price = price;
        }

        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(Name);
        }
    }
}
