using FlowProgram.DesignTime;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowProgram.Controls
{
    public class FlowEditor : Control
    {
        public Document Document = new Document();
        public static ThemeConfiguration ThemeConfig = null;
        public VisibleEntity HoverItem = null;
        public VisibleEntity FocusedItem = null;
        public VisibleEntity DragItem = null;

        public VisibleEntity ClickedOn = null;
        
        private Point OffsetClick;
        private Point OffsetMiddleClick;

        private Point LeftClickDown;
        private Point LeftClickDragLocation;

        private bool IsLeftMouseDown;
        private bool IsMouseWheenDown;
        private bool IsSpaceDown;
        private bool IsDraggingPoint;

        public bool DrawLocation { get; set; } = true;
        public bool DrawNodesNotVisible { get; set; } = true;

      //  public VisibleEntity P1 = null;
      //  public VisibleEntity P2 = null;

        public FlowEditor()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                ControlStyles.ResizeRedraw | 
                ControlStyles.StandardClick | 
                ControlStyles.StandardDoubleClick  | 
                ControlStyles.UserPaint | 
                ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            IsSpaceDown = e.KeyCode == Keys.Space;

            //if (e.KeyCode == Keys.A)
            //    P1 = FocusedItem;
            //if (e.KeyCode == Keys.S)
            //    P2 = FocusedItem;

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            IsSpaceDown = e.KeyCode == Keys.Space;
            if (IsSpaceDown)
                IsSpaceDown = false;
            base.OnKeyUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            IsLeftMouseDown = e.Button == MouseButtons.Left;
            IsMouseWheenDown = e.Button == MouseButtons.Middle;
            IsDraggingPoint = false;

            if (IsLeftMouseDown)
            {
                LeftClickDown = e.Location;

                var FoundItem = GetEntityByPoint(LeftClickDown);
                ClickedOn = FoundItem;
                DragItem = null;

                if (FoundItem != null)
                {
                    
                    OffsetClick = FoundItem.Location.Sub(LeftClickDown);

                    if(new Rectangle(FoundItem.Location, new Size(FoundItem.Size.Width, GetThemeFromItem(FoundItem).HeaderHeight)).Contains(LeftClickDown.Add(Document.ViewPoint)))
                    {
                        // Drag!
                        DragItem = FoundItem;
                    }
                    else
                    {
                        if(FoundItem.ConnectionRules == ConnectionRules.Both || FoundItem.ConnectionRules == ConnectionRules.Output)
                        {
                            LeftClickDragLocation = LeftClickDown;
                            IsDraggingPoint = true;
                        }
                    }
                }
                
                FocusedItem = FoundItem;
            }
            if(IsMouseWheenDown)
            {
                OffsetMiddleClick = e.Location;
            }
                        
            base.OnMouseDown(e);
        }

        public VisibleEntity GetEntityByPoint(Point p)
        {
            var mousePoint = p.Add(Document.ViewPoint);

            for (int i = Document.Items.Count - 1; i >= 0; i--)
            {
                VisibleEntity Item = Document.Items[i];

                if (new Rectangle(Item.Location, Item.Size).Contains(mousePoint))
                {
                    return Item;                    
                }
            }
            return null;
        }        

        protected override void OnMouseUp(MouseEventArgs e)
        {
            IsLeftMouseDown = e.Button == MouseButtons.Left;
            if (IsLeftMouseDown)
            {
                IsLeftMouseDown = false;
                if(IsDraggingPoint)
                {
                    var FoundItem = GetEntityByPoint(LeftClickDragLocation);

                    if(FoundItem != null)
                    {
                        FoundItem.AddConnection(new NodeConnection() { Output = ClickedOn, Input = FoundItem });
                    }
                    ClickedOn = null;                    

                    IsDraggingPoint = false;
                }
            }
            IsMouseWheenDown = e.Button == MouseButtons.Middle;
            if (IsMouseWheenDown)
                IsMouseWheenDown = false;

            if (DragItem != null)
            {
                Document.Items.Remove(DragItem);
                Document.Items.Add(DragItem);

                DragItem = null;
            }
                

            Refresh();

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool RefreshScreen = false;

            if (IsMouseWheenDown)
            {                
                Document.ViewPoint = IsSpaceDown ?
                    Document.ViewPoint.Add(e.Location.Sub(OffsetMiddleClick).Mul(2)) :
                    Document.ViewPoint.Sub(e.Location.Sub(OffsetMiddleClick));

                OffsetMiddleClick = e.Location;

                RefreshScreen = true;
            }

            if (IsLeftMouseDown)
            {
                if(DragItem != null)
                {
                    DragItem.Location = OffsetClick.Add(e.Location);
                    RefreshScreen = true;
                }else if (IsDraggingPoint) 
                {
                    LeftClickDragLocation = e.Location;
                    RefreshScreen = true;
                }
            }else
            {
                var prev = HoverItem;
                HoverItem = GetEntityByPoint(e.Location);
                if (prev != HoverItem)
                    RefreshScreen = true;
            }
            if (RefreshScreen)
                Refresh();
            base.OnMouseMove(e);
        }

        public static Theme GetThemeFromItemNoFlow(VisibleEntity Item)
        {
            Theme ItemTheme = ThemeConfig.Directory.ContainsKey(Item.Type()) ? ThemeConfig.Directory[Item.Type()] : null;

            if (ItemTheme == null)
            {
                ItemTheme = ThemeConfig.DefaultTheme;

                if (ItemTheme == null) // Cant draw..., maybe the next one will have a set theme...
                    return null;
            }            

            return ItemTheme;
        }

        public Theme GetThemeFromItem(VisibleEntity Item)
        {
            Theme ItemTheme = ThemeConfig.Directory.ContainsKey(Item.Type()) ? ThemeConfig.Directory[Item.Type()] : null;

            if (ItemTheme == null)
            {
                ItemTheme = ThemeConfig.DefaultTheme;

                if (ItemTheme == null) // Cant draw..., maybe the next one will have a set theme...
                    return null;
            }

            if (Item == FocusedItem)
            {
                if (ItemTheme.FocusedTheme == null && ThemeConfig.DefaultTheme.FocusedTheme != null)
                {
                    ItemTheme = ThemeConfig.DefaultTheme.FocusedTheme;
                }
                else if (ItemTheme.FocusedTheme != null)
                {
                    ItemTheme = ItemTheme.FocusedTheme;
                }
            }
            else if (Item == HoverItem)
            {
                if (ItemTheme.HoverTheme == null && ThemeConfig.DefaultTheme.HoverTheme != null)
                {
                    ItemTheme = ThemeConfig.DefaultTheme.HoverTheme;
                }
                else if (ItemTheme.HoverTheme != null)
                {
                    ItemTheme = ItemTheme.HoverTheme;
                }
            }
            
            return ItemTheme;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            
            if (Document == null || Document.Items.Count == 0)
                return;

            // for testing.
            //if(P1 != null && P2 != null)
            //{
            //    e.Graphics.DrawCurve(Pens.Black, GetPointsFrom(P1, P2));
            //}

            List<VisibleEntity> NotVisible = new List<VisibleEntity>();

            Rectangle ViewRectangle = new Rectangle(this.Document.ViewPoint, this.Size);
            
            for (int i = 0, length = Document.Items.Count; i < length; i++)
            {
                VisibleEntity Item = Document.Items[i];
                if (DragItem != Item)
                {
                    if (ViewRectangle.IntersectsWith(new Rectangle(Item.Location, Item.Size)))
                    {
                        Point ItemLocationInView = Item.Location.Sub(Document.ViewPoint);

                        Item.Render(GetThemeFromItem(Item), e.Graphics, Item.Location.Sub(Document.ViewPoint));

                        // Render Connections                        
                        foreach (var connection in Item.Connections)
                        {
                            if(connection.Input != null && connection.Output != null)
                            {
                                Point Point2;

                                if(connection.Input == Item)
                                {
                                    Point2 = connection.Output.Location.Sub(Document.ViewPoint);
                                }
                                else if (connection.Output == Item)
                                {
                                    Point2 = connection.Input.Location.Sub(Document.ViewPoint);

                                }
                                else
                                {
                                    continue;
                                }

                                e.Graphics.DrawLine(Pens.Black, ItemLocationInView, Point2);
                            }
                        }                        
                    }
                    else
                    {
                        NotVisible.Add(Item);
                    }
                }
            }

            if(DragItem != null)
            {
                Point ItemLocationInView = DragItem.Location.Sub(Document.ViewPoint);

                DragItem.Render(GetThemeFromItem(DragItem), e.Graphics, ItemLocationInView);

                foreach (var connection in DragItem.Connections)
                {
                    if (connection.Input != null && connection.Output != null)
                    {
                        Point Point2;

                        if (connection.Input == DragItem)
                        {
                            Point2 = connection.Output.Location.Sub(Document.ViewPoint);
                        }
                        else if (connection.Output == DragItem)
                        {
                            Point2 = connection.Input.Location.Sub(Document.ViewPoint);

                        }
                        else
                        {
                            continue;
                        }

                        e.Graphics.DrawLine(Pens.Black, ItemLocationInView, Point2);
                    }
                }
            }

            if(DrawNodesNotVisible)
            {
                Point Centre = new Point(Document.ViewPoint.X + this.Size.Width / 2, Document.ViewPoint.Y + this.Size.Height / 2);
                
                for (int i = 0, length = NotVisible.Count; i < length; i++)
                {
                    //NotVisible[i]
                    if(NotVisible[i].Connections.Count > 0)
                    {
                        Point ItemLocationInView = NotVisible[i].Location.Sub(Document.ViewPoint);

                        foreach (var connection in NotVisible[i].Connections)
                        {
                            if (connection.Input != null && connection.Output != null)
                            {
                                Point Point2;

                                if (connection.Input == NotVisible[i])
                                {
                                    if(NotVisible.Contains(connection.Output))
                                    {
                                        continue;
                                    }
                                    Point2 = connection.Output.Location.Sub(Document.ViewPoint);
                                }
                                else if (connection.Output == NotVisible[i])
                                {
                                    if (NotVisible.Contains(connection.Input))
                                    {
                                        continue;
                                    }
                                    Point2 = connection.Input.Location.Sub(Document.ViewPoint);
                                }
                                else
                                {
                                    continue;
                                }

                                e.Graphics.DrawLine(Pens.Black, ItemLocationInView, Point2);
                            }
                        }
                    }
                    

                    // Get Distance?
                    double Distance = NotVisible[i].Location.GetDistanceBetweenPoints(Centre);
                    var Item = NotVisible[i];
                    if (Item.Type().Name.Length > 0)
                    {
                        Theme theme = GetThemeFromItem(Item);
                        using (SolidBrush brush = new SolidBrush(theme.Forecolor))
                        {
                            // #TODO# WORK OUT WHERE WE SHOULD DRAW NODE


                            //TextRenderer.DrawText(e.Graphics, Item.Type().Name, theme.Font, , theme.Forecolor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        }
                    }
                }
            }

            if(IsDraggingPoint)
            {
                //                private Point LeftClickDown;
                //private Point LeftClickDragLocation;
                e.Graphics.DrawLine(Pens.Black, LeftClickDown, LeftClickDragLocation);
            }

            if(DrawLocation)
            {                
                TextRenderer.DrawText(e.Graphics, string.Format("Location: {0}\r\nVisible: {1}\r\nHidden: {2}\r\nTotal: {3}",
                    Document.ViewPoint, Document.Items.Count - NotVisible.Count, NotVisible.Count, Document.Items.Count)
                    , this.Font, Point.Empty, this.ForeColor);
            }
        }

        //public Point[] GetPointsFrom(VisibleEntity a, VisibleEntity b)
        //{
        //    Point[] Points = new Point[4];
            
        //    Points[0] = a.Location;

        //    if (a.Location.Y < b.Location.Y)
        //    {
        //        Points[0].Y += a.Size.Height;
                
        //        Points[1] = Points[0].Add(new Point(0, a.Size.Height));

        //        Points[3] = b.Location;

        //        Points[2] = Points[3].Sub(new Point(0, b.Size.Height));
        //    }
        //    else
        //    {                               
        //        Points[1] = Points[0].Sub(new Point(0, a.Size.Height));

        //        Points[3] = b.Location;
        //        Points[3].Y += b.Size.Height;

        //        Points[2] = Points[3].Add(new Point(0, b.Size.Height));
        //    }

            

        //    return Points;
        //}
    }
}
