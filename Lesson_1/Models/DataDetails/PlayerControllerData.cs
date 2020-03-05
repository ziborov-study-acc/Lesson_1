using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Models.DataDetails {
    public class PlayerControllerData : BaseData{
        public int PlayerNumber { get; private set; }
        public Competency TrainerCompetency { get; private set; }

        public PlayerControllerData(int playerNumber,Competency trainerCompetency) {

            PlayerNumber = playerNumber;
            TrainerCompetency = trainerCompetency;
        }
    }
}
