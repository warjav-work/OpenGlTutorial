using GLFW;
using OpenGLTutorial.Rendering.Display;

namespace OpenGLTutorial.GameLoop
{
    abstract class Game
    {
        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeigth { get; set; }
        protected string InitialWindowTitle { get; set; }
        public Game(int initialWindowWidth, int initialWindowHeigth, string initialWindowTitle)
        {
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeigth = initialWindowHeigth;
            InitialWindowTitle = initialWindowTitle;
        }

        public void Run()
        {
            Initalize();

            DisplayManager.CreateWindow(InitialWindowWidth, InitialWindowHeigth, InitialWindowTitle);

            LoadContent();
            while (!Glfw.WindowShouldClose(DisplayManager.Window))
            {
                GameTime.DeltaTime = (float)Glfw.Time - GameTime.TotalElapsedSeconds;
                GameTime.TotalElapsedSeconds = (float)Glfw.Time;
                Update();

                Glfw.PollEvents();

                Render();
            }

            DisplayManager.CloseWindow();
        }

        protected abstract void Initalize();
        protected abstract void LoadContent();

        protected abstract void Update();
        protected abstract void Render();

    }
}
