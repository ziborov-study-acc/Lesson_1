using Lesson_1.Helpers;
using Lesson_1.Interfaces;
using Lesson_1.Models;
using Lesson_1.Models.DataDetails;
using System;
using System.Threading;
using Timers = System.Timers;

namespace Lesson_1.Controllers {

    public class GameController : IGameController {

        //объект игры
        public Game Game { get; private set; }

        //Команды
        public TeamController HomeManager { get; private set; } = new TeamController();

        public TeamController GuestManager { get; private set; } = new TeamController();

        private Random random = new Random();

        //таймер
        private Timers.Timer timer;

        //
        private int matchTime;
        private int timeGone;
        private int additionalTime;

        //Событие окончания игры
        private delegate void GameOverHandler(object sender,GameEventArgs e);

        private event GameOverHandler GameOverEvent;

        //Игровые события
        private delegate void GameEventHandler(object sender, GameEventArgs e);

        private event GameEventHandler GameEvent;

        //Инициализация игры
        public void Create(BaseData data) {
            if (!(data is GameControllerData))
                throw new ArgumentException();

            HomeManager.Create(new TeamControllerData((data as GameControllerData).HomeTeamName));
            GuestManager.Create(new TeamControllerData((data as GameControllerData).GuestTeamName));

            GameOverEvent += OnGameOverEvent;
            GameEvent += OnGameEvent;
            Game = new Game(HomeManager.Team, GuestManager.Team);
            matchTime = 90;
            timeGone = 0;
            additionalTime = 2;
            timer = new Timers.Timer(500);
            timer.Elapsed += TimerCallback;
            timer.Start();
        }

        //Старт игры
        public void Start() {
            do
            { 
                double eventChance = random.NextDouble() + random.Next(100);

                //вызов игрового или командного события
                if (eventChance > 1)
                {
                    TeamController tempTeam;
                    tempTeam = random.Next(2) == 0 ? HomeManager : GuestManager;
                    tempTeam.GetRandAction();
                    CheckTeam(tempTeam);
                }
                else
                {
                    if (eventChance > 0.5)
                        BlackoutEvent();
                    else if (eventChance < 0.5)
                        BadWeatherEvent();
                }

                Thread.Sleep(random.Next(300, 500));
            } while (!Game.IsGameOver);
        }

        //Обработка игровых событий
        private void OnGameEvent(object sender, GameEventArgs e) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Game Event : {e.Message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Обработка событий конца игры
        private void OnGameOverEvent(object sender, GameEventArgs e) {
            Game.IsGameOver = true;
            timer.Stop();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Game Over Event : Match <{HomeManager.Team.Name} - {GuestManager.Team.Name}> over. {e.Message}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Проверка команды
        private void CheckTeam(TeamController teamController) {
            if (teamController.IsCanContinue())
                GameOverEvent?.Invoke(this,new GameEventArgs($"{teamController.Team.Name} team - 5 players are injured"));
        }
        //Плохая погода, матч закончен
        private void BadWeatherEvent() {
            GameOverEvent?.Invoke(this, new GameEventArgs($"Bad weather started the match has been cancelled."));
        }
        //Отсутствие электричества, матч закончен
        private void BlackoutEvent() {
            GameOverEvent?.Invoke(this, new GameEventArgs($"Stadium has no electricity the match has been cancelled."));
        }
        //дополнительное время
        private void AdditionalTimeEvent() {
            matchTime += 15;
            GameEvent?.Invoke(this, new GameEventArgs($"Match was extended on 15 minutes"));
        }

        private void TimerCallback(object sender, Timers.ElapsedEventArgs e) {
            ++timeGone;
            if (timeGone == matchTime)
            {
                string result;
                
                if (HomeManager.Team.TeamScore == GuestManager.Team.TeamScore)
                {
                    //Проверка на оставшееся дополнительное время
                    if (additionalTime != 0)
                    {
                        --additionalTime;
                        AdditionalTimeEvent();
                        return;
                    }
                    else
                    {
                        result = "Draw.";
                    }
                }
                else
                {
                    if (HomeManager.Team.TeamScore > GuestManager.Team.TeamScore)
                        result = "Home Team wins.";
                    else
                        result = "Guest Team wins";
                }
                GameOverEvent?.Invoke(this, new GameEventArgs($"Time left. {result}. Score <{HomeManager.Team.TeamScore} - {GuestManager.Team.TeamScore}>"));
                return;
            }
        }
    }
}