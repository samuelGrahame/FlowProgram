using FlowProgram.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Test test = new Test())
            {
                var obj = new FlowEditor() { Dock = DockStyle.Fill };

                obj.Document = new DesignTime.Item();

                obj.Document.Containers.Add(new NodeContainer() { Location = new System.Drawing.Point(50, 50), Size = new System.Drawing.Size(100, 100) });

                FlowEditor.ThemeConfig.DefaultTheme = new DesignTime.Theme()
                {
                    BackColor = Color.FromArgb(69, 69, 69),                    
                    Border = false,                    
                    CornerRadius = 5,
                    Forecolor = System.Drawing.Color.Yellow,
                    Font = new System.Drawing.Font("Arial", 16)
                };

                test.Controls.Add(obj);                

                Application.Run(test);
            }

        }
    }
}
