using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Models.DataDetails {
    public class GameControllerData : BaseData {
        public string HomeTeamName { get; private set; }
        public string GuestTeamName { get; private set; }

        public GameControllerData(string homeTeamName,string guestTeamName) {
            if (string.IsNullOrEmpty(homeTeamName) || string.IsNullOrEmpty(guestTeamName))
                throw new ArgumentException("GameController data is empty");
            HomeTeamName = homeTeamName;
            GuestTeamName = guestTeamName;
        }

    }
}
