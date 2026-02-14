using System;
using System.Net.Sockets;
using System.Text;
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
            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Lütfen tüm alanlarý doldurun!");
                return;
            }

            try
            {
                string ip = "";
                int port = 5000;
                string input = txtCode.Text.Trim();

                // --- SENÝN ÖZEL KODUN (MASTER CODE) ---
                if (input == "HEBELEKEDI-5541") // Burayý istediðin bir þifreyle deðiþtirebilirsin
                {
                    ip = "127.0.0.1"; // Kendi bilgisayarýn olduðu için localhost
                    port = 5000;
                }
                else
                {
                    // Normal kullanýcýlar için Base64 çözme iþlemi
                    string code = input;
                    if (code.StartsWith("CAT-")) code = code.Replace("CAT-", "");

                    byte[] data = Convert.FromBase64String(code);
                    string decodedString = Encoding.UTF8.GetString(data);

                    string[] parts = decodedString.Split(':');
                    ip = parts[0];
                    port = int.Parse(parts[1]);
                }

                // --- BAÐLANTI BAÞLAT ---
                TcpClient client = new TcpClient();
                client.Connect(ip, port);

                FormChat chatForm = new FormChat(client, txtUsername.Text);
                this.Hide();
                chatForm.Show();
                chatForm.FormClosed += (s, args) => { Application.Exit(); };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriþ baþarýsýz: " + ex.Message);
            }
        }
    }
}
