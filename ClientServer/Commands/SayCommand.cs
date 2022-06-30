using ClientServer.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer.Commands
{
    public class SayCommand : AbstractCommandBase
    {
        public SayCommand()
        {
            this.CommandName = "Say";
        }
        public override string ExecuteCommand()
        {
            return string.Join(" ", CommandArguments);
        }
    }
}
