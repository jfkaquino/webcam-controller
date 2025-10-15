namespace WebcamController.Views
{
    partial class CameraView
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
            tableLayoutPanel1 = new TableLayoutPanel();
            grpZoom = new GroupBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            trkZoom = new TrackBar();
            numZoom = new NumericUpDown();
            grpTilt = new GroupBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            trkTilt = new TrackBar();
            numTilt = new NumericUpDown();
            grpPan = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            trkPan = new TrackBar();
            numPan = new NumericUpDown();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            cmbDevices = new ComboBox();
            btnRestoreDefault = new Button();
            linkAdvancedProperties = new LinkLabel();
            tableLayoutPanel1.SuspendLayout();
            grpZoom.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkZoom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numZoom).BeginInit();
            grpTilt.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkTilt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTilt).BeginInit();
            grpPan.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkPan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPan).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(grpZoom, 0, 2);
            tableLayoutPanel1.Controls.Add(grpTilt, 0, 1);
            tableLayoutPanel1.Controls.Add(grpPan, 0, 0);
            tableLayoutPanel1.Location = new Point(3, 29);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(374, 280);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // grpZoom
            // 
            grpZoom.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            grpZoom.Controls.Add(tableLayoutPanel5);
            grpZoom.Location = new Point(0, 191);
            grpZoom.Margin = new Padding(0);
            grpZoom.Name = "grpZoom";
            grpZoom.Size = new Size(374, 83);
            grpZoom.TabIndex = 2;
            grpZoom.TabStop = false;
            grpZoom.Text = "Zoom";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.Controls.Add(trkZoom, 0, 0);
            tableLayoutPanel5.Controls.Add(numZoom, 1, 0);
            tableLayoutPanel5.Location = new Point(3, 19);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(368, 61);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // trkZoom
            // 
            trkZoom.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            trkZoom.Location = new Point(3, 8);
            trkZoom.Name = "trkZoom";
            trkZoom.Size = new Size(308, 45);
            trkZoom.TabIndex = 0;
            trkZoom.TickStyle = TickStyle.Both;
            // 
            // numZoom
            // 
            numZoom.Anchor = AnchorStyles.None;
            numZoom.Location = new Point(317, 19);
            numZoom.Name = "numZoom";
            numZoom.Size = new Size(48, 23);
            numZoom.TabIndex = 1;
            // 
            // grpTilt
            // 
            grpTilt.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            grpTilt.Controls.Add(tableLayoutPanel4);
            grpTilt.Location = new Point(0, 98);
            grpTilt.Margin = new Padding(0);
            grpTilt.Name = "grpTilt";
            grpTilt.Size = new Size(374, 83);
            grpTilt.TabIndex = 1;
            grpTilt.TabStop = false;
            grpTilt.Text = "Tilt";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.Controls.Add(trkTilt, 0, 0);
            tableLayoutPanel4.Controls.Add(numTilt, 1, 0);
            tableLayoutPanel4.Location = new Point(3, 19);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(368, 61);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // trkTilt
            // 
            trkTilt.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            trkTilt.Location = new Point(3, 8);
            trkTilt.Name = "trkTilt";
            trkTilt.Size = new Size(308, 45);
            trkTilt.TabIndex = 0;
            trkTilt.TickStyle = TickStyle.Both;
            // 
            // numTilt
            // 
            numTilt.Anchor = AnchorStyles.None;
            numTilt.Location = new Point(317, 19);
            numTilt.Name = "numTilt";
            numTilt.Size = new Size(48, 23);
            numTilt.TabIndex = 1;
            // 
            // grpPan
            // 
            grpPan.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            grpPan.Controls.Add(tableLayoutPanel3);
            grpPan.Location = new Point(0, 5);
            grpPan.Margin = new Padding(0);
            grpPan.Name = "grpPan";
            grpPan.Size = new Size(374, 83);
            grpPan.TabIndex = 0;
            grpPan.TabStop = false;
            grpPan.Text = "Pan";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.Controls.Add(trkPan, 0, 0);
            tableLayoutPanel3.Controls.Add(numPan, 1, 0);
            tableLayoutPanel3.Location = new Point(3, 19);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(368, 61);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // trkPan
            // 
            trkPan.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            trkPan.Location = new Point(3, 8);
            trkPan.Name = "trkPan";
            trkPan.Size = new Size(308, 45);
            trkPan.TabIndex = 0;
            trkPan.TickStyle = TickStyle.Both;
            // 
            // numPan
            // 
            numPan.Anchor = AnchorStyles.None;
            numPan.Location = new Point(317, 19);
            numPan.Name = "numPan";
            numPan.Size = new Size(48, 23);
            numPan.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(cmbDevices, 1, 0);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(374, 23);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(0, 4);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(48, 15);
            label1.TabIndex = 0;
            label1.Text = "Câmera";
            // 
            // cmbDevices
            // 
            cmbDevices.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbDevices.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDevices.FormattingEnabled = true;
            cmbDevices.Location = new Point(48, 0);
            cmbDevices.Margin = new Padding(0);
            cmbDevices.Name = "cmbDevices";
            cmbDevices.Size = new Size(326, 23);
            cmbDevices.TabIndex = 1;
            cmbDevices.SelectedIndexChanged += cmbDevices_SelectedIndexChanged;
            // 
            // btnRestoreDefault
            // 
            btnRestoreDefault.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRestoreDefault.Location = new Point(262, 315);
            btnRestoreDefault.Name = "btnRestoreDefault";
            btnRestoreDefault.Size = new Size(115, 23);
            btnRestoreDefault.TabIndex = 2;
            btnRestoreDefault.Text = "Restaurar padrão";
            btnRestoreDefault.UseVisualStyleBackColor = true;
            // 
            // linkAdvancedProperties
            // 
            linkAdvancedProperties.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            linkAdvancedProperties.AutoSize = true;
            linkAdvancedProperties.Location = new Point(3, 319);
            linkAdvancedProperties.Name = "linkAdvancedProperties";
            linkAdvancedProperties.Size = new Size(134, 15);
            linkAdvancedProperties.TabIndex = 3;
            linkAdvancedProperties.TabStop = true;
            linkAdvancedProperties.Text = "Propriedades avançadas";
            linkAdvancedProperties.Click += linkAdvancedProperties_Click;
            // 
            // CameraView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(linkAdvancedProperties);
            Controls.Add(btnRestoreDefault);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Name = "CameraView";
            Size = new Size(380, 341);
            Load += CameraView_Load;
            tableLayoutPanel1.ResumeLayout(false);
            grpZoom.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkZoom).EndInit();
            ((System.ComponentModel.ISupportInitialize)numZoom).EndInit();
            grpTilt.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkTilt).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTilt).EndInit();
            grpPan.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkPan).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPan).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private ComboBox cmbDevices;
        private GroupBox grpPan;
        private TableLayoutPanel tableLayoutPanel3;
        private TrackBar trkPan;
        private NumericUpDown numPan;
        private GroupBox grpZoom;
        private TableLayoutPanel tableLayoutPanel5;
        private TrackBar trkZoom;
        private NumericUpDown numZoom;
        private GroupBox grpTilt;
        private TableLayoutPanel tableLayoutPanel4;
        private TrackBar trkTilt;
        private NumericUpDown numTilt;
        private Button btnRestoreDefault;
        private LinkLabel linkAdvancedProperties;
    }
}
