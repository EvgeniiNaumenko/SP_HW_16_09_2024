using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SP_HW_16_09_2024
{
    class Program
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);

        [DllImport("user32.dll")]
        public static extern bool MessageBeep(uint uType);

        static void Main(string[] args)
        {
            //ОСНОВНОЕ
            string processPath = "notepad.exe";


            Process process = new Process();
            process.StartInfo.FileName = processPath;
            process.StartInfo.Arguments = "";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.Start();
            process.WaitForExit();

            //ДОП 1
            int[] frequencies = { 440, 330, 440, 330, 440, 415, 415, };
            int duration = 300;

            foreach (int frequency in frequencies)
            {
                Beep(frequency, duration);
            }
            Thread.Sleep(2000);

            foreach (int frequency in frequencies)
            {
                Beep(frequency, duration);
            }
            Thread.Sleep(2000);

            MessageBeep(0x00000040);



            string processPath2 = "notepad.exe";
            using (Process process2 = new Process())
            {
                process2.StartInfo.FileName = processPath;

                process2.Start();
                Console.WriteLine("Процесс запущен. Выберите действие:");
                Console.WriteLine("1. Ожидать завершения процесса");
                Console.WriteLine("2. Принудительно завершить процесс");


                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    // Ожидаем завершения процесса
                    process.WaitForExit();
                    int exitCode = process.ExitCode;
                    Console.WriteLine($"Процесс завершился с кодом: {exitCode}");
                }
                else if (choice == "2")
                {
                    // Принудительно завершаем процесс
                    process.Close();
                    Console.WriteLine("Процесс был принудительно завершен.");
                }
                else
                {
                    Console.WriteLine("Неверный выбор. Процесс продолжается в фоновом режиме.");
                }
            }
        }
    }
}
