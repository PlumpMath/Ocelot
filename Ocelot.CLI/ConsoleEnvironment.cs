using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ocelot.IO;
namespace Ocelot.CLI
{
    public class ConsoleEnvironment : IAnalyzerEnvironment
    {
        public void Message(string message_format, params object[] message)
        {
            Console.WriteLine(message_format, message);
        }

        
    }
}
