using System.Drawing;
using System.Collections.Generic;

namespace MyGame
{
    class Bullet: BaseObject
    {
       Image img =Properties.Resources.Laser_;
        private static int _countDestr = 0;
        private static int Records =0;
        /// <summary>
        /// свойство счетчика очков
        /// </summary>
        public static int countDestr => _countDestr;
        public static int _Records => Records+_countDestr;
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y, Size.Width,
Size.Height);
        }
        /// <summary>
        /// загрузка уровня и счетчик очков при уничтожении астеройда 
        /// </summary>
        /// <param name="A">коллекция астеройдов</param>
        /// <param name="b">коллекция пули</param>
        public static void DestroyAstrd(List<Asteroid> A, List<Bullet> b)
        {
            _countDestr++; 
            if(_countDestr == A.Count)
            {               
                Game.Level();
                Game.Load(); 
                b.Clear();       
            }
        }

        public override void Update()
        {
            Pos.X = Pos.X + 10;            
        }
    }
}
