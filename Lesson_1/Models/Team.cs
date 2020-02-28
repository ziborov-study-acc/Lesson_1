using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Models {
    public class Team {
        //Название 
        public string Name { get;private set; }
        //Игроки
        public Player[] Players { get; private set; }
        //Тренер
        public Trainer Trainer { get; private set; }
        //Количество возможных замен
        public int SubstitutionsCount { get; set; }
        //Счёт 
        public int TeamScore { get; set; }

        public Team(string teamName,Trainer trainer,Player[] players) {
            Name = teamName;
            Trainer = trainer;
            Players = players;
            SubstitutionsCount = 3;
        }
        public override string ToString() {
            string output = string.Empty;

            output += $"Team <{Name}>\n";
            output += $"Trainer competency <{Trainer.Competency}>\n";

            foreach (var item in Players)
            {
                output += $"\t{item}\n";
            }
            return output;
        }
    }
}
