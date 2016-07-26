using FlowProgram.Controls;
using FlowProgram.DesignTime;
using FlowProgram.Nodes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowProgram
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            // Load Themes!
            if(File.Exists("ThemeConfiguration.settings"))
            {
                FlowEditor.ThemeConfig = JsonConvert.DeserializeObject<ThemeConfiguration>(File.ReadAllText("ThemeConfiguration.settings"), Helper.DefaultJsonSettings);
            }

            if (FlowEditor.ThemeConfig == null || FlowEditor.ThemeConfig.Directory.Count == 0)
                FlowEditor.ThemeConfig = Helper.GetDefaultThemeConfiguration();            

            var BackColor = Color.FromArgb(100, 75, 75, 75);

            //FlowEditor.ThemeConfig.Directory.Add(typeof(IntNode), new DesignTime.Theme()
            //{
            //    BackColor = BackColor, // /,/Color.FromArgb(69, 69, 69),                    
            //    Border = true,
            //    HeaderColor = Color.ForestGreen,
            //    BorderThickness = 2,
            //    BorderColor = Color.FromArgb(100, Color.White),
            //    CornerRadius = 10,
            //    Forecolor = Color.White,
            //    Font = new Font("segoe ui", 11),
            //    FocusedTheme = new DesignTime.Theme()
            //    {
            //        BackColor = BackColor,
            //        HeaderColor = Color.ForestGreen,
            //        Border = true,
            //        BorderThickness = 2,
            //        BorderColor = Color.FromArgb(100, Color.White),
            //        CornerRadius = 10,
            //        Forecolor = Color.White,
            //        Font = new Font("segoe ui", 11),
            //    },
            //    HoverTheme = new DesignTime.Theme()
            //    {
            //        BackColor = Color.FromArgb(100, 75, 75, 75), // /,/Color.FromArgb(69, 69, 69),                    
            //        Border = true,
            //        HeaderColor = Color.ForestGreen,
            //        BorderThickness = 2,
            //        BorderColor = Color.FromArgb(100, Color.White),
            //        CornerRadius = 10,
            //        Forecolor = Color.White,
            //        Font = new Font("segoe ui", 11),
            //    }
            //});

            //FlowEditor.ThemeConfig.Directory.Add(typeof(LongNode), new DesignTime.Theme()
            //{
            //    BackColor = BackColor, // /,/Color.FromArgb(69, 69, 69),                    
            //    Border = true,
            //    HeaderColor = Color.LightBlue,
            //    BorderThickness = 2,
            //    BorderColor = Color.FromArgb(100, Color.White),
            //    CornerRadius = 10,
            //    Forecolor = Color.White,
            //    Font = new Font("segoe ui", 11),
            //    FocusedTheme = new DesignTime.Theme()
            //    {
            //        BackColor = BackColor,
            //        HeaderColor = Color.LightBlue,
            //        Border = true,
            //        BorderThickness = 2,
            //        BorderColor = Color.FromArgb(100, Color.White),
            //        CornerRadius = 10,
            //        Forecolor = Color.White,
            //        Font = new Font("segoe ui", 11),
            //    },
            //    HoverTheme = new DesignTime.Theme()
            //    {
            //        BackColor = BackColor, // /,/Color.FromArgb(69, 69, 69),                    
            //        Border = true,
            //        HeaderColor = Color.LightBlue,
            //        BorderThickness = 2,
            //        BorderColor = Color.FromArgb(100, Color.White),
            //        CornerRadius = 10,
            //        Forecolor = Color.White,
            //        Font = new Font("segoe ui", 11),
            //    }
            //});

            //FlowEditor.ThemeConfig.Directory.Add(typeof(DoubleNode), new DesignTime.Theme()
            //{
            //    BackColor = BackColor, // /,/Color.FromArgb(69, 69, 69),                    
            //    Border = true,
            //    HeaderColor = Color.OrangeRed,
            //    BorderThickness = 2,
            //    BorderColor = Color.FromArgb(100, Color.White),
            //    CornerRadius = 10,
            //    Forecolor = Color.White,
            //    Font = new Font("segoe ui", 11),
            //    FocusedTheme = new DesignTime.Theme()
            //    {
            //        BackColor = BackColor,
            //        HeaderColor = Color.OrangeRed,
            //        Border = true,
            //        BorderThickness = 2,
            //        BorderColor = Color.FromArgb(100, Color.White),
            //        CornerRadius = 10,
            //        Forecolor = Color.White,
            //        Font = new Font("segoe ui", 11),
            //    },
            //    HoverTheme = new DesignTime.Theme()
            //    {
            //        BackColor = BackColor, // /,/Color.FromArgb(69, 69, 69),                    
            //        Border = true,
            //        HeaderColor = Color.OrangeRed,
            //        BorderThickness = 2,
            //        BorderColor = Color.FromArgb(100, Color.White),
            //        CornerRadius = 10,
            //        Forecolor = Color.White,
            //        Font = new Font("segoe ui", 11)
            //    }
            //});

            //FlowEditor.ThemeConfig.Directory.Add(typeof(StringNode), new DesignTime.Theme()
            //{
            //    BackColor = BackColor, // /,/Color.FromArgb(69, 69, 69),                    
            //    Border = true,
            //    HeaderColor = Color.Purple,
            //    BorderThickness = 2,
            //    BorderColor = Color.FromArgb(100, Color.White),
            //    CornerRadius = 10,
            //    Forecolor = Color.White,
            //    Font = new Font("segoe ui", 11),
            //    FocusedTheme = new DesignTime.Theme()
            //    {
            //        BackColor = BackColor,
            //        HeaderColor = Color.Purple,
            //        Border = true,
            //        BorderThickness = 2,
            //        BorderColor = Color.FromArgb(100, Color.White),
            //        CornerRadius = 10,
            //        Forecolor = Color.White,
            //        Font = new Font("segoe ui", 11),
            //    },
            //    HoverTheme = new DesignTime.Theme()
            //    {
            //        BackColor = BackColor, // /,/Color.FromArgb(69, 69, 69),                    
            //        Border = true,
            //        HeaderColor = Color.Purple,
            //        BorderThickness = 2,
            //        BorderColor = Color.FromArgb(100, Color.White),
            //        CornerRadius = 10,
            //        Forecolor = Color.White,
            //        Font = new Font("segoe ui", 11),
            //    }
            //});

            Application.Run(new Studio());

            //using (Test test = new Test())
            //{
            //    var obj = new FlowEditor() { Dock = DockStyle.Fill };
            //    obj.BackColor = Color.White;
            //    obj.Document = new DesignTime.Document();

            //    Random r = new Random();

            //    var BackColor = Color.FromArgb(100, 75, 75, 75);

            //    obj.Document.Items.Add(new IntNode() { Location = new Point(r.Next(0, 500), r.Next(0, 500)), Size = new Size(100, 100) });
            //    obj.Document.Items.Add(new LongNode() { Location = new Point(r.Next(0, 500), r.Next(0, 500)), Size = new Size(100, 100) });
            //    obj.Document.Items.Add(new DoubleNode() { Location = new Point(r.Next(0, 500), r.Next(0, 500)), Size = new Size(100, 100) });
            //    obj.Document.Items.Add(new StringNode() { Value = "Hello World", Location = new Point(r.Next(0, 500), r.Next(0, 500)), Size = new Size(100, 100) });



            //    test.Controls.Add(obj);                

            //    Application.Run(test);
            //}

        }
    }
}
