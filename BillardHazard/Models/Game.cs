namespace BillardHazard.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public ICollection<Team> Teams { get; } = new List<Team>();
        private DateTime GameBegin { get; } = DateTime.Now.Date;
    }
}
