using System.Windows.Forms;

namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            {
                form.Width = 900/*Screen.PrimaryScreen.Bounds.Width*/;
                form.Height = 600/* Screen.PrimaryScreen.Bounds.Height*/;
            };    
            Game.Init(form);
            form.Show();            
            Game.Draw();
            Game.Level();
            Application.Run(form);
        }
    }
}