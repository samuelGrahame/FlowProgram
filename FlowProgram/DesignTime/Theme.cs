using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram.DesignTime
{
    public class Theme
    {
        // this is used for the Node themes...
        // What do we need for a node.
        // Forecolor
        // border?
        // border thickness
        // border color.
        // TextFont
        // BackColor
        // HoverTheme
        // FocusedTheme

        public Color Forecolor { get; set; }
        public Color BackColor { get; set; }
        public Color HeaderColor { get; set; }
        public bool Border { get; set; } // do we need border enable off and on, Thickness can be used.
        public int BorderThickness { get; set; }
        public Color BorderColor { get; set; }
        public Font Font { get; set; }
        public Theme HoverTheme { get; set; } // if null it will be default
        public Theme FocusedTheme { get; set; } // '' ''
        public int CornerRadius { get; set; }
        public bool Shadow { get; set; }
        public int HeaderHeight { get; set; } = 25;
    }
}
