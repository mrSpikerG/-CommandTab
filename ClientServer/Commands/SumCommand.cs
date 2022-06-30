using ClientServer.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer.Commands
{
    public class SumCommand : AbstractCommandBase
    {
        public SumCommand()
        {
            this.CommandName = "Sum";
        }
        public override string ExecuteCommand()
        {
            if (this.CommandArguments.Count < 2) { return "Incorrect arguments"; }

            double sum = 0;

            try
            {
                foreach (string num in CommandArguments)
                {
                    sum += Convert.ToDouble(num);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return sum.ToString();
        }
    }
}
