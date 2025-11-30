namespace GameStore.Models
{
    public struct Client
    {
        public int Id;
        public string Name;
        public string Email;

        public Client(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Email);
        }
    }
}
