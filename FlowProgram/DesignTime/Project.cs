using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram.DesignTime
{
    public class Project : Entity
    {
        public List<Document> Documents;

        public Document EntryPoint;

        public string Name { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }        

        public Project()
        {
            Documents = new List<Document>();
        }
    }
}
