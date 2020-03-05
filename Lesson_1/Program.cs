using Lesson_1.Controllers;
using Lesson_1.Models.DataDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1 {
    class Program {
        static void Main(string[] args) {
            GameController manager = new GameController();
            GameControllerData gameData = new GameControllerData("Home", "Guest");
            manager.Create(gameData);

            Console.WriteLine(manager.Game);

            manager.Start();
            Console.ReadLine();
        }
    }
}
