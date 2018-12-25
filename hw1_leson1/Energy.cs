using System.Drawing;

namespace MyGame
{
    class Energy: BaseObject
    {
        Image img = Properties.Resources.energy;
        public int Power { get; set; }
        public Energy(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y,
            Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < -50)
            {
                Pos.X = Game.rnd.Next(900, 1800);
                Pos.Y = Game.rnd.Next(0, Game.Height);
                Dir.X = -Game.rnd.Next(1, 10);
            }
        }
    }
}
