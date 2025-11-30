namespace GameStore.Models
{
    public struct Game
    {
        public int Id;
        public string Name;
        public string Genre;
        public double Rating;
        public double Price;

        public Game(int id, string name, string genre, double rating, double price)
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