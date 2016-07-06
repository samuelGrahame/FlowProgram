using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram.Nodes
{
    class ByteNode : FlowNode
    {
        public byte Value;
        public ByteNode()
        {
            Name = "Byte";
        }
    }
}
