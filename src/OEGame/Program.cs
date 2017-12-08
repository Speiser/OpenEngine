using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenEngine;
using OpenEngine.Engine;

namespace OEGame
{
    class GBehavior : GameBehaviour { }
    class Program
    {
        static void Main(string[] args)
        {
            var w = new OpenEngineWindow(1280, 720, "title", new GBehavior());
            w.Start();
        }
    }
}
