using GLFW;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static OpenGLTutorial.OpenGL.GL;

namespace OpenGLTutorial.Rendering.Display
{
    static class DisplayManager
    {
        public static Window Window { get; set; }
        public static Vector2 WindowSize { get; set; }
        public static void CreateWindow(int width, int heigth, string title)
        {
            // Ancho y alto de la Ventana inicial
            WindowSize = new Vector2(width, heigth);
            Glfw.Init();

            // opengl 3.3 core profile
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);

            Glfw.WindowHint(Hint.Focused, true);
            // No resizable
            Glfw.WindowHint(Hint.Resizable, false);

            // Crear la ventana
            Window = Glfw.CreateWindow(width, heigth, title, Monitor.None, Window.None);

            if(Window == Window.None)
            {
                // Algo ha fallado
                return;
            }

            Rectangle screen = Glfw.PrimaryMonitor.WorkArea;
            int x = (screen.Width - width) / 2;
            int y = (screen.Height - heigth) / 2;

            Glfw.SetWindowPosition(Window, x, y);

            Glfw.MakeContextCurrent(Window);
            Import(Glfw.GetProcAddress);

            glViewport(0, 0, width, heigth);
            Glfw.SwapInterval(0); // VSync is off, 1 VSync on.



        }

        public static void CloseWindow()
        {
            Glfw.Terminate();
        }
    }
}
