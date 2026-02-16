namespace CatChat
{
    partial class FormDM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDM));
            btnTemizle = new Button();
            lstMessages = new ListBox();
            btnGonder = new Button();
            txtMsg = new TextBox();
            SuspendLayout();
            // 
            // btnTemizle
            // 
            btnTemizle.BackColor = Color.FromArgb(192, 192, 255);
            btnTemizle.FlatStyle = FlatStyle.Popup;
            btnTemizle.ForeColor = Color.Black;
            btnTemizle.Location = new Point(345, 383);
            btnTemizle.Name = "btnTemizle";
            btnTemizle.Size = new Size(30, 23);
            btnTemizle.TabIndex = 7;
            btnTemizle.Text = "C";
            btnTemizle.TextAlign = ContentAlignment.BottomCenter;
            btnTemizle.UseVisualStyleBackColor = false;
            btnTemizle.Click += btnTemizle_Click;
            // 
            // lstMessages
            // 
            lstMessages.BackColor = Color.FromArgb(192, 192, 255);
            lstMessages.FormattingEnabled = true;
            lstMessages.ItemHeight = 15;
            lstMessages.Location = new Point(12, 12);
            lstMessages.Name = "lstMessages";
            lstMessages.Size = new Size(363, 394);
            lstMessages.TabIndex = 6;
            // 
            // btnGonder
            // 
            btnGonder.BackColor = Color.FromArgb(192, 192, 255);
            btnGonder.ForeColor = Color.Black;
            btnGonder.Location = new Point(277, 415);
            btnGonder.Name = "btnGonder";
            btnGonder.Size = new Size(100, 23);
            btnGonder.TabIndex = 5;
            btnGonder.Text = "Gönder";
            btnGonder.UseVisualStyleBackColor = false;
            btnGonder.Click += btnGonder_Click;
            // 
            // txtMsg
            // 
            txtMsg.BackColor = Color.FromArgb(192, 192, 255);
            txtMsg.Location = new Point(12, 415);
            txtMsg.Name = "txtMsg";
            txtMsg.Size = new Size(259, 23);
            txtMsg.TabIndex = 4;
            // 
            // FormDM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 64);
            ClientSize = new Size(388, 451);
            Controls.Add(btnTemizle);
            Controls.Add(lstMessages);
            Controls.Add(btnGonder);
            Controls.Add(txtMsg);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormDM";
            Text = "DM";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnTemizle;
        private ListBox lstMessages;
        private Button btnGonder;
        private TextBox txtMsg;
    }
}