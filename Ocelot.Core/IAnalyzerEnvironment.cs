using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocelot.IO
{
    public interface IAnalyzerEnvironment
    {
        void Message(string message_format, params object[] message);

       
    }
}
