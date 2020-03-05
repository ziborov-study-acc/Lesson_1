using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Models.DataDetails {
    public class TeamControllerData : BaseData {
        public string TeamName { get; private set; }
        public TeamControllerData(string teamName) {
            if (string.IsNullOrEmpty(teamName))
                throw new ArgumentException("Player Number is empty");
            TeamName = teamName;
        }
    }
}
