namespace WebcamController.Views
{
    partial class PresetsView
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ListViewGroup listViewGroup2 = new ListViewGroup("Nome da câmera", HorizontalAlignment.Left);
            ListViewItem listViewItem2 = new ListViewItem(new string[] { "Novo Preset", "Ctrl+Shift+1" }, -1);
            listPresets = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            ctxItem = new ContextMenuStrip(components);
            itemViewMode = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            toolStripMenuItem9 = new ToolStripMenuItem();
            toolStripMenuItem10 = new ToolStripMenuItem();
            toolStripMenuItem11 = new ToolStripMenuItem();
            itemSelectAll = new ToolStripMenuItem();
            itemApplyPreset = new ToolStripMenuItem();
            sprItem1 = new ToolStripSeparator();
            itemNewPreset = new ToolStripMenuItem();
            itemDeletePreset = new ToolStripMenuItem();
            itemRenamePreset = new ToolStripMenuItem();
            sprItem2 = new ToolStripSeparator();
            itemHotkey = new ToolStripMenuItem();
            imgSmall = new ImageList(components);
            imgLarge = new ImageList(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            btnDeletePreset = new Button();
            btnApplyPreset = new Button();
            btnNewPreset = new Button();
            opçõesToolStripMenuItem = new ToolStripMenuItem();
            itemReadOnly = new ToolStripMenuItem();
            ctxItem.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // listPresets
            // 
            listPresets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listPresets.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listPresets.ContextMenuStrip = ctxItem;
            listPresets.FullRowSelect = true;
            listViewGroup2.CollapsedState = ListViewGroupCollapsedState.Expanded;
            listViewGroup2.Header = "Nome da câmera";
            listViewGroup2.Name = "listViewGroup1";
            listViewGroup2.Subtitle = "Status";
            listPresets.Groups.AddRange(new ListViewGroup[] { listViewGroup2 });
            listPresets.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listViewItem2.Group = listViewGroup2;
            listPresets.Items.AddRange(new ListViewItem[] { listViewItem2 });
            listPresets.LabelEdit = true;
            listPresets.Location = new Point(3, 3);
            listPresets.Name = "listPresets";
            listPresets.ShowItemToolTips = true;
            listPresets.Size = new Size(255, 242);
            listPresets.TabIndex = 0;
            listPresets.UseCompatibleStateImageBehavior = false;
            listPresets.View = View.Details;
            listPresets.AfterLabelEdit += listPresets_AfterLabelEdit;
            listPresets.ItemActivate += listPresets_ItemActivate;
            listPresets.SelectedIndexChanged += listPresets_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Nome";
            columnHeader1.Width = 130;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Atalho";
            columnHeader2.Width = 100;
            // 
            // ctxItem
            // 
            ctxItem.Items.AddRange(new ToolStripItem[] { itemViewMode, itemSelectAll, itemApplyPreset, sprItem1, itemNewPreset, itemDeletePreset, itemRenamePreset, sprItem2, itemHotkey, opçõesToolStripMenuItem });
            ctxItem.Name = "contextItem";
            ctxItem.Size = new Size(199, 214);
            // 
            // itemViewMode
            // 
            itemViewMode.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem7, toolStripMenuItem8, toolStripMenuItem9, toolStripMenuItem10, toolStripMenuItem11 });
            itemViewMode.Name = "itemViewMode";
            itemViewMode.Size = new Size(198, 22);
            itemViewMode.Text = "Exibir";
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(163, 22);
            toolStripMenuItem7.Tag = "0";
            toolStripMenuItem7.Text = "Ícones grandes";
            toolStripMenuItem7.Click += SelectListViewMode;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(163, 22);
            toolStripMenuItem8.Tag = "2";
            toolStripMenuItem8.Text = "Ícones pequenos";
            toolStripMenuItem8.Click += SelectListViewMode;
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            toolStripMenuItem9.Size = new Size(163, 22);
            toolStripMenuItem9.Tag = "3";
            toolStripMenuItem9.Text = "Lista";
            toolStripMenuItem9.Click += SelectListViewMode;
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            toolStripMenuItem10.Size = new Size(163, 22);
            toolStripMenuItem10.Tag = "1";
            toolStripMenuItem10.Text = "Detalhes";
            toolStripMenuItem10.Click += SelectListViewMode;
            // 
            // toolStripMenuItem11
            // 
            toolStripMenuItem11.Name = "toolStripMenuItem11";
            toolStripMenuItem11.Size = new Size(163, 22);
            toolStripMenuItem11.Tag = "4";
            toolStripMenuItem11.Text = "Blocos";
            toolStripMenuItem11.Click += SelectListViewMode;
            // 
            // itemSelectAll
            // 
            itemSelectAll.Name = "itemSelectAll";
            itemSelectAll.ShortcutKeys = Keys.Control | Keys.A;
            itemSelectAll.Size = new Size(198, 22);
            itemSelectAll.Text = "Selecionar tudo";
            itemSelectAll.Click += SelectAll_Click;
            // 
            // itemApplyPreset
            // 
            itemApplyPreset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            itemApplyPreset.Name = "itemApplyPreset";
            itemApplyPreset.ShortcutKeyDisplayString = "Enter";
            itemApplyPreset.Size = new Size(198, 22);
            itemApplyPreset.Text = "Aplicar";
            itemApplyPreset.Click += ApplyPreset_Click;
            // 
            // sprItem1
            // 
            sprItem1.Name = "sprItem1";
            sprItem1.Size = new Size(195, 6);
            // 
            // itemNewPreset
            // 
            itemNewPreset.Name = "itemNewPreset";
            itemNewPreset.ShortcutKeys = Keys.Control | Keys.N;
            itemNewPreset.Size = new Size(198, 22);
            itemNewPreset.Text = "Nova";
            itemNewPreset.Click += NewPreset_Click;
            // 
            // itemDeletePreset
            // 
            itemDeletePreset.Name = "itemDeletePreset";
            itemDeletePreset.ShortcutKeys = Keys.Delete;
            itemDeletePreset.Size = new Size(198, 22);
            itemDeletePreset.Text = "Excluir";
            itemDeletePreset.Click += DeletePreset_Click;
            // 
            // itemRenamePreset
            // 
            itemRenamePreset.Name = "itemRenamePreset";
            itemRenamePreset.ShortcutKeys = Keys.F2;
            itemRenamePreset.Size = new Size(198, 22);
            itemRenamePreset.Text = "Renomear";
            itemRenamePreset.Click += RenamePreset_Click;
            // 
            // sprItem2
            // 
            sprItem2.Name = "sprItem2";
            sprItem2.Size = new Size(195, 6);
            // 
            // itemHotkey
            // 
            itemHotkey.Name = "itemHotkey";
            itemHotkey.Size = new Size(198, 22);
            itemHotkey.Text = "Atalho de teclado";
            itemHotkey.Click += SetHotkey_Click;
            // 
            // imgSmall
            // 
            imgSmall.ColorDepth = ColorDepth.Depth32Bit;
            imgSmall.ImageSize = new Size(16, 16);
            imgSmall.TransparentColor = Color.Transparent;
            // 
            // imgLarge
            // 
            imgLarge.ColorDepth = ColorDepth.Depth32Bit;
            imgLarge.ImageSize = new Size(32, 32);
            imgLarge.TransparentColor = Color.Transparent;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(btnDeletePreset, 1, 0);
            tableLayoutPanel1.Controls.Add(btnApplyPreset, 2, 0);
            tableLayoutPanel1.Controls.Add(btnNewPreset, 0, 0);
            tableLayoutPanel1.Location = new Point(3, 248);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(255, 23);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // btnDeletePreset
            // 
            btnDeletePreset.Dock = DockStyle.Fill;
            btnDeletePreset.Location = new Point(85, 0);
            btnDeletePreset.Margin = new Padding(0);
            btnDeletePreset.Name = "btnDeletePreset";
            btnDeletePreset.Size = new Size(85, 23);
            btnDeletePreset.TabIndex = 3;
            btnDeletePreset.Text = "Excluir";
            btnDeletePreset.UseVisualStyleBackColor = true;
            btnDeletePreset.Click += DeletePreset_Click;
            // 
            // btnApplyPreset
            // 
            btnApplyPreset.Dock = DockStyle.Fill;
            btnApplyPreset.Location = new Point(170, 0);
            btnApplyPreset.Margin = new Padding(0);
            btnApplyPreset.Name = "btnApplyPreset";
            btnApplyPreset.Size = new Size(85, 23);
            btnApplyPreset.TabIndex = 4;
            btnApplyPreset.Text = "Aplicar";
            btnApplyPreset.UseVisualStyleBackColor = true;
            btnApplyPreset.Click += ApplyPreset_Click;
            // 
            // btnNewPreset
            // 
            btnNewPreset.Dock = DockStyle.Fill;
            btnNewPreset.Location = new Point(0, 0);
            btnNewPreset.Margin = new Padding(0);
            btnNewPreset.Name = "btnNewPreset";
            btnNewPreset.Size = new Size(85, 23);
            btnNewPreset.TabIndex = 2;
            btnNewPreset.Text = "&Nova";
            btnNewPreset.UseVisualStyleBackColor = true;
            btnNewPreset.Click += NewPreset_Click;
            // 
            // opçõesToolStripMenuItem
            // 
            opçõesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { itemReadOnly });
            opçõesToolStripMenuItem.Name = "opçõesToolStripMenuItem";
            opçõesToolStripMenuItem.Size = new Size(198, 22);
            opçõesToolStripMenuItem.Text = "Opções";
            // 
            // itemReadOnly
            // 
            itemReadOnly.CheckOnClick = true;
            itemReadOnly.Name = "itemReadOnly";
            itemReadOnly.Size = new Size(180, 22);
            itemReadOnly.Text = "Somente leitura";
            itemReadOnly.Click += itemReadOnly_Click;
            // 
            // PresetsView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(listPresets);
            Name = "PresetsView";
            Size = new Size(261, 274);
            Tag = "Predefinições";
            ctxItem.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView listPresets;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnNewPreset;
        private Button btnDeletePreset;
        private Button btnApplyPreset;
        private ImageList imgLarge;
        private ToolStripMenuItem salvarTudoToolStripMenuItem;
        private ContextMenuStrip ctxItem;
        private ToolStripMenuItem itemApplyPreset;
        private ToolStripSeparator sprItem1;
        private ToolStripMenuItem itemNewPreset;
        private ToolStripMenuItem itemDeletePreset;
        private ToolStripMenuItem itemRenamePreset;
        private ToolStripSeparator sprItem2;
        private ToolStripMenuItem itemHotkey;
        private ImageList imgSmall;
        private ToolStripMenuItem itemViewMode;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem toolStripMenuItem11;
        private ToolStripMenuItem itemSelectAll;
        private ToolStripMenuItem opçõesToolStripMenuItem;
        private ToolStripMenuItem itemReadOnly;
    }
}
