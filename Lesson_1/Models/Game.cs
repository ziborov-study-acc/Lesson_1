using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Models {
    //Модель игры
    public class Game {
        //Домашняя команда
        public Team HomeTeam { get; set; }
        //Гостевая команда
        public Team GuestTeam { get; set; }
        //
        public bool IsGameOver { get; set; }

        public Game(Team Home,Team Guest) {
            HomeTeam = Home;
            GuestTeam = Guest;
            IsGameOver = false;
        }
        public override string ToString() { 
            string output = string.Empty;
            output += HomeTeam;
            output += GuestTeam;
            return output;
        }
    }
}
