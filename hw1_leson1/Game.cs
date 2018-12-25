using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace MyGame
{
    static class Game
    {
        #region переменные
        private static int level = 0;
        public static Random rnd = new Random();
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        private static List <Asteroid> asteroids = new List<Asteroid>();
        public static BaseObject[] _objs;
        public static BaseObject Sobjs;
        public static Shuttle shuttle;
        private static List<Bullet> bullet = new List<Bullet>();
        private static int MaxAsteroid = 14;
        private static int tmp;
        public static Energy _energy;
        private static Timer timer = new Timer() { Interval = 40};
        #endregion

        public static int Width { get; set; }
        public static int Height { get; set; }
                
        static Game()
        {
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        public static void Init(Form form)
        {
            Win32.AllocConsole();
            
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = 900;
            Height = 600;

            #region проверка на задание размера экрана в классе Game
            //проверка на задание размера экрана в классе Game
            //if (Width > 1000 | Height > 1000 | Width<0 | Height<0)
            //{
            //    ArgumentOutOfRangeException e = new ArgumentOutOfRangeException();
            //    MessageBox.Show(e.Message);
            //    //form.Width = 900;
            //    //form.Height = 600;
            //    Width = 900;
            //    Height = 600;
            //}            
            #endregion

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));           
            timer.Start();
            timer.Tick += Timer_Tick;
            Load();           
            form.KeyDown += Form_KeyDown;
            Shuttle.MessageDie += Finish;            
        }
        public static void Draw()
        {
            Buffer.Render();
            Buffer.Graphics.Clear(Color.Black);
            Sobjs.Draw();

            foreach (Asteroid obj in asteroids)
                obj?.Draw();

            foreach (BaseObject obj in _objs)
                obj.Draw();

            shuttle?.Draw();

            foreach (Bullet _b in bullet)
            _b?.Draw();

            _energy?.Draw();

            if (shuttle != null)
                Buffer.Graphics.DrawString("Energy:" + shuttle.Energy,
                SystemFonts.DefaultFont, Brushes.White, 0, 0);
           
                Buffer.Graphics.DrawString("Score:" + Bullet.countDestr,
                SystemFonts.DefaultFont, Brushes.White, 800, 0);
            Buffer.Graphics.DrawString("Records:" + Bullet._Records,
               SystemFonts.DefaultFont, Brushes.White, 800, 540);
            Buffer.Render();
        }

        public static void Load()
        {
            _objs = new BaseObject[30];

            shuttle = new Shuttle(new Point(0, 200),
                                  new Point(0, 0), 
                                  new Size(200,100 ));
            _energy = new Energy(new Point(rnd.Next(Width, 1800), rnd.Next(0, Height)), 
                                    new Point(-15, 0), 
                                    new Size(120, 70));
                       
            for (int i =0; i < MaxAsteroid+level; i++)
                asteroids.Add(new Asteroid(new Point(rnd.Next(700, 1300), rnd.Next(0, Height)), 
                                           new Point(rnd.Next(-15, -1), -1), 
                                           new Size(tmp = rnd.Next(25, 55), tmp)));
            
            for (int i = 0; i < _objs.Length; i++)
                _objs[i] = new Star(new Point(rnd.Next(0, Height), rnd.Next(0, 600)), 
                                    new Point(rnd.Next(-60, -1), -1), 
                                    new Size(tmp = rnd.Next(3, 30), tmp));
                       
            Sobjs = new StarDeath(new Point(800, 50), new Point(-1, 0), new Size(200, 200));            
        }       
        private static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            foreach (Bullet b in bullet) b.Update();
            
            for (var i = 0; i < asteroids.Count; i++)
            {
                if (asteroids[i] == null) continue;
                asteroids[i].Update();
                for (int j = 0; j < bullet.Count; j++)
                    if (asteroids[i] != null && bullet[j].Collisions(asteroids[i]))
                    {                        
                        asteroids[i].Bang();
                        System.Media.SystemSounds.Beep.Play();
                        
                        bullet.RemoveAt(j);
                        j--;
                        Bullet.DestroyAstrd(asteroids, bullet);
                        asteroids[i] = null;
                    }
                if (asteroids[i] == null || !shuttle.Collisions(asteroids[i])) continue;
                shuttle.Bang();
                shuttle.EnergyLow(new Shuttle.Log(_Collisions), rnd.Next(5, 10));
                asteroids[i] = new Asteroid(new Point(Width, rnd.Next(0, Height)),
                                            new Point(rnd.Next(-15, -1), -1),
                                            new Size(tmp = rnd.Next(25, 55), tmp));
            System.Media.SystemSounds.Asterisk.Play();
                if (shuttle.Energy <= 0) shuttle.Die();

            }
                #region UPDATE циклы пули и астеройдов
                //foreach (BaseObject obj in _objs)
                //    obj?.Update();
                //foreach (Bullet _b in bullet)
                //{
                //    _b?.Update();
                //}

                //for (int i = 0; i < asteroids.Count; i++)
                //{
                //    if (asteroids[i] == null) continue;
                //    asteroids[i].Update();
                //    for (int j = 0; j < bullet.Count; j++)
                //    {                    
                //        if (asteroids[i] != null && bullet[j].Collisions(asteroids[i]))
                //        {
                //            Bullet.DestroyAstrd();
                //            asteroids[i].Bang();
                //            System.Media.SystemSounds.Hand.Play();
                //            bullet.RemoveAt(j);
                //            asteroids.RemoveAt(i);
                //            j--;
                //            i--;

                //            continue;
                //        }
                //    }

                //    if (!shuttle.Collisions(asteroids[i])) continue;
                //    shuttle.Bang();
                //    shuttle?.EnergyLow(new Shuttle.Log(_Collisions), rnd.Next(1, 5));
                //    asteroids[i] = new Asteroid(new Point(Width, rnd.Next(0, Height)),
                //                                new Point(rnd.Next(-15, -1), -1),
                //                                new Size(tmp = rnd.Next(15, 55), tmp));

                //    //asteroids.RemoveAt(i);

                //    //asteroids.Add(new Asteroid(new Point(rnd.Next(900, Width), rnd.Next(0, Height)),
                //    //                           new Point(rnd.Next(-15, -1), -1),
                //    //                           new Size(tmp = rnd.Next(15, 55), tmp)));

                //    System.Media.SystemSounds.Asterisk.Play();

                //    if (shuttle.Energy <= 0) shuttle?.Die();
                //}
                #endregion

                if (shuttle.Collisions(_energy))
            {
                shuttle?.EnergyMedic(new Shuttle.Log(_Collisions), rnd.Next(5, 10));
                _energy = new Energy(new Point(rnd.Next(Width, 1800), rnd.Next(0, Height)), 
                                     new Point(-15, 0), 
                                     new Size(120, 70));
            }            
            _energy?.Update();
            shuttle?.Update();
            Sobjs.Update();
        }
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control) bullet.Add(new Bullet(new Point(shuttle.Rect.X + 190, shuttle.Rect.Y + 35), 
                                                 new Point(8, 0), 
                                                 new Size(78, 31)));                    
            if (e.KeyCode == Keys.Up ) shuttle.Up();
            if (e.KeyCode == Keys.Down) shuttle.Down();
            if (e.KeyCode == Keys.Right) shuttle.Right();
            if (e.KeyCode == Keys.Left) shuttle.Left();
        }
        /// <summary>
        /// запись в журнал расхода и пополнения энергии при столкновениях с астеройдами и аптечками
        /// </summary>
        /// <param name="n">указывает на количество пополняемой или расходуемой энергии</param>
        private static void _Collisions(int n)
        {
            string EnergyMin;
            if (shuttle.Collisions(_energy))
            {
                EnergyMin = $"{DateTime.Now}: Пополнение энергии на- {n}";
            }else  EnergyMin =$"{DateTime.Now}: Столкновение, потеря- {n} энергии";
            Console.WriteLine(EnergyMin);
            StreamWriter stream = new StreamWriter("file.txt", true, System.Text.Encoding.Default);
            stream.WriteLine(EnergyMin);
            stream.Close();
        }
      /// <summary>
      /// метод завершения игры при полном расходе энергии
      /// </summary>
        private static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("GAME OVER", new Font(FontFamily.GenericSansSerif,
            60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
        public static void Level()
        {            
            level++;
            Buffer.Graphics.DrawString($"LEVEL {level}", new Font(FontFamily.GenericSansSerif,
            60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
            System.Threading.Thread.Sleep(3000);            
        }
    }
}

