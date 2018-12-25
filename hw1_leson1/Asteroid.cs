using System.Drawing;

namespace MyGame
{
        class Asteroid : BaseObject
    {
        Image img = Properties.Resources.asterd;
        public int Power { get; set; }
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y,
            Size.Width, Size.Height);
        }

        public override void Update()
        {
            //проверка на скорость астеройда
            if (Dir.X < -15 | Dir.X > 0)
                throw new GameObjectException("Asteroid speed is high");

            Pos.X = Pos.X + Dir.X;
            if (Pos.X < -50)
            {
                Pos.X = 900;
                Pos.Y = Game.rnd.Next(0, Game.Height);
                Dir.X = -Game.rnd.Next(1, 15);
            }

        }
    }
}