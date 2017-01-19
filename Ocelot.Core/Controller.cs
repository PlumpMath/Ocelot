using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Cci;
using Microsoft.Cci.MutableContracts;

namespace Ocelot
{
    public class Controller
    {
        #region Properties
        public IAssembly PrimaryAssembly { get; protected set; } = null;
        #endregion

        #region Methods
        public bool Load(string location)
        {
            IAssembly assembly = LoadAssembly(location);
            if (assembly == null || assembly is Dummy)
            {
                return false;
            }
            else
            {
                PrimaryAssembly = assembly;
                return true;
            }
        }

        protected IAssembly LoadAssembly(string location)
        {
            FullyResolvedPathHost host = new FullyResolvedPathHost();
            IAssembly assembly = host.LoadUnitFrom(location) as IAssembly;
            return assembly;
        }
        #endregion

    }
}
