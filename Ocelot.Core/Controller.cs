using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Cci;
using Microsoft.Cci.Analysis;
using Microsoft.Cci.MutableContracts;

using Ocelot.IO;
namespace Ocelot
{
    public class Controller
    {
        #region Constructors
        public Controller(IAnalyzerEnvironment environment)
        {
            this.Environment = environment;
        }
        #endregion

        #region Properties
        public IModule PrimaryModule { get; protected set; } = null;
        public IAssembly PrimaryAssembly { get; protected set; } = null;
        public IMetadataReaderHost MetadataReaderHost { get; } = new FullyResolvedPathHost();      
        protected IAnalyzerEnvironment Environment { get; set; }
        #endregion

        #region Methods
        public bool Load(string location)
        {
            IModule module = MetadataReaderHost.LoadUnitFrom(location) as IModule;
            if (module == null || module is Dummy)
            {
                return false;
            }
            else
            {
                PrimaryModule = module;
                PrimaryAssembly = module as IAssembly;
                Environment.Message("Loaded primary assembly {0} with {1} assembly references.", PrimaryAssembly.Name.Value, PrimaryAssembly.AssemblyReferences.Count());
                return true;
            }
        }

        public bool AnalyzeMethods()
        {
            foreach (INamedTypeDefinition t in PrimaryModule.GetAllTypes())
            {
                if (t.IsClass && t.Methods.Count() > 0 && !t.Name.Value.StartsWith("<"))
                {
                    Environment.Message("Class {0} has {1} members, {2} methods.", t.Name.Value, t.Members.Count(), t.Methods.Count());
                    List<IMethodDefinition> methods = t.Methods.ToList();
                    foreach(IMethodDefinition m in methods)
                    {
                        ControlAndDataFlowGraph<BasicBlock<Instruction>, Instruction> cdfg = ControlAndDataFlowGraph<BasicBlock<Instruction>, Instruction>.GetControlAndDataFlowGraphFor(MetadataReaderHost, m.Body);
                        Environment.Message("  Method {0} has visibility {3}, {1} parameters, {2} local variables in body, {4} total basic blocks or nodes and {5} successor edges in CFG.\n", m.Name.Value, m.ParameterCount, 
                            m.Body.LocalVariables.Count(), m.Visibility.ToString(), cdfg.AllBlocks.Count,
                            cdfg.SuccessorEdges.Count);
                        
                    }
                }
            }
            return true;
        }
        #endregion

    }
}
