using BillardHazard.Models;

namespace BillardHazard.Tools
{
    public static class DefaultRules
    {
        public static List<Rule> DefaultRulesList
            = new List<Rule>() {
                new Rule() { Id = Guid.NewGuid(), Explanation = "Toucher 2 bandes avec la boule blanche." , Points = 1 },
                new Rule() { Id = Guid.NewGuid(), Explanation = "Rentrer 2 boules en un tour quelque soit leur couleur.", Points = 3 },
                new Rule() { Id = Guid.NewGuid(), Explanation = "Jouer le prochain coup avec la queue dans le dos." , Points = 1 },
                new Rule() { Id = Guid.NewGuid(), Explanation = "Toucher 2 boules avec la blanche.", Points = 1 },
                new Rule() { Id = Guid.NewGuid(), Explanation = "Toucher une boule et une seule avec la blanche.", Points = 1 },
                new Rule() { Id = Guid.NewGuid(), Explanation = "Tirer le prochain coup en fermant les yeux (on peut se positionner les yeux ouverts).", Points = 1 },
                new Rule() { Id = Guid.NewGuid(), Explanation = "Jouer le prochain coup en inversant les mains habituellement utilisées pour jouer (un droitier jouera comme un gaucher et vice versa).", Points = 1 },
                new Rule() { Id = Guid.NewGuid(), Explanation = "Rentrer une boule dans une poche spécifique (annoncer la poche avant de tirer).", Points = 2 },
                new Rule() { Id = Guid.NewGuid(), Explanation = "Rentrer une boule sans toucher les bandes.", Points = 3 },
                new Rule() { Id = Guid.NewGuid(), Explanation = "Toucher une bande avec n'importe quelle boule avant de rentrer une boule.", Points = 2 }
            };
    }
}
