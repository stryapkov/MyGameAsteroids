﻿using System;
using System.Runtime.InteropServices;


namespace MyGame
{ 
    class Win32
{
    /// <summary>
    /// Allocates a new console for current process.
    /// </summary>
    [DllImport("kernel32.dll")]
    public static extern Boolean AllocConsole();

    /// <summary>
    /// Frees the console.
    /// </summary>
    [DllImport("kernel32.dll")]
    public static extern Boolean FreeConsole();
}

}