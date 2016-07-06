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
                obj.BackColor = Color.White;
                obj.Document = new DesignTime.Item();

                Random r = new Random();

                for (int i = 0; i < 20; i++)
                {
                    obj.Document.Containers.Add(new NodeContainer() { Location = new Point(r.Next(0, 500), r.Next(0, 500)), Size = new Size(100, 100) });
                }
                
                FlowEditor.ThemeConfig.DefaultTheme = new DesignTime.Theme()
                {
                    BackColor = Color.FromArgb(69, 69, 69), // /,/Color.FromArgb(69, 69, 69),                    
                    Border = false,
                    BorderThickness = 1,
                    BorderColor = Color.LightGray,
                    CornerRadius = 5,
                    Forecolor = Color.Yellow,
                    Font = new Font("Arial", 16),
                    FocusedTheme = new DesignTime.Theme()
                    {
                        BackColor = Color.FromArgb(69, 69, 69),                    
                        Border = true,
                        BorderThickness = 1,
                        BorderColor = Color.CornflowerBlue,
                        CornerRadius = 5,
                        Forecolor = Color.Yellow,
                        Font = new Font("Arial", 16),
                    },
                    HoverTheme = new DesignTime.Theme()
                    {
                        BackColor = Color.FromArgb(75, 75, 75), // /,/Color.FromArgb(69, 69, 69),                    
                        Border = false,
                        BorderThickness = 1,
                        BorderColor = Color.CornflowerBlue,
                        CornerRadius = 5,
                        Forecolor = Color.Yellow,
                        Font = new Font("Arial", 16),
                    }
                };

                test.Controls.Add(obj);                

                Application.Run(test);
            }

        }
    }
}
