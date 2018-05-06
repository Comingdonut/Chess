using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Models.Board
{
    internal class BoardValuePair: List<KeyValuePair<int,int>>
    {
        internal void AddPair(int x, int y)
        {
            Add(new KeyValuePair<int, int>(x, y));
        }
    }
}
