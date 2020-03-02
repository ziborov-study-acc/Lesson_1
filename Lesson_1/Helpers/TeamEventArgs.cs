using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Helpers {
    public class TeamEventArgs {
        public string Message { get; private set; }
        public TeamEventArgs(string message) {
            Message = message;
        }
    }
}
