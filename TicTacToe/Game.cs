using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Game
    {
        public delegate void MyDelegate(State state, Symbol symbol);
        public event MyDelegate StateChange;

        public enum TypeGame
        {
            Computer,
            Friend
        }
        public enum Symbol
        {
            X,
            O
        }
        public enum State
        {
            Victory,
            Drow
        }

        public int Size { get; set; }
        public TypeGame Type { get; set; }
        public Symbol Symb { get; set; }
        public string[,] Map;
        private static Random random = new Random();

        public Game(int size, TypeGame type, Symbol symbol)
        {
            Size = size;
            Type = type;
            Symb = symbol;
            Map = new string[Size, Size];
        }

        public void StartGame()
        {
            for (int cols = 0; cols < Size; cols++)
                for (int rows = 0; rows < Size; rows++)
                {
                    Map[rows, cols] = "";
                }
        }

        public void Mark(int position)
        {
            void FriendMark(int positionFriend)
            {
                GetPoint(positionFriend, out int x, out int y);
                Map[x, y] = Symb.ToString();
                Check();
                Symb = RefreshSymbol(Symb);
                if (Type == TypeGame.Computer) ComputerMark();
            }
            void ComputerMark()
            {
                if (Type == TypeGame.Computer)
                {
                    while (true)
                    {
                        int rnd = random.Next(Size * Size);
                        GetPoint(rnd, out int rx, out int ry);
                        if (Map[rx, ry] == null || Map[rx, ry] == "")
                        {
                            GetPoint(rnd, out int x, out int y);
                            Map[x, y] = Symb.ToString();
                            Check();
                            Symb = RefreshSymbol(Symb);
                            return;
                        }
                    }
                }
            }
            void Check()
            {
                try
                {
                    string symbol = Symb.ToString();
                    if ((Map[0, 0] == symbol && Map[1, 0] == symbol && Map[2, 0] == symbol) //check at victory
                      || (Map[0, 1] == symbol && Map[1, 1] == symbol && Map[2, 1] == symbol)
                      || (Map[0, 2] == symbol && Map[1, 2] == symbol && Map[2, 2] == symbol)
                      || (Map[0, 0] == symbol && Map[0, 1] == symbol && Map[0, 2] == symbol)
                      || (Map[1, 0] == symbol && Map[1, 1] == symbol && Map[1, 2] == symbol)
                      || (Map[2, 0] == symbol && Map[2, 1] == symbol && Map[2, 2] == symbol)
                      || (Map[0, 0] == symbol && Map[1, 1] == symbol && Map[2, 2] == symbol)
                      || (Map[2, 0] == symbol && Map[1, 1] == symbol && Map[0, 2] == symbol))
                        StateChange(State.Victory, Symb);
                    else
                    {
                        bool isBusy = true;
                        for (int cols = 0; cols < Size; cols++)
                            for (int rows = 0; rows < Size; rows++)
                                if (Map[rows, cols] == null || Map[rows, cols] == "") isBusy = false;
                        if (isBusy) StateChange(State.Drow, Symb);
                    }// check at Drow
                }
                catch
                {
                    StartGame();
                }
            }

            FriendMark(position);
        }

        private Symbol RefreshSymbol(Symbol symbol)
        {
            if (symbol == Symbol.X) return Symbol.O;
            else return Symbol.X;
        }

        private State CheckState()
        {
            return State.Drow;
        }

        public int GetPosition(int x, int y)
        {
            return y * Size + x;
        }

        public void GetPoint(int position, out int x, out int y)
        {
            x = position % Size;
            y = position / Size;
        }
    }
}
