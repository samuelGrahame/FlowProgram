using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram.Nodes
{
    /// <summary>
    /// output a value only
    /// </summary>
    class LiteralNode : VisibleEntity
    {
        private NodeConnection Output = null;
        public override void AddConnection(NodeConnection connection)
        {
            if(Output == null)
            {
                if (connection == null)
                    return;
                Output = connection;

                if(!Connections.Contains(connection))
                    base.Connections.Add(connection);
            }else
            {
                if (Connections.Contains(Output))
                    Connections.Remove(Output);  
                              
                Output = connection;

                if (!Connections.Contains(connection))
                    base.Connections.Add(connection);
            }            
        }
    }
}
