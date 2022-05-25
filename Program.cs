using System;

namespace TrenchesRTS
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new TrenchesRTS();
            game.Run();
        }
    }
}
