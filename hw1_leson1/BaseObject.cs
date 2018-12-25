using System.Drawing;


namespace MyGame
{

    abstract class BaseObject : ICollisions
    {
      //  static public string Patch = @"C:\Users\ДНС\Desktop\Программирование\С#\c# projects\MyGameAsteroids\";
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public abstract void Draw();
        public abstract void Update();
        /// <summary>
        /// метод взрыва при столкновении с астеройдом
        /// </summary>
        public virtual void Bang()
        {
            Game.Buffer.Graphics.DrawImage(imgBang, Pos.X - 40, Pos.Y, Size.Width = 110,
            Size.Height = 60);
        }
        public bool Collisions(ICollisions o) => o.Rect.IntersectsWith(Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);

        public delegate void Message();
        
        Image imgBang = Properties.Resources.bang;
    }
}
