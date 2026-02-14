using System;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CatChat
{
    public partial class FormDM : Form
    {
        private NetworkStream _stream;
        private string _myUsername;
        private string _targetUsername;

        public FormDM(NetworkStream stream, string myUser, string targetUser)
        {
            InitializeComponent();
            _stream = stream;
            _myUsername = myUser;
            _targetUsername = targetUser;
            this.Text = $"{_targetUsername} ile Özel Sohbet";

            txtMsg.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { btnGonder.PerformClick(); e.SuppressKeyPress = true; }
            };
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMsg.Text))
            {
                try
                {
                    // FORMAT: DM|HEDEF|KİMDEN|MESAJ
                    // Örnek: DM|a|b|Merhaba\n
                    string data = $"DM|{_targetUsername}|{txtMsg.Text}\n";
                    byte[] buffer = Encoding.UTF8.GetBytes(data);
                    _stream.Write(buffer, 0, buffer.Length);

                    lstMessages.Items.Add($"Siz: {txtMsg.Text}");
                    txtMsg.Clear();
                }
                catch { MessageBox.Show("Bağlantı hatası!"); }
            }
        }

        public void AddMessage(string sender, string message)
        {
            this.Invoke((MethodInvoker)delegate 
            {
                lstMessages.Items.Add($"{sender}: {message}");
                lstMessages.TopIndex = lstMessages.Items.Count - 1;
            });
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            lstMessages.Items.Clear();
        }
    }
}