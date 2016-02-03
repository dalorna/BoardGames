using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class CellState
    {
        public enum State
        {
            EMPTY = 0,
            WHITE_PIECE = 1,
            BLACK_PIECE = 2,
            WHITE_PIECE_PROMOTED = 3,
            BLACK_PIECE_PROMOTED = 4
        }
    }
}
