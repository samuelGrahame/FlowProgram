using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram.Nodes
{
    class DecimalNode : FlowNode
    {
        public decimal Value;
        public DecimalNode()
        {
            Name = "Decimal";
        }
    }
}
