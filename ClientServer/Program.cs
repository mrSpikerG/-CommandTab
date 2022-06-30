using ClientServer.Commands.Base;
using ClientServer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 8008;
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //
            //  регистрируем комманды
            //  
            List<AbstractCommandBase> commandList = new List<AbstractCommandBase>() {
                new SumCommand(),
                new DateCommand(),
                new SayCommand() 
            };

            try
            {
                socket.Bind(iPEnd);
                socket.Listen(10);


                //берем клиента
                Socket clientSocket = socket.Accept();
                int byteCount = 0;
                byte[] buffer = new byte[256];
                StringBuilder stringBuilder = new StringBuilder();
                //do
                //{

                do { 
                    Task.Factory.StartNew(() =>
                    {
                        byteCount = clientSocket.Receive(buffer);
                        stringBuilder.Append(Encoding.Unicode.GetString(buffer, 0, byteCount));
                        string command = stringBuilder.ToString();

                        if (command != String.Empty)
                        {
                            Console.WriteLine($"[ {DateTime.Now} ] Client logger:\t{command}");
                            byte[] subbuffer = null;
                            string[] SplittedCommand = command.Split(' ');
                            foreach (var cmd in commandList)
                            {
                                if (SplittedCommand[0].ToLower().Equals(cmd.CommandName.ToLower()))
                                {
                                    cmd.setArguments(command);
                                    subbuffer = Encoding.Unicode.GetBytes(cmd.ExecuteCommand());
                                    clientSocket.Send(subbuffer);
                                }
                            }
                            if (subbuffer == null)
                            {
                                clientSocket.Send(Encoding.Unicode.GetBytes("Invalid command"));
                            }
                            stringBuilder.Clear();
                        }
                    });
                } while (true);
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
        
    }

}
