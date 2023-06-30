namespace BillardHazard.Models
{
    public class Game
    {
        public int Id { get; set; }
        public ICollection<Team> Teams { get; } = new List<Team>();
    }
}
