using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ocelot;
using Ocelot.Args;

namespace Ocelot.CLI
{
    class Program
    {
        static int Main(string[] args)
        {
            Options options = new Options(args, "oc");
            if (!options.ParseSucceded)
            {
                Console.WriteLine(options.Usage);
                return 1;
            }
            else
            {
                Controller c = new Controller(new ConsoleEnvironment());
                if (!c.Load(options.File))
                {
                    Console.WriteLine("Could not load assembly from file {0}.", options.File);
                    return 1;
                }
                c.AnalyzeMethods();
                return 0;
            }
        }
    }
}
