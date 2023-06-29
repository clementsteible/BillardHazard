namespace BillardHazard.Models
{
    public class Game
    {
        public int Id { get; set; }
        public ICollection<Team> Equipes { get; } = new List<Team>();
    }
}
