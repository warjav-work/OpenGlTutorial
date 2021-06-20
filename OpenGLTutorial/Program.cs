using OpenGLTutorial.GameLoop;

namespace OpenGLTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new TestGame(800, 600, "Test Game!");
            game.Run();
        }
    }
}