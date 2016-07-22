using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowProgram.DesignTime;

namespace FlowProgram.Controls
{
    public class FlowXtraTabPage : XtraTabPage
    {
        public Controls.FlowEditor Flow;

        public FlowXtraTabPage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Flow = new FlowProgram.Controls.FlowEditor();
            this.SuspendLayout();
            // 
            // Flow
            // 
            this.Flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Flow.DrawLocation = true;
            this.Flow.DrawNodesNotVisible = true;            
            this.Flow.Name = "Flow";            
            this.Flow.TabIndex = 0;

            this.Controls.Add(this.Flow);
                    
            this.ResumeLayout(false);

        }

        public static void AddToXtraTabControl(XtraTabControl xtraTab, Document FlowDocument)
        {
            if (xtraTab == null || FlowDocument == null)
                return;

            for (int i = 0; i < xtraTab.TabPages.Count; i++)
            {
                var currentPage = (xtraTab.TabPages[i] as FlowXtraTabPage);
                if (currentPage != null && currentPage.Flow != null && currentPage.Flow.Document == FlowDocument)
                {
                    xtraTab.SelectedTabPage = currentPage;
                    return;
                }
            }

            var fxtp = new FlowXtraTabPage();

            fxtp.Flow.Document = FlowDocument;

            fxtp.Text = FlowDocument.Name;

            xtraTab.TabPages.Add(fxtp);
            xtraTab.SelectedTabPage = fxtp;
        }
    }
}
