using Lesson_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1.Controllers {
    public class TeamController {
        //Событие при каком-либо действии
        public delegate void TeamHandler(string message);
        public event TeamHandler TeamNotify;

        //делегат для действий
        public delegate void DoAction();
        DoAction[] actions;

        private static readonly Random random = new Random();

        //Объект команды
        public Team Team { get; private set; }
       
        public TeamController() {
            
            actions = new DoAction[] {PlayerInjure,FanSupport,TryScoreGoal };
        }

        //инициализация команды
        public void CreateTeam(string teamName) {
            Trainer trainer = TrainerController.CreateTrainer();
            Player[] players = new Player[11];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = PlayerController.CreatePlayer(i + 1, trainer.Competency);
            }
            Team = new Team(teamName, trainer, players);
            TeamNotify += OnTeamEvent;
        }

        //Случайное действие команды
        public void GetRandAction() {
            //TODO сделать шанс

            actions[random.Next(0, actions.Length)]();
        }

        //Травма игрока
        private void PlayerInjure() {

            Player playerToInjure = Team.Players.ElementAt(random.Next(0, Team.Players.Length));

            if (!playerToInjure.CanPlay)
                return;

            int injure = random.Next(1, 15);
            playerToInjure.Skill -= injure;

            if (playerToInjure.Skill == 0)
            {
                playerToInjure.CanPlay = false;
                TeamNotify?.Invoke($"{Team.Name} team player <{playerToInjure.PlayerNumber}> injured and cannot continue");
                PlayerSubstitution(playerToInjure);
            }
            else
                TeamNotify?.Invoke($"{Team.Name} team player <{playerToInjure.PlayerNumber}> injured ");

        }

        //Замена игрока
        private void PlayerSubstitution(Player player) {
            if (Team.SubstitutionsCount == 0)
                return;
            --Team.SubstitutionsCount;
            player = PlayerController.CreatePlayer(player.PlayerNumber, Team.Trainer.Competency);
            TeamNotify?.Invoke($"{Team.Name} team player <{player.PlayerNumber}> has been replaced");

        }
        //Фанатская поддержка
        private void FanSupport() {
            int support = random.Next(1, 10);

            Player playerToSupport = Team.Players.ElementAt(random.Next(0, Team.Players.Length));

            if (!playerToSupport.CanPlay)
                return;
            playerToSupport.Skill += support;

            TeamNotify?.Invoke($"{Team.Name} team player <{playerToSupport.PlayerNumber}> had fan support");
        }
        //Попытка забить гол
        private void TryScoreGoal() {
            Player player = Team.Players.ElementAt(random.Next(0, Team.Players.Length));

            if (!player.CanPlay)
                return;

            int chance = random.Next(1, 200);

            if (chance <= player.Skill + player.Luck)
            {
                Team.TeamScore++;
                TeamNotify?.Invoke($"{Team.Name} team player <{player.PlayerNumber}> scored goal | Team score:{Team.TeamScore}");
            }
            else
                TeamNotify?.Invoke($"{Team.Name} team player <{player.PlayerNumber}> missed");
        }

        //Может ли состав команды продолжать игру
        public bool IsCanContinue() {
            return Team.Players.Count(player => player.CanPlay) < 7;
        }
        //Обработка событий команды
        private void OnTeamEvent(string message) {
            Console.WriteLine($"Team Event : {message}");
        }
    }
}
