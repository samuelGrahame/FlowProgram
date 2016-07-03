using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram.DesignTime
{
    class Project : Entity
    {
        public List<Item> Items;

        public Project()
        {
            Items = new List<Item>();
        }
    }
}
