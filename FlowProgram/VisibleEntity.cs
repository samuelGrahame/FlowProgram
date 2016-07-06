using FlowProgram.DesignTime;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowProgram
{
    class VisibleEntity : Entity
    {
        public Point Location;
        public Size Size;
        private readonly Point Offset = new Point(5, 5);
        private readonly Size OffsetSize = new Size(10, 10);

        public VisibleEntity()
        {
            Location = Point.Empty;
            Size = Size.Empty;
        }

        private Type type = null;

        public Type Type()
        {
            return type ?? (type = this.GetType());
        }
        
        /// <summary>
        /// Must call first, if you dont want the default stuff you can override and not call..
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="g"></param>
        public virtual void Render(Theme theme, Graphics g, Point ViewLocation)
        {
            // lets draw the base??            
            // what is good about this, we dont need to worry if we are selected or hovered because it gets taken care of that through the theme class.

            if (theme == null) // nothing to draw!
                return;

            // theme.BackColor

            if(theme.Shadow)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(25, Color.Black)))
                {
                    if (theme.CornerRadius == 0)
                        g.FillRectangle(brush, new Rectangle(ViewLocation.Sub(Offset), Size.Add(OffsetSize)));
                    else
                        g.FillRoundedRectangle(brush, new Rectangle(ViewLocation.Sub(Offset), Size.Add(OffsetSize)), theme.CornerRadius);
                }
            }

            using (SolidBrush brush = new SolidBrush(theme.BackColor))
            {
                if(theme.CornerRadius==0)
                    g.FillRectangle(brush, new Rectangle(ViewLocation, Size));
                else
                    g.FillRoundedRectangle(brush, new Rectangle(ViewLocation, Size), theme.CornerRadius);                
            }

            using (SolidBrush brush = new SolidBrush(theme.HeaderColor))
            {
                if (theme.CornerRadius == 0)
                    g.FillRectangle(brush, new Rectangle(ViewLocation, new Size(Size.Width, theme.HeaderHeight)));
                else
                    g.FillRoundedRectangle(brush, new Rectangle(ViewLocation, new Size(Size.Width, theme.HeaderHeight)), theme.CornerRadius, Helper.Edges.Top);
            }            

            if(this.type.Name.Length > 0)
            {
                using (SolidBrush brush = new SolidBrush(theme.Forecolor))
                {
                    TextRenderer.DrawText(g, this.type.Name, theme.Font, new Rectangle(ViewLocation.Add(new Point(0, theme.CornerRadius == 0 ? 0 : theme.CornerRadius / 4)), new Size(Size.Width, theme.HeaderHeight)), theme.Forecolor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);                    
                }
            }

            if (theme.Border && theme.BorderThickness > 0)
            {
                using (SolidBrush brush = new SolidBrush(theme.BorderColor))
                {
                    using (Pen pen = new Pen(brush, theme.BorderThickness))
                    {
                        if (theme.CornerRadius == 0)
                            g.DrawRectangle(pen, new Rectangle(ViewLocation, Size));
                        else
                            g.DrawRoundedRectangle(pen, new Rectangle(ViewLocation, Size), theme.CornerRadius);
                    }
                }
            }

            
        }
    }
}
