using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Models {
    //Уровень компетенции/профессионализма тренера команды
    public enum Competency { Low, Middle ,High}
    //Модель тренера
    public class Trainer {
        //Компетенция/Профессионализм
        public Competency Competency { get; private set; }
        //Конструктор
        public Trainer(Competency competency) {
            Competency = competency;
        }

    }
}
