using System.Drawing;
using System;

namespace MyGame
{
    class Shuttle: BaseObject
    {
        private int _energy = 100;
        private int MAX_energy = 100;
        public int Energy => _energy;
        /// <summary>
        /// метод пополнения энергии
        /// </summary>
        /// <param name="a">делегат для метода ведения журнала</param>
        /// <param name="n">количество поплняемой энергии</param>
        public void EnergyMedic(Log a, int n)
        {
            _energy += n;
            if (_energy > MAX_energy)
            {
                n = n - (_energy - 100);
                _energy = 100;
            }            
            a(n);
        }
        /// <summary>
        /// метод получения урона
        /// </summary>
        /// <param name="a">делегат</param>
        /// <param name="n">количество урона</param>
        public void EnergyLow(Log a, int n )
        {
            _energy -= n;
            a(n);
        }

        public Shuttle(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        Image imgBang = Properties.Resources.bang;
        Image img = Properties.Resources.starwars1;
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y,
            Size.Width, Size.Height);
        }
        public override void Bang()
        {
            Game.Buffer.Graphics.DrawImage(imgBang, Pos.X , Pos.Y, Size.Width,
            Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;          
        }
        public void Up()
        {
            if (Pos.Y > 0)
                Pos.Y = Pos.Y - 10;
        }
        public void Down()
        {
            if(Pos.Y < Game.Height-140)
            Pos.Y = Pos.Y + 10;
        }
        public void Right()
        {
            if (Pos.X < Game.Width-240)
            Pos.X = Pos.X + 10;
        }
        public void Left()
        {
            if (Pos.X > 0)
            Pos.X = Pos.X - 10;
        }


        public void Die()
        {
            MessageDie?.Invoke();
        }

        public static event Message MessageDie;

        public delegate void Log(int a);
    }
}
