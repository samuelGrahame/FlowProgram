using FlowProgram.DesignTime;
using System;
using System.Collections.Generic;
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
        public VisibleEntity HoverItem;
        public VisibleEntity FocusedItem;

        public FlowEditor()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                ControlStyles.ResizeRedraw | 
                ControlStyles.StandardClick | 
                ControlStyles.StandardDoubleClick  | 
                ControlStyles.UserPaint | 
                ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            
            if (Document == null || Document.Containers.Count == 0)
                return;

            for (int i = 0, length = Document.Containers.Count; i < length; i++)
            {
                VisibleEntity Item = Document.Containers[i];
                Theme ItemTheme = ThemeConfig.Directory.ContainsKey(Item.Type()) ? ThemeConfig.Directory[Item.Type()] : null;

                if (ItemTheme == null)
                {
                    ItemTheme = ThemeConfig.DefaultTheme;

                    if (ItemTheme == null) // Cant draw..., maybe the next one will have a set theme...
                        continue;
                }                    

                if(Item == HoverItem)
                {
                    if(ItemTheme.HoverTheme == null && ThemeConfig.DefaultTheme.HoverTheme != null)
                    {
                        ItemTheme = ThemeConfig.DefaultTheme.HoverTheme;
                    }else if(ItemTheme.HoverTheme != null)
                    {
                        ItemTheme = ItemTheme.HoverTheme;
                    }
                }else if(Item == FocusedItem)
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

                // should we send the drawing size??

                Item.Render(ItemTheme, e.Graphics);

                // lets get the theme.

                //Theme theme;
                //ThemeConfig
                //Item.Render()
            }
        }
    }
}
