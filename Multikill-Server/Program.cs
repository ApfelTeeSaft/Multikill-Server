using System;
using Multikill_Server.Unity;

namespace Multikill_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MultikillServer server = new MultikillServer();
            server.StartServer();

            Console.WriteLine("Press ESC to stop the server...");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                server.PollEvents();
            }

            server.StopServer();
            Console.WriteLine("Server stopped.");
        }
    }
}