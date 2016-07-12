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

                if(!connections.Contains(connection))
                    base.connections.Add(connection);
            }else
            {
                if (connections.Contains(Output))                
                    connections.Remove(Output);  
                              
                Output = connection;

                if (!connections.Contains(connection))
                    base.connections.Add(connection);
            }            
        }
    }
}
