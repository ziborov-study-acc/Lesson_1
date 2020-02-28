using Lesson_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Controllers {
    public class PlayerController {

        private static readonly Random random = new Random();
        //Метод создания нового игрока
        public static Player CreatePlayer(int playerNumber,Competency traineCompetency) {
            return new Player(playerNumber,GenerateLuck(), GenerateSkill(traineCompetency));
        }
        //Генерация уровня игры в зависимости от профессионализма тренера
        private static int GenerateSkill(Competency traineCompetency) {
            int competency = 0;
            switch (traineCompetency)
            {
                case Competency.Low:
                    competency += 10;
                    break;
                case Competency.Middle:
                    competency += 20;
                    break;
                case Competency.High:
                    competency += 30;
                    break;
                default:
                    break;
            }
            return random.Next(competency, 100);
        }
        //Получение удачи
        private static int GenerateLuck() {
            return random.Next(100);
        }

    }
}
