using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 8008;
            const string IP_ADDR = "127.0.0.1";
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IP_ADDR), PORT);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(iPEnd);
                int byteCount = 0;
                byte[] buffer = new byte[256];
                StringBuilder stringBuilder = new StringBuilder();
                do
                {
                    //
                    //  Отправка команды
                    //
                    Console.Write(" > ");
                    string msg = Console.ReadLine();
                    byte[] data = Encoding.Unicode.GetBytes(msg);
                    socket.Send(data);
                    stringBuilder.Clear();

                    //
                    //  Считывание команды
                    //
                    byteCount = socket.Receive(buffer);
                    stringBuilder.Append(Encoding.Unicode.GetString(buffer, 0, byteCount));
                    string returnedInfo = stringBuilder.ToString();
                    stringBuilder.Clear();

                    WriteConsoleTime();
                    Console.WriteLine(returnedInfo);
                  
                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                  socket.Shutdown(SocketShutdown.Both);
                  socket.Close();
            }
            Console.ReadKey();
        }

        public static void WriteConsoleTime()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(DateTime.Now.ToShortTimeString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] > ");
        }
    }
}
