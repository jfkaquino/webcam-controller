namespace WebcamController.Forms
{
    partial class HotkeyReader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            linkRemoveHotkey = new LinkLabel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(234, 15);
            label1.TabIndex = 0;
            label1.Text = "Pressione qualquer combinação de teclas...";
            // 
            // label2
            // 
            label2.ImageAlign = ContentAlignment.TopLeft;
            label2.ImageIndex = 0;
            label2.Location = new Point(12, 37);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.No;
            label2.Size = new Size(270, 50);
            label2.TabIndex = 1;
            label2.Text = "Dica: Use uma combinação com várias teclas modificadores para evitar conflitos com atalhos já reservados.";
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(207, 96);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 2;
            btnOk.TabStop = false;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(126, 96);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 3;
            btnCancel.TabStop = false;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // linkRemoveHotkey
            // 
            linkRemoveHotkey.AutoSize = true;
            linkRemoveHotkey.Location = new Point(12, 100);
            linkRemoveHotkey.Name = "linkRemoveHotkey";
            linkRemoveHotkey.Size = new Size(90, 15);
            linkRemoveHotkey.TabIndex = 4;
            linkRemoveHotkey.TabStop = true;
            linkRemoveHotkey.Text = "Remover atalho";
            linkRemoveHotkey.Click += linkRemoveHotkey_Click;
            // 
            // HotkeyReader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(294, 131);
            Controls.Add(linkRemoveHotkey);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HotkeyReader";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Definir atalho de teclado";
            KeyDown += HotkeyReader_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button btnOk;
        private Button btnCancel;
        private LinkLabel linkRemoveHotkey;
    }
}