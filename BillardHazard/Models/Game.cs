namespace BillardHazard.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public ICollection<Team> Teams { get; set; }
        public DateTime Beginning { get; set; }

        public Game()
        {
            Id = Guid.NewGuid();
            Teams = new List<Team>();
            Beginning = DateTime.Now;
        }
    }
}
