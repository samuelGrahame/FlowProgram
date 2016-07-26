using FlowProgram.DesignTime;
using FlowProgram.Nodes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram
{
    public static class Helper
    {
        public enum Edges
        {
            All,
            Top,
            Bottom
        }

        public static VisibleEntity[] GetRegisteredNodes()
        {
            return new VisibleEntity[] {
                new EntryNode(), new VarNode(),
                new BoolNode(), new ByteNode(), new DateTimeNode() , new DecimalNode(), new DoubleNode(), new FunctionNode(), new IntNode(),
                new LongNode(), new MathNode(), new NewObjectNode(), new ShortNode(), new StringNode(), new VoidNode()};
        }

        public static ThemeConfiguration GetDefaultThemeConfiguration()
        {
            var DefaultBackground = Color.FromArgb(100, 75, 75, 75);
            var DefaultFont = new Font("segoe ui", 11);
            var random = new Random(3);
            var themeConfig = new ThemeConfiguration();
            foreach (var item in GetRegisteredNodes())
            {
                var code = Color.FromArgb(255, Color.FromArgb(random.Next()));                

                var ItemHeaderColor = code;

                themeConfig.Directory.Add(item.Type(), new Theme()
                {
                    BackColor = DefaultBackground,
                    Border = true,
                    HeaderColor = ItemHeaderColor,
                    BorderThickness = 2,
                    BorderColor = Color.FromArgb(100, Color.White),
                    CornerRadius = 10,
                    Forecolor = Color.White,
                    Font = DefaultFont,
                    FocusedTheme = new Theme()
                    {
                        BackColor = DefaultBackground,
                        Border = true,
                        HeaderColor = ItemHeaderColor,
                        BorderThickness = 2,
                        BorderColor = Color.FromArgb(100, Color.Black),
                        CornerRadius = 10,
                        Forecolor = Color.White,
                        Font = DefaultFont
                    },
                    HoverTheme = new Theme()
                    {
                        BackColor = Color.FromArgb(100, DefaultBackground),
                        Border = true,
                        HeaderColor = ItemHeaderColor,
                        BorderThickness = 2,
                        BorderColor = Color.FromArgb(100, Color.White),
                        CornerRadius = 10,
                        Forecolor = Color.White,
                        Font = DefaultFont
                    }
                });                
            }

            return themeConfig;
        }

        public static readonly JsonSerializerSettings DefaultJsonSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            DateFormatString = "yyyy-MM-dd",
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore, //prevents a infinite loop of serialisation
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full //FIXES ISSUE WHERE JSON HAS NO IDEA WHAT TO DO WITH DYNAMIC PROXIES GENERATED FROM EF
        }; // able to link to DLL

        public static double GetDistanceBetweenPoints(this Point p, Point q)
        {
            double a = p.X - q.X;
            double b = p.Y - q.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius = 0, Edges edge = Edges.All)
        {            
            using (GraphicsPath path = RoundedRect(bounds, cornerRadius, edge))
            {
                graphics.DrawPath(pen, path);
            }
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius = 0, Edges edge = Edges.All)
        {
            using (GraphicsPath path = RoundedRect(bounds, cornerRadius, edge))
            {
                graphics.FillPath(brush, path);
            }
        }

        public static Point Mul(this Point A, Point B)
        {
            return new Point(A.X * B.X, A.Y * B.Y);
        }

        public static Point Mul(this Point A, int B)
        {
            return new Point(A.X * B, A.Y * B);
        }

        public static Point Sub(this Point A, Point B)
        {
            return new Point(A.X - B.X, A.Y - B.Y);
        }

        public static Point Add(this Point A, Point B)
        {
            return new Point(A.X + B.X, A.Y + B.Y);
        }

        public static Size Sub(this Size A, Size B)
        {
            return new Size(A.Width - B.Width, A.Height- B.Height);
        }

        public static Size Add(this Size A, Size B)
        {
            return new Size(A.Width + B.Width, A.Height + B.Height);
        }

        public static GraphicsPath RoundedRect(Rectangle bounds, int radius = 0, Edges edge = Edges.All)
        {            
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);

            switch (edge)
            {
                case Edges.All:
                    // top left arc  
                    path.AddArc(arc, 180, 90);

                    // top right arc  
                    arc.X = bounds.Right - diameter;
                    path.AddArc(arc, 270, 90);

                    // bottom right arc
                    arc.Y = bounds.Bottom - diameter;
                    path.AddArc(arc, 0, 90);

                    // bottom left arc
                    arc.X = bounds.Left;
                    path.AddArc(arc, 90, 90);
                    break;
                case Edges.Top:
                    // top left arc 
                    path.AddArc(arc, 180, 90);

                    // top right arc
                    arc.X = bounds.Right - diameter;
                    path.AddArc(arc, 270, 90);

                    path.AddRectangle(new Rectangle(bounds.Location.Add(new Point (0, radius)), bounds.Size.Sub(new Size(0, radius))));

                    break;
                case Edges.Bottom:
                    // bottom right arc
                    arc.Y = bounds.Bottom - diameter;
                    path.AddArc(arc, 0, 90);

                    // bottom left arc
                    arc.X = bounds.Left;
                    path.AddArc(arc, 90, 90);
                    break;
                default:
                    break;
            }
            
            path.CloseFigure();
            return path;
        }
    }
}
