using Lesson_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Interfaces {
    public interface IPlayerController : ICreate {
        void IncreasSkill(int increase);
        void ReduceSkill(int reduce);
        bool CanPlay();
    }
}
