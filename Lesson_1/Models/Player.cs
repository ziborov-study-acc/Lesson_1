using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Models {
	//Модель игрока
    public class Player {
		//Номер игрока
		public int PlayerNumber { get;private set; }
		//удача
		private int _luck;
		public int Luck {
			get { return _luck; }
			set {
				if (value < 0)
					throw new Exception("Player can't have negative luck");
				_luck = value;
			}
		}

		private int _skill;
		//Уровень игры
		public int Skill {
			get { return _skill; }
			set {
				if (value < 0)
				{
					value = 0;
				}
				if (value > 100)
				{
					_skill = 100;
					return;
				}
				_skill = value;
			}
		}

		public bool CanPlay { get; set; }

		public Player(int number,int luck,int skill) {
			PlayerNumber = number;
			Luck = luck;
			Skill = skill;
			CanPlay = true;	
		}

		public override string ToString() {
			return $"Player <{PlayerNumber}> : Luck <{Luck}> , Skill <{Skill}>";
		}


	}
}
