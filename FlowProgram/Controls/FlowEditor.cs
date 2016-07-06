﻿using FlowProgram.DesignTime;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowProgram.Controls
{
    class FlowEditor : Control
    {
        public Item Document = null;
        public static ThemeConfiguration ThemeConfig = new ThemeConfiguration();
        public VisibleEntity HoverItem = null;
        public VisibleEntity FocusedItem = null;
        public VisibleEntity DragItem = null;

        public Point ViewPoint = Point.Empty;

        public Point OffsetClick;
        public Point OffsetMiddleClick;

        private bool IsLeftMouseDown;
        private bool IsMouseWheenDown;
        private bool IsSpaceDown;

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

            if(IsLeftMouseDown)
            {                
                var FoundItem = GetEntityByPoint(e.Location);

                if (FoundItem != null)
                {
                    OffsetClick = FoundItem.Location.Sub(e.Location);
                }

                DragItem = FoundItem;
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
            var mousePoint = p.Add(ViewPoint);

            for (int i = Document.Containers.Count - 1; i >= 0; i--)
            {
                VisibleEntity Item = Document.Containers[i];

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
                IsLeftMouseDown = false;
            IsMouseWheenDown = e.Button == MouseButtons.Middle;
            if (IsMouseWheenDown)
                IsMouseWheenDown = false;

            if (DragItem != null)
            {
                Document.Containers.Remove(DragItem);
                Document.Containers.Add(DragItem);

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
                ViewPoint = IsSpaceDown ?
                    ViewPoint.Add(e.Location.Sub(OffsetMiddleClick).Mul(2)) :
                    ViewPoint.Sub(e.Location.Sub(OffsetMiddleClick));

                OffsetMiddleClick = e.Location;

                RefreshScreen = true;
            }

            if (IsLeftMouseDown)
            {
                if(DragItem != null)
                {
                    DragItem.Location = OffsetClick.Add(e.Location);
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
                            
            if (Document == null || Document.Containers.Count == 0)
                return;

            for (int i = 0, length = Document.Containers.Count; i < length; i++)
            {
                VisibleEntity Item = Document.Containers[i];                
                if(DragItem != Item)
                {
                    Item.Render(GetThemeFromItem(Item), e.Graphics, Item.Location.Sub(ViewPoint), true);
                }
            }

            if(DragItem != null)
            {
                DragItem.Render(GetThemeFromItem(DragItem), e.Graphics, DragItem.Location.Sub(ViewPoint), true);
            }
        }
    }
}
