using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram.DesignTime
{
    public class Document
    {        
        public Point ViewPoint { get; set; }
        public List<VisibleEntity> Items;
        public string Name { get; set; }        

        public Document()
        {
            Items = new List<VisibleEntity>();
        }
    }
}
