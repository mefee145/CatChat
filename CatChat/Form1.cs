using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace CatChat
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnBaglan_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                // IP ve Port bilgisini alýp baðlanýyoruz
                client.Connect("192.168." + txtIP.Text, int.Parse(txtPort.Text));

                // BAÞARILI: Yeni formu oluþtur ve bilgileri içine gönder
                FormChat chatForm = new FormChat(client, txtUsername.Text);

                this.Hide(); // Giriþ ekranýný gizle
                chatForm.ShowDialog(); // Sohbet ekranýný aç
                this.Close(); // Sohbet kapanýrsa uygulamayý tamamen bitir
            }
            catch (Exception ex)
            {
                MessageBox.Show("Baðlantý hatasý: " + ex.Message);
            }
        }

        private void btnGizGos_Click(object sender, EventArgs e)
        {
            if (txtPort.PasswordChar == '*')
            {
                txtPort.PasswordChar = '\0'; 
                btnGizGos.Text = "Gizle";    
            }
            else
            {
                txtPort.PasswordChar = '*';  
                btnGizGos.Text = "Göster";   
            }
        }
    }
}
