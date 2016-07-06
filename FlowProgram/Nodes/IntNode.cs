using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram.Nodes
{
    class IntNode : FlowNode
    {
        public int Value;
        public IntNode()
        {
            Name = "Integer";
        }
    }
}
