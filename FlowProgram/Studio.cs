using FlowProgram.DesignTime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using FlowProgram.Nodes;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraTab;
using FlowProgram.Controls;

namespace FlowProgram
{
    public partial class Studio : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Project project { get; set; } = new Project();

        public string ProjectLocation { get; set; } = "";

        public Studio()
        {
            InitializeComponent();
        }

        public void LoadProject(string projectFile)
        {
            if(File.Exists(projectFile))
            {
                try
                {
                    project = JsonConvert.DeserializeObject<Project>(File.ReadAllText(projectFile), DefaultJsonSettings);
                    ProjectLocation = projectFile;

                    DisplayProject();
                }
                catch (Exception ex)
                {
                    // #TODO# show custom error box!
                    MessageBox.Show(ex.ToString());
                }                
            }
            else
            {
                // #TODO#: Show msg, saying file does not exist!
            }
        }

        public void SaveProject(string projectFile)
        {
            try
            {
                File.WriteAllText(projectFile, JsonConvert.SerializeObject(project, DefaultJsonSettings));
                ProjectLocation = projectFile;
            }
            catch (Exception ex)
            {
                // #TODO# show custom error box!
                MessageBox.Show(ex.ToString());
            }
        }
        
        public void AddItemToToolBox(VisibleEntity node, DataTable dt)
        {
            DataRow dr = dt.NewRow();

            var bmp = new Bitmap(24, 24);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                node.Size = new Size(24, 24);
                node.Location = new Point(0, 0);
                VisibleEntity.DisplayMode = false;

                node.Render(FlowEditor.GetThemeFromItemNoFlow(node), g, Point.Empty);

                VisibleEntity.DisplayMode = true;
                dr["Icon"] = bmp;
                dr["Name"] = node.Type().Name;
                dr["Node"] = node.Type();

                dt.Rows.Add(dr);
            }            
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() {
                Filter = "project files (*.FlowProject)|*.FlowProject",
                Title = "Flow Project"
            })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadProject(ofd.FileName);
                }
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(ProjectLocation.Length != 0)
            {
                SaveProject(ProjectLocation);
            }
            else
            {
                using (SaveFileDialog ofd = new SaveFileDialog()
                {
                    Filter = "project files (*.FlowProject)|*.FlowProject",
                    Title = "Flow Project"
                })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        SaveProject(ofd.FileName);
                    }
                }
            }            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProjectLocation = "";
            project = new Project();

            DisplayProject();
        }
        
        public void SetupSolution()
        {
            if (gridControl1.DataSource != null)
                return;

            var dt = new DataTable();

            dt.Columns.Add("Icon", typeof(Image));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Document", typeof(Document));

            gridControl1.DataSource = dt;
        }        

        public void DisplayProject()
        {
            xtraTabControl1.TabPages.Clear();

            gridControl1.DataSource = null;

            SetupSolution();

            var dt = (DataTable)gridControl1.DataSource;

            for (int i = 0; i < project.Documents.Count; i++)
            {
                DataRow dr = dt.NewRow();

                dr["Icon"] = null;
                dr["Name"] = project.Documents[i].Name;
                dr["Document"] = project.Documents[i];

                dt.Rows.Add(dr);
            }

            dt.AcceptChanges();            
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
                return;

            FlowXtraTabPage.
                AddToXtraTabControl(xtraTabControl1, gridView1.GetFocusedRowCellValue("Document") as Document);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            var newDoc = new Document
            {
                Name = $"FlowDocument{project.Documents.Count + 1}"
            };

            SetupSolution();

            DataTable dt = (DataTable)gridControl1.DataSource;

            DataRow dr = dt.NewRow();

            dr["Icon"] = null;
            dr["Name"] = newDoc.Name;
            dr["Document"] = newDoc;

            dt.Rows.Add(dr);

            dt.AcceptChanges();

            project.Documents.Add(newDoc);
            
            FlowXtraTabPage.
                AddToXtraTabControl(xtraTabControl1, newDoc);
        }

        private static readonly JsonSerializerSettings DefaultJsonSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            DateFormatString = "yyyy-MM-dd",
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore, //prevents a infinite loop of serialisation
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full //FIXES ISSUE WHERE JSON HAS NO IDEA WHAT TO DO WITH DYNAMIC PROXIES GENERATED FROM EF
        }; // able to link to DLL

        private void Studio_Load(object sender, EventArgs e)
        {
            var dt = new DataTable();

            dt.Columns.Add("Icon", typeof(Image));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Node", typeof(Type));

            AddItemToToolBox(new BoolNode(), dt);
            AddItemToToolBox(new ByteNode(), dt);
            AddItemToToolBox(new DateTimeNode(), dt);
            AddItemToToolBox(new DecimalNode(), dt);
            AddItemToToolBox(new DoubleNode(), dt);
            AddItemToToolBox(new FlowNode(), dt);
            AddItemToToolBox(new FunctionNode(), dt);
            AddItemToToolBox(new IntNode(), dt);
            AddItemToToolBox(new LongNode(), dt);
            AddItemToToolBox(new MathNode(), dt);
            AddItemToToolBox(new NewObjectNode(), dt);
            AddItemToToolBox(new ShortNode(), dt);
            AddItemToToolBox(new StringNode(), dt);
            AddItemToToolBox(new VoidNode(), dt);

            dt.AcceptChanges();

            gridControl2.DataSource = dt;
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            var arg = e as ClosePageButtonEventArgs;
            if(arg != null)
            {
                xtraTabControl1.TabPages.Remove((XtraTabPage)arg.Page);
            }
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            if(gridView2.FocusedRowHandle > -1 &&
                xtraTabControl1.SelectedTabPage != null && xtraTabControl1.SelectedTabPage is FlowXtraTabPage)
            {
                var flow = ((FlowXtraTabPage)xtraTabControl1.SelectedTabPage).Flow;
                var ve = (VisibleEntity)Activator.CreateInstance((Type)gridView2.GetFocusedRowCellValue("Node"));

                ve.Size = new Size(75, 75);
                ve.Location = flow.Document.ViewPoint.Add(new Point(flow.Width / 2, flow.Height / 2).Sub(new Point(ve.Size.Width / 2, ve.Size.Height / 2)));
                
                flow.Document.Items.Add(ve);

                flow.FocusedItem = ve;

                flow.Refresh();
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if(e.Menu != null)
            {
                e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("Set as Entry Point", (a, e2) => {
                    if (gridView1.FocusedRowHandle < 0)
                        return;
                    var doc = (Document)gridView1.GetFocusedRowCellValue("Document");
                    int prevIndex = -1;

                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        if(gridView1.GetRowCellValue(i, "Document") == project.EntryPoint)
                        {
                            prevIndex = i;break;
                        }
                    }

                    project.EntryPoint = (Document)gridView1.GetFocusedRowCellValue("Document");

                    gridView1.RefreshRow(gridView1.FocusedRowHandle);
                    if(prevIndex > -1)
                        gridView1.RefreshRow(prevIndex);
                }));
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if(project.EntryPoint == (Document)gridView1.GetRowCellValue(e.RowHandle, "Document"))
            {
                e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Bold);                
            }
        }
    }
}
