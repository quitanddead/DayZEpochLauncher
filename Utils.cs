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
            string Arguments, serverIP, serverPort;
            GetServer(out serverIP, out serverPort);

            if (windowedMode())
            {
                if (GetPassword() == string.Empty)
                {
                    Arguments = "-window -skipIntro -mod=@DayZ_Epoch -noSplash -noFilePatching -world=empty -connect=" + serverIP + " -port=" + serverPort + " \"-mod=" + Arma2Location() + ";expansion;expansion\\beta;expansion\\beta\\expansion;@DayZ_Epoch\"";
                }
                else
                {
                    Arguments = "-window -skipIntro -mod=@DayZ_Epoch -noSplash -noFilePatching -world=empty -connect=" + serverIP + " -port=" + serverPort + " -password=" + GetPassword() + " \"-mod=" + Arma2Location() + ";expansion;expansion\\beta;expansion\\beta\\expansion;@DayZ_Epoch\"";
                }
            }
            else
            {
                if (GetPassword() == string.Empty)
                {
                    Arguments = "-skipIntro -mod=@DayZ_Epoch -noSplash -noFilePatching -world=empty -connect=" + serverIP + " -port=" + serverPort + " \"-mod=" + Arma2Location() + ";expansion;expansion\\beta;expansion\\beta\\expansion;@DayZ_Epoch\"";
                }
                else
                {
                    Arguments = "-skipIntro -mod=@DayZ_Epoch -noSplash -noFilePatching -world=empty -connect=" + serverIP + " -port=" + serverPort + " -password=" + GetPassword() + " \"-mod=" + Arma2Location() + ";expansion;expansion\\beta;expansion\\beta\\expansion;@DayZ_Epoch\"";
                }
            }

            return Arguments;
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
            return;
        }

        public static string GetPassword()
        {
            string[] Lines = System.IO.File.ReadAllLines(Environment.CurrentDirectory + @"\Client.ini");
            return Lines[2].Replace("Password=", string.Empty);
        }
    }
}
