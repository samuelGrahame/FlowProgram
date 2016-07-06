using FlowProgram.Controls;
using FlowProgram.Nodes;
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

                obj.Document.Containers.Add(new IntNode() { Location = new Point(r.Next(0, 500), r.Next(0, 500)), Size = new Size(100, 100) });
                obj.Document.Containers.Add(new LongNode() { Location = new Point(r.Next(0, 500), r.Next(0, 500)), Size = new Size(100, 100) });
                obj.Document.Containers.Add(new DoubleNode() { Location = new Point(r.Next(0, 500), r.Next(0, 500)), Size = new Size(100, 100) });

                FlowEditor.ThemeConfig.DefaultTheme = new DesignTime.Theme()
                {
                    BackColor = Color.FromArgb(69, 69, 69), // /,/Color.FromArgb(69, 69, 69),                    
                    Border = false,
                    HeaderColor = Color.ForestGreen,
                    BorderThickness = 1,
                    BorderColor = Color.FromArgb(50, 50, 50),
                    CornerRadius = 10,
                    Forecolor = Color.White,
                    Font = new Font("segoe ui", 11),
                    Shadow = true,
                    FocusedTheme = new DesignTime.Theme()
                    {
                        BackColor = Color.FromArgb(69, 69, 69),
                        HeaderColor = Color.ForestGreen,
                        Border = false,
                        BorderThickness = 1,
                        BorderColor = Color.FromArgb(50, 50, 50),
                        CornerRadius = 10,
                        Forecolor = Color.White,
                        Font = new Font("segoe ui", 11),
                        Shadow = true
                    },
                    HoverTheme = new DesignTime.Theme()
                    {
                        BackColor = Color.FromArgb(75, 75, 75), // /,/Color.FromArgb(69, 69, 69),                    
                        Border = false,
                        HeaderColor = Color.ForestGreen,
                        BorderThickness = 1,
                        BorderColor = Color.FromArgb(50, 50, 50),
                        CornerRadius = 10,
                        Forecolor = Color.White,
                        Font = new Font("segoe ui", 11),
                        Shadow = true
                    }
                };


                FlowEditor.ThemeConfig.Directory.Add(typeof(LongNode), new DesignTime.Theme()
                {
                    BackColor = Color.FromArgb(69, 69, 69), // /,/Color.FromArgb(69, 69, 69),                    
                    Border = false,
                    HeaderColor = Color.LightBlue,
                    BorderThickness = 1,
                    BorderColor = Color.FromArgb(50, 50, 50),
                    CornerRadius = 10,
                    Forecolor = Color.White,
                    Font = new Font("segoe ui", 11),
                    Shadow = true,
                    FocusedTheme = new DesignTime.Theme()
                    {
                        BackColor = Color.FromArgb(69, 69, 69),
                        HeaderColor = Color.LightBlue,
                        Border = false,
                        BorderThickness = 1,
                        BorderColor = Color.FromArgb(50, 50, 50),
                        CornerRadius = 10,
                        Forecolor = Color.White,
                        Font = new Font("segoe ui", 11),
                        Shadow = true
                    },
                    HoverTheme = new DesignTime.Theme()
                    {
                        BackColor = Color.FromArgb(75, 75, 75), // /,/Color.FromArgb(69, 69, 69),                    
                        Border = false,
                        HeaderColor = Color.LightBlue,
                        BorderThickness = 1,
                        BorderColor = Color.FromArgb(50, 50, 50),
                        CornerRadius = 10,
                        Forecolor = Color.White,
                        Font = new Font("segoe ui", 11),
                        Shadow = true
                    }
                });

                FlowEditor.ThemeConfig.Directory.Add(typeof(DoubleNode), new DesignTime.Theme()
                {
                    BackColor = Color.FromArgb(69, 69, 69), // /,/Color.FromArgb(69, 69, 69),                    
                    Border = false,
                    HeaderColor = Color.OrangeRed,
                    BorderThickness = 1,
                    BorderColor = Color.FromArgb(50, 50, 50),
                    CornerRadius = 10,
                    Forecolor = Color.White,
                    Font = new Font("segoe ui", 11),
                    Shadow = true,
                    FocusedTheme = new DesignTime.Theme()
                    {
                        BackColor = Color.FromArgb(69, 69, 69),
                        HeaderColor = Color.OrangeRed,
                        Border = false,
                        BorderThickness = 1,
                        BorderColor = Color.FromArgb(50, 50, 50),
                        CornerRadius = 10,
                        Forecolor = Color.White,
                        Font = new Font("segoe ui", 11),
                        Shadow = true
                    },
                    HoverTheme = new DesignTime.Theme()
                    {
                        BackColor = Color.FromArgb(75, 75, 75), // /,/Color.FromArgb(69, 69, 69),                    
                        Border = false,
                        HeaderColor = Color.OrangeRed,
                        BorderThickness = 1,
                        BorderColor = Color.FromArgb(50, 50, 50),
                        CornerRadius = 10,
                        Forecolor = Color.White,
                        Font = new Font("segoe ui", 11),
                        Shadow = true
                    }
                });

                test.Controls.Add(obj);                

                Application.Run(test);
            }

        }
    }
}
