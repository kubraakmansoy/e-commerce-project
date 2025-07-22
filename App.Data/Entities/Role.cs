namespace App.Data.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Navigation Property
        public List<User>? Users { get; set; }
    }
}
