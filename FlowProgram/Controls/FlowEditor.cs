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
            if (Document == null || Document.Containers.Count == 0)
                return;

            for (int i = 0, length = Document.Containers.Count; i < length; i++)
            {
                VisibleEntity Item = Document.Containers[i];


            }
        }
    }
}
