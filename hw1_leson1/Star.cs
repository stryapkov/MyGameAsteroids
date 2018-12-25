using System;
using System.Drawing;

namespace MyGame
{
    class Star: BaseObject
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        Image img = Properties.Resources.stars;

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y,
            Size.Width, Size.Height);            
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < -20) Pos.X = Game.Width + Size.Width;
        }       
    }
    class StarDeath : BaseObject
    {       
        public StarDeath(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
              
        Image Image_SDeath = Properties.Resources.star_death;
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Image_SDeath, Pos.X, Pos.Y,
            Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < -100) //dir.X = -dir.X;
            {
                Pos.X = 900;
                Pos.Y = Game.rnd.Next(0, Game.Height);
                Dir.X = -Game.rnd.Next(1, 2);
            }
        }
    }
}
