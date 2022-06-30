using ClientServer.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer.Commands
{
    public class DateCommand : AbstractCommandBase
    {
        public DateCommand()
        {
            this.CommandName = "Date";
        }

        public override string ExecuteCommand()
        {
            return DateTime.Now.ToString();
        }
    }
}
