using Chess_Project.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Models.Player
{
    internal class Player
    {
        internal string Name { get; set; }
        internal Color Color { get; set; }
        // TODO: Time took
        // TODO: History
        internal Player() { }
        internal Player(string name, Color color)
        {
            Name = name;
            Color = color;
        }
    }
}
