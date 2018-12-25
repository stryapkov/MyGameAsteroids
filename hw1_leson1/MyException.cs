using System;
using System.Drawing;

namespace MyGame
{
    class GameObjectException: Exception
    {     
            public GameObjectException() { }

        public GameObjectException(string message) : base(message) { }
     
    }
}
