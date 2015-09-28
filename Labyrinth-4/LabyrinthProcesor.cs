﻿namespace Labyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Labyrinth.Renderer;
    using Labyrinth.Users;
    using Labyrinth.Commands;

    public class LabyrinthProcesor : Subject
    {
        public const int MaximalHorizontalPosition = 6;
        public const int MinimalHorizontalPosition = 0;
        public const int MaximalVerticalPosition = 6;
        public const int MinimalVerticalPosition = 0;

        private LabyrinthMatrix matrix;
        private IRenderer renderer;
        private IPlayerCloneable player;
        private Messenger messenger;
        private IScoreBoardObserver scoreBoardHandler;

        public LabyrinthProcesor(IRenderer renderer, IPlayerCloneable player, IScoreBoardObserver scoreBoardHandler)
        {
            this.Attach(scoreBoardHandler);
            this.messenger = new Messenger();
            this.scoreBoardHandler = scoreBoardHandler;
            this.renderer = renderer;
            this.player = player;
            this.Restart();
        }

        public LabyrinthMatrix Matrix
        {
            get { return this.matrix; }
            set { this.matrix = value; }
        }

        public void ShowInputMessage()
        {
            renderer.ShowMessage(Messenger.InputMessage);
        }

        public void HandleInput(string input)
        {
            string lowerInput = input.ToLower();
            Command command;

            if(lowerInput.Length == 1){
                command = new PlayerCommand(this.player, this.matrix.Matrix, lowerInput);
            }
            else
            {
                command = new GameCommand(this, this.scoreBoardHandler, this.renderer, lowerInput);
            }

            command.Execute();

            this.IsFinished();
        }

        private void IsFinished()
        {
            if (this.player.PositionCol == MinimalHorizontalPosition ||
                this.player.PositionCol == MaximalHorizontalPosition ||
                this.player.PositionRow == MinimalVerticalPosition ||
                this.player.PositionRow == MaximalVerticalPosition)
            {
                renderer.ShowMessage(this.messenger.WriteFinalMessage(this.player.Score));
                var clone = (IPlayerCloneable)this.player.Clone();
                this.Notify(clone);
                this.Restart();
            }
        }

        public void Restart()
        {
            this.renderer.ShowMessage(Messenger.WelcomeMessage);
            this.matrix = new LabyrinthMatrix();
            this.player.Score = 0;
            this.player.PositionCol = 3;
            this.player.PositionRow = 3;
        }

        // private bool MovePlayer(int playerHorizontalPosition, int playerVerticalPosition,int directionBoundary)
        // {
        //    if (!(playerHorizontalPosition == directionBoundary) &&
        //        this.matrix.Matrix[playerHorizontalPosition][playerVerticalPosition + 1] == '-')
        //    {
        //        this.matrix.MyPostionVertical++;
        //        this.moveCount++;
        //        return true;
        //    }
        //
        //    return false;
        //
        // }

        public override void Notify(IPlayerCloneable player)
        {
            foreach (IScoreBoardObserver observer in this.observers)
            {
                observer.Update(player);
            }
        }
    }
}
