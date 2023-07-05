namespace BillardHazard.Models
{
    public class HighScore
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TeamName { get; set; }
        public int Score { get; set; }
    }
}
