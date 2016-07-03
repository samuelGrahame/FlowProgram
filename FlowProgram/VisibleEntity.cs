using FlowProgram.DesignTime;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram
{
    class VisibleEntity : Entity
    {
        public PointF Location;
        public SizeF Size;

        public VisibleEntity()
        {
            Location = PointF.Empty;
            Size = SizeF.Empty;
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
        public virtual void Render(Theme theme, Graphics g)
        {
            // lets draw the base??            
            // what is good about this, we dont need to worry if we are selected or hovered because it gets taken care of that through the theme class.

            if (theme == null) // nothing to draw!
                return;

            // theme.BackColor
            using (SolidBrush brush = new SolidBrush(theme.BackColor))
            {
                g.FillRectangle(brush, new RectangleF(Location, Size));
            }

            if(theme.Border && theme.BorderThickness > 0)
            {
                using (SolidBrush brush = new SolidBrush(theme.BorderColor))
                {
                    using (Pen pen = new Pen(brush, theme.BorderThickness))
                    {
                        g.DrawRectangle(pen, Location.X, Location.Y, Size.Width, Size.Height);
                    }
                }
                //
                //g.DrawRectangle()
            }
             //g.FillRectangle()
        }
    }
}
