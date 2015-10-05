﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labyrinth.Renderer
{
    abstract class Decorator : IRenderer
    {
        protected Decorator(IRenderer renderer)
        {
            this.Renderer = renderer;
        }

        protected IRenderer Renderer { get; set; }

        public void ShowLabyrinth(LabyrinthMatrix labyrinth, Users.IPlayer player)
        {
            this.Renderer.ShowLabyrinth(labyrinth, player);
        }



        public string AddInput()
        {
            return this.Renderer.AddInput();
        }

        public void ShowMessage(string message)
        {
            this.Renderer.ShowMessage(message);
        }

    }
}
