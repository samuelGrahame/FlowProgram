using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram.Nodes
{
    class StringNode : FlowNode
    {
        public string Value;

        public StringNode()
        {
            Name = "String";
        }
    }
}
