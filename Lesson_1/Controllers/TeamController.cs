using Lesson_1.Helpers;
using Lesson_1.Interfaces;
using Lesson_1.Models;
using Lesson_1.Models.DataDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Controllers {
    public class TeamController : ITeamController {
        //Событие при каком-либо действии
        //TODO сделать EventArgs
        public delegate void TeamHandler(object sender,TeamEventArgs e);
        public event TeamHandler TeamNotify;

        //делегат для действий
        public delegate void DoAction();
        DoAction[] actions;

        private static readonly Random random = new Random();
        //Объект команды
        public Team Team { get; private set; }
        //
        public PlayerController[] PlayerControllers { get; private set; }
        public TeamController() {
            
            actions = new DoAction[] {PlayerInjure,FanSupport,TryScoreGoal };
        }

        //инициализация команды
        public void Create(BaseData data) {
            if (!(data is TeamControllerData))
                throw new ArgumentException("Invalid data");

            Trainer trainer = TrainerController.CreateTrainer();
            PlayerControllers = new PlayerController[11];
            Player[] players = new Player[11];

            for (int i = 0; i < PlayerControllers.Length; i++)
            {
                PlayerControllers[i] = new PlayerController();
                PlayerControllers[i].Create(new PlayerControllerData( i + 1, trainer.Competency));
                players[i] = PlayerControllers[i].Player;
            }

            Team = new Team((data as TeamControllerData).TeamName, trainer, players);
            TeamNotify += OnTeamEvent;
        }

        //Случайное действие команды
        public void GetRandAction() {
            //TODO сделать шанс

            actions[random.Next(0, actions.Length)]();
        }

        //Травма игрока
        private void PlayerInjure() {

            PlayerController playerToInjure = PlayerControllers.ElementAt(random.Next(0, PlayerControllers.Length));

            if (!playerToInjure.CanPlay())
                return;

            int injure = random.Next(1, 15);
            playerToInjure.ReduceSkill(injure);

            if (!playerToInjure.CanPlay())
            {
                TeamNotify?.Invoke(this, new TeamEventArgs($"player <{playerToInjure.Player.PlayerNumber}> injured and cannot continue"));
                PlayerSubstitution(playerToInjure);
            }
            else
                TeamNotify?.Invoke(this, new TeamEventArgs($"player <{playerToInjure.Player.PlayerNumber}> injured "));

        }

        //Замена игрока
        private void PlayerSubstitution(PlayerController playerController) {
            if (Team.SubstitutionsCount == 0)
                return;
            --Team.SubstitutionsCount;
            playerController.Create(new PlayerControllerData(playerController.Player.PlayerNumber, Team.Trainer.Competency));
            TeamNotify?.Invoke(this, new TeamEventArgs($"player <{playerController.Player.PlayerNumber}> has been replaced"));

        }
        //Фанатская поддержка
        private void FanSupport() {
            PlayerController playerToSupport = PlayerControllers.ElementAt(random.Next(0, PlayerControllers.Length));

            if (!playerToSupport.CanPlay())
                return; 
            int support = random.Next(1, 10);
            playerToSupport.IncreasSkill(support);

            TeamNotify?.Invoke(this, new TeamEventArgs($"player <{playerToSupport.Player.PlayerNumber}> had fan support"));
        }
        //Попытка забить гол
        private void TryScoreGoal() {
            PlayerController playerController =PlayerControllers.ElementAt(random.Next(0, PlayerControllers.Length));

            if (!playerController.CanPlay())
                return;

            int chance = random.Next(1, 200);

            if (chance <= playerController.PlayerPower)
            {
                Team.TeamScore++;
                TeamNotify?.Invoke(this, new TeamEventArgs($"player <{playerController.Player.PlayerNumber}> scored goal | Team score:{Team.TeamScore}"));
            }
            else
                TeamNotify?.Invoke(this, new TeamEventArgs($"player <{playerController.Player.PlayerNumber}> missed"));
        }

        //Может ли состав команды продолжать игру
        public bool IsCanContinue() {
            return PlayerControllers.Count(player => player.CanPlay()) < 7;
        }
        //Обработка событий команды
        private void OnTeamEvent(object sender, TeamEventArgs e) {
            Console.WriteLine($"Team Event : {(sender as TeamController).Team.Name} team - {e.Message}");
        }
    }
}
