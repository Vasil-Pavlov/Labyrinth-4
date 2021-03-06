﻿namespace Labyrinth
{
    using System;

    using Labyrinth.Scoreboard;
    using Users;

    public class ScoreBoardHandler : IScoreBoardObserver
    {
        private IScoreboard scoreboard;

        public ScoreBoardHandler()
        {
            this.scoreboard = new LocalScoreBoard();
        }

        public void Update(IPlayer player)
        {
            this.scoreboard.AddToScoreBoard(player);
            this.ShowScoreboard();
        }

        public void ShowScoreboard()
        {
            var currentScoreBoard = this.scoreboard.ReturnCurrentScoreBoard();
            if (currentScoreBoard.Count == 0)
            {
                Console.WriteLine("The scoreboard is empty.");
                return;
            }

            for (int i = 0; i < currentScoreBoard.Count; ++i)
            {
                Console.WriteLine("{0}. {1} --> {2} moves", (i + 1).ToString(), currentScoreBoard[i].Name, currentScoreBoard[i].Score.ToString());
            }
        }        
    }
}
