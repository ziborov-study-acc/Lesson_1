using Lesson_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Controllers {
    public class TrainerController {
        private static readonly Random random = new Random();
        //Получение нового тренера
        public static Trainer CreateTrainer() {
            return new Trainer(GetRandomCompetency());
        }
        //Случайный уровень профессионализма
        private static Competency GetRandomCompetency() {
            return (Competency)random.Next(Enum.GetValues(typeof(Competency)).Length);
        }
    }
}
