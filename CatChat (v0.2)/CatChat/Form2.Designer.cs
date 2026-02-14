namespace CatChat
{
    partial class FormChat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChat));
            txtMsg = new TextBox();
            btnGonder = new Button();
            lstMessages = new ListBox();
            btnTemizle = new Button();
            lstUsers = new ListBox();
            btnCik = new Button();
            SuspendLayout();
            // 
            // txtMsg
            // 
            txtMsg.Location = new Point(12, 415);
            txtMsg.Name = "txtMsg";
            txtMsg.PlaceholderText = "Mesajınızı Buraya Girin . . .";
            txtMsg.Size = new Size(259, 23);
            txtMsg.TabIndex = 0;
            // 
            // btnGonder
            // 
            btnGonder.BackColor = Color.White;
            btnGonder.ForeColor = Color.Black;
            btnGonder.Location = new Point(277, 415);
            btnGonder.Name = "btnGonder";
            btnGonder.Size = new Size(100, 23);
            btnGonder.TabIndex = 1;
            btnGonder.Text = "Gönder";
            btnGonder.UseVisualStyleBackColor = false;
            btnGonder.Click += btnGonder_Click;
            // 
            // lstMessages
            // 
            lstMessages.FormattingEnabled = true;
            lstMessages.ItemHeight = 15;
            lstMessages.Location = new Point(12, 12);
            lstMessages.Name = "lstMessages";
            lstMessages.Size = new Size(363, 394);
            lstMessages.TabIndex = 2;
            // 
            // btnTemizle
            // 
            btnTemizle.BackColor = Color.White;
            btnTemizle.ForeColor = Color.Black;
            btnTemizle.Location = new Point(345, 383);
            btnTemizle.Name = "btnTemizle";
            btnTemizle.Size = new Size(30, 23);
            btnTemizle.TabIndex = 3;
            btnTemizle.Text = "C";
            btnTemizle.TextAlign = ContentAlignment.BottomCenter;
            btnTemizle.UseVisualStyleBackColor = false;
            btnTemizle.Click += btnTemizle_Click;
            // 
            // lstUsers
            // 
            lstUsers.FormattingEnabled = true;
            lstUsers.ItemHeight = 15;
            lstUsers.Location = new Point(381, 12);
            lstUsers.Name = "lstUsers";
            lstUsers.Size = new Size(242, 394);
            lstUsers.TabIndex = 4;
            lstUsers.SelectedIndexChanged += lstUsers_SelectedIndexChanged;
            lstUsers.MouseDoubleClick += lstUsers_MouseDoubleClick;
            // 
            // btnCik
            // 
            btnCik.BackColor = Color.Red;
            btnCik.FlatAppearance.BorderSize = 0;
            btnCik.FlatStyle = FlatStyle.Flat;
            btnCik.ForeColor = Color.Transparent;
            btnCik.Location = new Point(383, 415);
            btnCik.Name = "btnCik";
            btnCik.Size = new Size(240, 23);
            btnCik.TabIndex = 5;
            btnCik.Text = "Çık";
            btnCik.UseVisualStyleBackColor = false;
            btnCik.Click += btnCik_Click;
            // 
            // FormChat
            // 
            AcceptButton = btnGonder;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(630, 450);
            Controls.Add(btnCik);
            Controls.Add(lstUsers);
            Controls.Add(btnTemizle);
            Controls.Add(lstMessages);
            Controls.Add(btnGonder);
            Controls.Add(txtMsg);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormChat";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CatChat2";
            Load += FormChat_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtMsg;
        private Button btnGonder;
        private ListBox lstMessages;
        private Button btnTemizle;
        private ListBox lstUsers;
        private Button btnCik;
    }
}