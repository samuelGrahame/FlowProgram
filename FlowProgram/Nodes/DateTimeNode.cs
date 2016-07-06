using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram.Nodes
{
    class DateTimeNode : FlowNode
    {
        public DateTime? Value;
        public DateTimeNode()
        {
            Name = "DateTime";
        }
    }
}
