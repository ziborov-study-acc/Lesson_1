using Lesson_1.Interfaces;
using Lesson_1.Models;
using Lesson_1.Models.DataDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Controllers {
    public class PlayerController : IPlayerController {
        public Player Player { get; private set; }

        public int PlayerPower => Player.Skill + Player.Luck;

        private static readonly Random random = new Random();
        //Метод создания нового игрока
        public void Create(BaseData data) {
            if (!(data is PlayerControllerData))
                throw new ArgumentException("Invalid data");

            Player = new Player((data as PlayerControllerData).PlayerNumber, GenerateLuck(), GenerateSkill((data as PlayerControllerData).TrainerCompetency));
        }

        //Генерация уровня игры в зависимости от профессионализма тренера
        private int GenerateSkill(Competency traineCompetency) {
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
        private int GenerateLuck() {
            return random.Next(100);
        }

        public void IncreasSkill(int increase) {
            Player.Skill += increase;
        }

        public void ReduceSkill(int reduce) {
            Player.Skill -= reduce;

            if (Player.Skill == 0)
                Player.CanPlay = false;
        }

        public bool CanPlay() {
            return Player.CanPlay;
        }


    }
    
}
