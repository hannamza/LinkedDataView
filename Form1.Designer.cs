namespace LinkedDataView
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();
            this.PanelBtn = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelBtn = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelInputCircuit = new System.Windows.Forms.Panel();
            this.treeViewInput = new System.Windows.Forms.TreeView();
            this.panelSort = new System.Windows.Forms.Panel();
            this.comboBoxSort = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listViewOutputPtn = new System.Windows.Forms.ListView();
            this.lvop_item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvop_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvop_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.richTextBoxIputCircuit = new System.Windows.Forms.RichTextBox();
            this.listViewOutputCircuit = new System.Windows.Forms.ListView();
            this.lvoc_image = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvoc_number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvoc_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelSearch = new System.Windows.Forms.TableLayoutPanel();
            this.panelTopSearch = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.panelBottomSearch = new System.Windows.Forms.Panel();
            this.treeViewPtnSearch = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBoxPtnName = new System.Windows.Forms.RichTextBox();
            this.listViewPtnItemList = new System.Windows.Forms.ListView();
            this.lvpl_image = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvpl_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolTipOpen = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipClose = new System.Windows.Forms.ToolTip(this.components);
            this.tableMain.SuspendLayout();
            this.PanelBtn.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanelBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelInputCircuit.SuspendLayout();
            this.panelSort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tableLayoutPanelSearch.SuspendLayout();
            this.panelTopSearch.SuspendLayout();
            this.panelBottomSearch.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableMain
            // 
            this.tableMain.BackColor = System.Drawing.SystemColors.Window;
            this.tableMain.ColumnCount = 1;
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableMain.Controls.Add(this.PanelBtn, 0, 0);
            this.tableMain.Controls.Add(this.splitContainer1, 0, 1);
            this.tableMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableMain.Location = new System.Drawing.Point(0, 0);
            this.tableMain.Name = "tableMain";
            this.tableMain.RowCount = 2;
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.04112F));
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.95889F));
            this.tableMain.Size = new System.Drawing.Size(1264, 681);
            this.tableMain.TabIndex = 0;
            // 
            // PanelBtn
            // 
            this.PanelBtn.BackColor = System.Drawing.SystemColors.Window;
            this.PanelBtn.Controls.Add(this.tableLayoutPanel3);
            this.PanelBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBtn.Location = new System.Drawing.Point(3, 3);
            this.PanelBtn.Name = "PanelBtn";
            this.PanelBtn.Size = new System.Drawing.Size(1258, 76);
            this.PanelBtn.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.83421F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.16579F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanelBtn, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1252, 76);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // flowLayoutPanelBtn
            // 
            this.flowLayoutPanelBtn.Controls.Add(this.btnOpen);
            this.flowLayoutPanelBtn.Controls.Add(this.btnClose);
            this.flowLayoutPanelBtn.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelBtn.Name = "flowLayoutPanelBtn";
            this.flowLayoutPanelBtn.Size = new System.Drawing.Size(163, 70);
            this.flowLayoutPanelBtn.TabIndex = 0;
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::LinkedDataView.Properties.Resources.fileopen;
            this.btnOpen.Location = new System.Drawing.Point(3, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 67);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            this.btnOpen.MouseHover += new System.EventHandler(this.btnOpen_MouseHover);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::LinkedDataView.Properties.Resources.fileclose;
            this.btnClose.Location = new System.Drawing.Point(84, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 67);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseHover += new System.EventHandler(this.btnClose_MouseHover);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 85);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelInputCircuit);
            this.splitContainer1.Panel1.Controls.Add(this.panelSort);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1258, 593);
            this.splitContainer1.SplitterDistance = 414;
            this.splitContainer1.TabIndex = 1;
            // 
            // panelInputCircuit
            // 
            this.panelInputCircuit.BackColor = System.Drawing.SystemColors.Window;
            this.panelInputCircuit.Controls.Add(this.treeViewInput);
            this.panelInputCircuit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInputCircuit.Location = new System.Drawing.Point(0, 73);
            this.panelInputCircuit.Name = "panelInputCircuit";
            this.panelInputCircuit.Size = new System.Drawing.Size(414, 520);
            this.panelInputCircuit.TabIndex = 2;
            // 
            // treeViewInput
            // 
            this.treeViewInput.BackColor = System.Drawing.SystemColors.Control;
            this.treeViewInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewInput.Location = new System.Drawing.Point(0, 0);
            this.treeViewInput.Name = "treeViewInput";
            this.treeViewInput.Size = new System.Drawing.Size(414, 520);
            this.treeViewInput.TabIndex = 0;
            this.treeViewInput.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewInput_AfterSelect);
            // 
            // panelSort
            // 
            this.panelSort.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelSort.Controls.Add(this.comboBoxSort);
            this.panelSort.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSort.Location = new System.Drawing.Point(0, 0);
            this.panelSort.Name = "panelSort";
            this.panelSort.Size = new System.Drawing.Size(414, 73);
            this.panelSort.TabIndex = 1;
            // 
            // comboBoxSort
            // 
            this.comboBoxSort.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSort.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBoxSort.FormattingEnabled = true;
            this.comboBoxSort.Items.AddRange(new object[] {
            "회로번호로 정렬",
            "입력타입으로 정렬",
            "출력타입으로 정렬",
            "설비명으로 정렬",
            "출력내용으로 정렬"});
            this.comboBoxSort.Location = new System.Drawing.Point(84, 26);
            this.comboBoxSort.Name = "comboBoxSort";
            this.comboBoxSort.Size = new System.Drawing.Size(245, 23);
            this.comboBoxSort.TabIndex = 0;
            this.comboBoxSort.SelectedIndexChanged += new System.EventHandler(this.comboBoxSort_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(840, 593);
            this.splitContainer2.SplitterDistance = 410;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.BackColor = System.Drawing.Color.Yellow;
            this.splitContainer4.Panel2.Controls.Add(this.listViewOutputCircuit);
            this.splitContainer4.Size = new System.Drawing.Size(410, 593);
            this.splitContainer4.SplitterDistance = 283;
            this.splitContainer4.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.listViewOutputPtn, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.richTextBoxIputCircuit, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.53957F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.46043F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(410, 283);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // listViewOutputPtn
            // 
            this.listViewOutputPtn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvop_item,
            this.lvop_name,
            this.lvop_type});
            this.listViewOutputPtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewOutputPtn.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listViewOutputPtn.FullRowSelect = true;
            this.listViewOutputPtn.GridLines = true;
            this.listViewOutputPtn.HideSelection = false;
            this.listViewOutputPtn.Location = new System.Drawing.Point(3, 75);
            this.listViewOutputPtn.Name = "listViewOutputPtn";
            this.listViewOutputPtn.Size = new System.Drawing.Size(404, 205);
            this.listViewOutputPtn.TabIndex = 0;
            this.listViewOutputPtn.UseCompatibleStateImageBehavior = false;
            this.listViewOutputPtn.View = System.Windows.Forms.View.Details;
            this.listViewOutputPtn.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewOutputPtn_ColumnClick);
            this.listViewOutputPtn.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewOutputPtn_ItemSelectionChanged);
            // 
            // lvop_item
            // 
            this.lvop_item.Text = "출력 항목";
            this.lvop_item.Width = 80;
            // 
            // lvop_name
            // 
            this.lvop_name.Text = "패턴 이름";
            this.lvop_name.Width = 230;
            // 
            // lvop_type
            // 
            this.lvop_type.Text = "종류";
            this.lvop_type.Width = 80;
            // 
            // richTextBoxIputCircuit
            // 
            this.richTextBoxIputCircuit.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxIputCircuit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxIputCircuit.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.richTextBoxIputCircuit.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxIputCircuit.Name = "richTextBoxIputCircuit";
            this.richTextBoxIputCircuit.ReadOnly = true;
            this.richTextBoxIputCircuit.Size = new System.Drawing.Size(404, 66);
            this.richTextBoxIputCircuit.TabIndex = 1;
            this.richTextBoxIputCircuit.Text = "";
            // 
            // listViewOutputCircuit
            // 
            this.listViewOutputCircuit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvoc_image,
            this.lvoc_number,
            this.lvoc_name});
            this.listViewOutputCircuit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewOutputCircuit.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listViewOutputCircuit.FullRowSelect = true;
            this.listViewOutputCircuit.GridLines = true;
            this.listViewOutputCircuit.HideSelection = false;
            this.listViewOutputCircuit.Location = new System.Drawing.Point(0, 0);
            this.listViewOutputCircuit.Name = "listViewOutputCircuit";
            this.listViewOutputCircuit.Size = new System.Drawing.Size(410, 306);
            this.listViewOutputCircuit.TabIndex = 0;
            this.listViewOutputCircuit.UseCompatibleStateImageBehavior = false;
            this.listViewOutputCircuit.View = System.Windows.Forms.View.Details;
            this.listViewOutputCircuit.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewOutputCircuit_ColumnClick);
            // 
            // lvoc_image
            // 
            this.lvoc_image.Text = "종류";
            this.lvoc_image.Width = 55;
            // 
            // lvoc_number
            // 
            this.lvoc_number.Text = "번호";
            // 
            // lvoc_name
            // 
            this.lvoc_name.Text = "출력 회로";
            this.lvoc_name.Width = 345;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tableLayoutPanelSearch);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.Color.Magenta;
            this.splitContainer3.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer3.Size = new System.Drawing.Size(426, 593);
            this.splitContainer3.SplitterDistance = 199;
            this.splitContainer3.TabIndex = 0;
            // 
            // tableLayoutPanelSearch
            // 
            this.tableLayoutPanelSearch.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanelSearch.ColumnCount = 1;
            this.tableLayoutPanelSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSearch.Controls.Add(this.panelTopSearch, 0, 0);
            this.tableLayoutPanelSearch.Controls.Add(this.panelBottomSearch, 0, 1);
            this.tableLayoutPanelSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSearch.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelSearch.Name = "tableLayoutPanelSearch";
            this.tableLayoutPanelSearch.RowCount = 2;
            this.tableLayoutPanelSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.37374F));
            this.tableLayoutPanelSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.62626F));
            this.tableLayoutPanelSearch.Size = new System.Drawing.Size(426, 199);
            this.tableLayoutPanelSearch.TabIndex = 0;
            // 
            // panelTopSearch
            // 
            this.panelTopSearch.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelTopSearch.Controls.Add(this.btnSearch);
            this.panelTopSearch.Controls.Add(this.textBoxSearch);
            this.panelTopSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTopSearch.Location = new System.Drawing.Point(3, 3);
            this.panelTopSearch.Name = "panelTopSearch";
            this.panelTopSearch.Size = new System.Drawing.Size(420, 68);
            this.panelTopSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(293, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(110, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxSearch.Location = new System.Drawing.Point(25, 22);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(249, 23);
            this.textBoxSearch.TabIndex = 0;
            // 
            // panelBottomSearch
            // 
            this.panelBottomSearch.BackColor = System.Drawing.SystemColors.Window;
            this.panelBottomSearch.Controls.Add(this.treeViewPtnSearch);
            this.panelBottomSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomSearch.Location = new System.Drawing.Point(3, 77);
            this.panelBottomSearch.Name = "panelBottomSearch";
            this.panelBottomSearch.Size = new System.Drawing.Size(420, 119);
            this.panelBottomSearch.TabIndex = 1;
            // 
            // treeViewPtnSearch
            // 
            this.treeViewPtnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.treeViewPtnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewPtnSearch.Location = new System.Drawing.Point(0, 0);
            this.treeViewPtnSearch.Name = "treeViewPtnSearch";
            this.treeViewPtnSearch.Size = new System.Drawing.Size(420, 119);
            this.treeViewPtnSearch.TabIndex = 0;
            this.treeViewPtnSearch.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewPtnSearch_NodeMouseClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxPtnName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listViewPtnItemList, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.09375F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.90625F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(426, 390);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // richTextBoxPtnName
            // 
            this.richTextBoxPtnName.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxPtnName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxPtnName.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.richTextBoxPtnName.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxPtnName.Name = "richTextBoxPtnName";
            this.richTextBoxPtnName.ReadOnly = true;
            this.richTextBoxPtnName.Size = new System.Drawing.Size(420, 76);
            this.richTextBoxPtnName.TabIndex = 0;
            this.richTextBoxPtnName.Text = "";
            // 
            // listViewPtnItemList
            // 
            this.listViewPtnItemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvpl_image,
            this.lvpl_name});
            this.listViewPtnItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPtnItemList.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listViewPtnItemList.FullRowSelect = true;
            this.listViewPtnItemList.GridLines = true;
            this.listViewPtnItemList.HideSelection = false;
            this.listViewPtnItemList.Location = new System.Drawing.Point(3, 85);
            this.listViewPtnItemList.Name = "listViewPtnItemList";
            this.listViewPtnItemList.Size = new System.Drawing.Size(420, 302);
            this.listViewPtnItemList.TabIndex = 1;
            this.listViewPtnItemList.UseCompatibleStateImageBehavior = false;
            this.listViewPtnItemList.View = System.Windows.Forms.View.Details;
            this.listViewPtnItemList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewPtnItemList_ColumnClick);
            // 
            // lvpl_image
            // 
            this.lvpl_image.Text = "종류";
            this.lvpl_image.Width = 55;
            // 
            // lvpl_name
            // 
            this.lvpl_name.Text = "출력 회로";
            this.lvpl_name.Width = 355;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.tableMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableMain.ResumeLayout(false);
            this.PanelBtn.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanelBtn.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelInputCircuit.ResumeLayout(false);
            this.panelSort.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tableLayoutPanelSearch.ResumeLayout(false);
            this.panelTopSearch.ResumeLayout(false);
            this.panelTopSearch.PerformLayout();
            this.panelBottomSearch.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableMain;
        private System.Windows.Forms.FlowLayoutPanel PanelBtn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeViewInput;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ListView listViewOutputCircuit;
        private System.Windows.Forms.Panel panelSort;
        private System.Windows.Forms.Panel panelInputCircuit;
        private System.Windows.Forms.ComboBox comboBoxSort;
        private System.Windows.Forms.ColumnHeader lvoc_image;
        private System.Windows.Forms.ColumnHeader lvoc_number;
        private System.Windows.Forms.ColumnHeader lvoc_name;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox richTextBoxPtnName;
        private System.Windows.Forms.ListView listViewPtnItemList;
        private System.Windows.Forms.ColumnHeader lvpl_image;
        private System.Windows.Forms.ColumnHeader lvpl_name;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSearch;
        private System.Windows.Forms.Panel panelTopSearch;
        private System.Windows.Forms.Panel panelBottomSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.TreeView treeViewPtnSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListView listViewOutputPtn;
        private System.Windows.Forms.ColumnHeader lvop_item;
        private System.Windows.Forms.ColumnHeader lvop_name;
        private System.Windows.Forms.ColumnHeader lvop_type;
        private System.Windows.Forms.RichTextBox richTextBoxIputCircuit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBtn;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolTip toolTipOpen;
        private System.Windows.Forms.ToolTip toolTipClose;
    }
}

