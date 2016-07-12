using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram
{
    class NodeConnection : Entity
    {
        public string Name { get; set; }

        public VisibleEntity Input = null;
        public VisibleEntity Output = null;
    }
}
