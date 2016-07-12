using FlowProgram.DesignTime;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowProgram.Nodes
{
    class BoolNode : LiteralNode
    {
        public bool Value;
        public BoolNode()
        {
            //Name = "Bool";
        }

        public override void Render(Theme theme, Graphics g, Point ViewLocation)
        {
            base.Render(theme, g, ViewLocation);

            using (SolidBrush brush = new SolidBrush(theme.Forecolor))
            {
                TextRenderer.DrawText(g, Convert.ToString(Value), theme.Font, new Rectangle(ViewLocation.Add(new Point(0, theme.HeaderHeight)), new Size(Size.Width, Size.Height - theme.HeaderHeight)), theme.Forecolor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
