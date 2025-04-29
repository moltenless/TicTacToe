using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    partial class MainForm : Form
    {
        Game game = new Game(3, Game.TypeGame.Friend, Game.Symbol.X);

        public MainForm()
        {
            InitializeComponent();
            game.StateChange += Game_StateChange;
        }

        private void Game_StateChange(Game.State state, Game.Symbol symbol)
        {
            Refresh();
            if (state == Game.State.Drow) MessageBox.Show("Draw!");
            else MessageBox.Show($"{symbol} won!");
            StartGame();
        }

        private void button0_Click(object sender, EventArgs e)
        {
            int position = (sender as Button).TabIndex;
            game.Mark(position);
            Refresh();
        }

        public void StartGame()
        {
            game.StartGame();
            Refresh();
            for (int i = 0; i < game.Size * game.Size; i++)
                button(i).Enabled = true;
        }

        private new void Refresh()
        {
            for (int i = 0; i < game.Size * game.Size; i++)
            {
                game.GetPoint(i, out int x, out int y);
                button(i).Text = game.Map[x, y];
                if (game.Map[x, y] != "" && game.Map[x, y] != null) button(i).Enabled = false;
            }
        }

        public Button button(int position)
        {
            switch (position)
            {
                case 0: return button0;
                case 1: return button1;
                case 2: return button2;
                case 3: return button3;
                case 4: return button4;
                case 5: return button5;
                case 6: return button6;
                case 7: return button7;
                default: return button8;
            }
        }

        private void крестикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Text == "X") game.Symb = Game.Symbol.X;
            else if ((sender as ToolStripMenuItem).Text == "O") game.Symb = Game.Symbol.O;
            else if ((sender as ToolStripMenuItem).Text == "Play with computer") game.Type = Game.TypeGame.Computer;
            else if ((sender as ToolStripMenuItem).Text == "Play with friend") game.Type = Game.TypeGame.Friend;
            StartGame();
        }
    }
}
