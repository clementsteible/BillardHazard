namespace BillardHazard.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Number { get; set; }
        public int Score { get; set; }
        public string? Color { get; set; }
        public bool IsItsTurn { get; set; }
        public Guid GameId { get; set; }
    }
}
