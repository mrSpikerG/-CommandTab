using System;
using System.Collections.Generic;

namespace ClientServer.Commands.Base
{
    public abstract class AbstractCommandBase
    {

        //
        //  Command model
        //
        public string CommandName { get; protected set; }
        public List<string> CommandArguments { get; set; }


        public void setArguments(string input)
        {
            List<string> arguments = new List<string>();
            string[] splitedInput = input.Split(' ');
            foreach (string item in splitedInput)
            {
                arguments.Add(item);
            }
            // удаляем саму команду
            arguments.RemoveAt(0);
            CommandArguments = arguments;
        }
        

        public abstract string ExecuteCommand();
    }
}
