using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram
{
    class NodeContainer : Entity
    {
        public List<FlowNode> Nodes;

        public NodeContainer()
        {
            Nodes = new List<FlowNode>();
        }
    }
}
