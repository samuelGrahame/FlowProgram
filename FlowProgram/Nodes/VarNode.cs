using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram.Nodes
{
    public class VarNode : VisibleEntity
    {
        public VarNode()
        {
            ConnectionRules = ConnectionRules.Both;
        }
    }
}

