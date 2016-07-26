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
    public class VisibleEntity : Entity
    {
        public static bool DisplayMode = true;
        public Point Location;
        public Size Size;
        private readonly Point Offset = new Point(5, 5);
        private readonly Size OffsetSize = new Size(10, 10);
        private static TextFormatFlags CentreText = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
        public ConnectionRules ConnectionRules = ConnectionRules.Output;
                
        /// <summary>
        /// To be added from higher objects..
        /// </summary>
        public List<NodeConnection> Connections = new List<NodeConnection>();

        /// <summary>
        /// This does nothing
        /// </summary>
        /// <param name="connection"></param>
        public void AddConnection(NodeConnection connection)
        {
            // we are input!           
            if(ConnectionRules == ConnectionRules.Input || ConnectionRules == ConnectionRules.Both)
            {
                foreach (var item in Connections)
                {
                    if (item.Output == connection.Output && item.Input == connection.Input)
                    {
                        return;
                    }
                }
                if (!Connections.Contains(connection))
                    Connections.Add(connection);
            }
        }
        
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
                    if (theme.CornerRadius == 0 || !DisplayMode)
                        g.FillRectangle(brush, new Rectangle(ViewLocation.Sub(Offset), Size.Add(OffsetSize)));
                    else
                        g.FillRoundedRectangle(brush, new Rectangle(ViewLocation.Sub(Offset), Size.Add(OffsetSize)), theme.CornerRadius);
                }
            }

            using (SolidBrush brush = new SolidBrush(theme.BackColor))
            {
                if(theme.CornerRadius == 0 || !DisplayMode)
                    g.FillRectangle(brush, new Rectangle(ViewLocation, Size));
                else
                    g.FillRoundedRectangle(brush, new Rectangle(ViewLocation, Size), theme.CornerRadius);                
            }

            using (SolidBrush brush = new SolidBrush(theme.HeaderColor))
            {
                if (theme.CornerRadius == 0 || !DisplayMode)
                    g.FillRectangle(brush, new Rectangle(ViewLocation, new Size(Size.Width, theme.HeaderHeight)));
                else
                    g.FillRoundedRectangle(brush, new Rectangle(ViewLocation, new Size(Size.Width, theme.HeaderHeight)), theme.CornerRadius, Helper.Edges.Top);
            }            

            if(DisplayMode && this.Type().Name.Length > 0)
            {
                using (SolidBrush brush = new SolidBrush(theme.Forecolor))
                {
                    TextRenderer.DrawText(g, this.Type().Name, theme.Font, new Rectangle(ViewLocation.Add(new Point(0, theme.CornerRadius == 0 ? 0 : (int)(theme.CornerRadius * 0.25f))), new Size(Size.Width, theme.HeaderHeight)), theme.Forecolor, CentreText);                    
                }
            }

            if (theme.Border && theme.BorderThickness > 0)
            {
                using (SolidBrush brush = new SolidBrush(theme.BorderColor))
                {
                    using (Pen pen = new Pen(brush, theme.BorderThickness))
                    {
                        if (theme.CornerRadius == 0 || !DisplayMode)
                            g.DrawRectangle(pen, new Rectangle(ViewLocation, Size));
                        else
                            g.DrawRoundedRectangle(pen, new Rectangle(ViewLocation, Size), theme.CornerRadius);
                    }
                }
            }
        }
    }

    public enum ConnectionRules
    {
        Output,
        Input,
        Both,
        None
    }
}
