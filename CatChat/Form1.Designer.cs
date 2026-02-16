namespace CatChat
{
    partial class FormLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            txtIP = new TextBox();
            txtPort = new TextBox();
            txtUsername = new TextBox();
            btnBaglan = new Button();
            btnGizGos = new Button();
            SuspendLayout();
            // 
            // txtIP
            // 
            txtIP.BackColor = Color.PowderBlue;
            txtIP.Location = new Point(12, 12);
            txtIP.Name = "txtIP";
            txtIP.PlaceholderText = "IP'nin sadece son 2 sayısı";
            txtIP.Size = new Size(143, 23);
            txtIP.TabIndex = 0;
            txtIP.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPort
            // 
            txtPort.BackColor = Color.PowderBlue;
            txtPort.BorderStyle = BorderStyle.FixedSingle;
            txtPort.Location = new Point(12, 41);
            txtPort.Name = "txtPort";
            txtPort.PasswordChar = '*';
            txtPort.PlaceholderText = "Port";
            txtPort.Size = new Size(68, 23);
            txtPort.TabIndex = 1;
            txtPort.Text = "5000";
            txtPort.TextAlign = HorizontalAlignment.Center;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.PowderBlue;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Location = new Point(12, 70);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Kullanıcı Adı";
            txtUsername.Size = new Size(143, 23);
            txtUsername.TabIndex = 2;
            txtUsername.TextAlign = HorizontalAlignment.Center;
            // 
            // btnBaglan
            // 
            btnBaglan.BackColor = Color.Black;
            btnBaglan.FlatAppearance.BorderSize = 0;
            btnBaglan.FlatStyle = FlatStyle.Flat;
            btnBaglan.Font = new Font("Georgia", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnBaglan.ForeColor = Color.White;
            btnBaglan.Location = new Point(12, 99);
            btnBaglan.Name = "btnBaglan";
            btnBaglan.Size = new Size(143, 23);
            btnBaglan.TabIndex = 3;
            btnBaglan.Text = "Bağlan";
            btnBaglan.UseVisualStyleBackColor = false;
            btnBaglan.Click += btnBaglan_Click;
            // 
            // btnGizGos
            // 
            btnGizGos.BackColor = Color.Red;
            btnGizGos.Cursor = Cursors.IBeam;
            btnGizGos.FlatAppearance.BorderSize = 0;
            btnGizGos.FlatStyle = FlatStyle.Flat;
            btnGizGos.Font = new Font("Georgia", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnGizGos.ForeColor = Color.White;
            btnGizGos.Location = new Point(86, 41);
            btnGizGos.Name = "btnGizGos";
            btnGizGos.Size = new Size(68, 23);
            btnGizGos.TabIndex = 4;
            btnGizGos.Text = "Göster";
            btnGizGos.UseVisualStyleBackColor = false;
            btnGizGos.Click += btnGizGos_Click;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MidnightBlue;
            ClientSize = new Size(166, 133);
            Controls.Add(btnGizGos);
            Controls.Add(btnBaglan);
            Controls.Add(txtUsername);
            Controls.Add(txtPort);
            Controls.Add(txtIP);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Giriş";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtIP;
        private TextBox txtPort;
        private TextBox txtUsername;
        private Button btnBaglan;
        private Button btnGizGos;
    }
}
