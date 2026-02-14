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
            txtCode = new TextBox();
            txtUsername = new TextBox();
            btnBaglan = new Button();
            SuspendLayout();
            // 
            // txtCode
            // 
            txtCode.BackColor = Color.PowderBlue;
            txtCode.Location = new Point(12, 12);
            txtCode.Name = "txtCode";
            txtCode.PlaceholderText = "IP'nin sadece son 2 sayısı";
            txtCode.Size = new Size(143, 23);
            txtCode.TabIndex = 0;
            txtCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.PowderBlue;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Location = new Point(12, 41);
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
            btnBaglan.Location = new Point(12, 70);
            btnBaglan.Name = "btnBaglan";
            btnBaglan.Size = new Size(143, 23);
            btnBaglan.TabIndex = 3;
            btnBaglan.Text = "Bağlan";
            btnBaglan.UseVisualStyleBackColor = false;
            btnBaglan.Click += btnBaglan_Click;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MidnightBlue;
            ClientSize = new Size(166, 104);
            Controls.Add(btnBaglan);
            Controls.Add(txtUsername);
            Controls.Add(txtCode);
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

        private TextBox txtCode;
        private TextBox txtUsername;
        private Button btnBaglan;
    }
}
