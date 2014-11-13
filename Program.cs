namespace Client
{
    using System;
    using System.Diagnostics;

    class Program
    {
        static void Main(string[] args)
        {
            Log("Initializing...");
            Console.Title = "DayZEpoch Launcher";
            Log("ArmA2 location: " + Utils.Arma2Location());
            Log("Creating ProcessInfo...");
            ProcessStartInfo Game = new ProcessStartInfo();
            Game.UseShellExecute = true;
            Game.Verb = "runas";
            Game.WorkingDirectory = Utils.Arma2Location();
            Game.FileName = Utils.Arma2Location() + "\\Expansion\\beta\\" + "arma2oa.exe";
            Log("ArmA2 OA location: " + Game.FileName);
            Log("Getting ini content...");
            string serverIP, serverPort;
            Utils.GetServer(out serverIP, out serverPort);
            Log("Got " + serverIP + ":" + serverPort + " from file");
            Game.Arguments = Utils.GetArguments();
            Log("Starting the game...");
            Log("Connecting to [" + serverIP + ":" + serverPort + "]");
            Log("Closing...");
            Process.Start(Game);
            Environment.Exit(0x0);
        }

        private static void Log(string Input)
        {
            Input = DateTime.Now.ToString("[dd-MM-yyyy @ HH:mm:ss]") + " - " + Input;
            Console.WriteLine(Input);
            return;
        }
    }
}
