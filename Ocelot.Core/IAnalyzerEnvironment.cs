using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocelot.IO
{
    public interface IAnalyzerEnvironment
    {
        void Info(string message_format, params object[] message);

        void Error(string message_format, params object[] message);

        void Error(string caller, string message_format, params object[] message);

        void Error(Exception e);

        void Error(string caller, Exception e);
        
        void Success(string message_format, params object[] message);

        void Warning(string message_format, params object[] message);

        void Status(string message_format, params object[] message);

        void Progress(string operation, int total, int complete, TimeSpan? time = null);

        void Debug(string message_format, params object[] message);
    }
}
