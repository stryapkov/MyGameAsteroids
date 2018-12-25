using System.Drawing;


interface ICollisions    
{
    bool Collisions(ICollisions obj);
    Rectangle Rect { get; }
}
