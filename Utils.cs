namespace Client
{
    using System;
    using Microsoft.Win32;

    public class Utils
    {
        public static string Arma2Location()
        {            
            RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\Bohemia Interactive Studio\ArmA 2 OA");

            if (key != null) return key.GetValue("MAIN").ToString();
            else
            {
                key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Bohemia Interactive Studio\ArmA 2 OA");
                if (key != null) return key.GetValue("MAIN").ToString();
                else return string.Empty;
            }
        }

        public static string GetArguments()
        {
            string serverIP, serverPort;
            GetServer(out serverIP, out serverPort);
            List<string> Parameters = new List<string>();
            if (windowedMode()) Parameters.Add("-window");
            Parameters.Add("-skipIntro");
            Parameters.Add("-mod=@DayZ_Epoch");
            Parameters.Add("-noSplash");
            Parameters.Add("-noFilePatching");
            Parameters.Add("-world=empty");
            Parameters.Add("-connect=" + serverIP);
            Parameters.Add("-port=" + serverPort);
            if (GetPassword() != string.Empty) Parameters.Add("-password=" + GetPassword());
            Parameters.Add("\"mod=" + Arma2Location());
            return string.Join(" ", Parameters.ToArray());
        }

        public static bool windowedMode()
        {
            string[] Lines = System.IO.File.ReadAllLines(Environment.CurrentDirectory + @"\Client.ini");
            if (Lines[0].Replace("Windowed=", string.Empty) == "0") return false;
            else return true;
        }

        public static void GetServer(out string serverIP, out string serverPort)
        {
            string[] Lines = System.IO.File.ReadAllLines(Environment.CurrentDirectory + @"\Client.ini");
            string serverInfo = Lines[1].Replace("Server=", string.Empty);
            serverIP = serverInfo.Split(':')[0];
            serverPort = serverInfo.Split(':')[1];
        }

        public static string GetPassword()
        {
            string[] Lines = System.IO.File.ReadAllLines(Environment.CurrentDirectory + @"\Client.ini");
            return Lines[2].Replace("Password=", string.Empty);
        }
    }
}
