﻿namespace BillardHazard.Models
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        private DateTime GameBegin { get; } = DateTime.Now.Date;
    }
}
