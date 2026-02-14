using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CatChat
{
    public partial class FormChat : Form
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private string _username;
        private Thread? _receiveThread;
        private bool _isLoaded = false;
        private Dictionary<string, FormDM> _openDMs = new Dictionary<string, FormDM>();

        public FormChat(TcpClient client, string username)
        {
            InitializeComponent();
            this._client = client;
            this._username = username;
            this._stream = _client.GetStream();

            txtMsg.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { btnGonder.PerformClick(); e.SuppressKeyPress = true; }
            };
            lstUsers.MouseDoubleClick += lstUsers_MouseDoubleClick;
        }

        private void txtMsg_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGonder.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMsg.Text))
            {
                SendMessage($"GENEL|{_username}|{txtMsg.Text}\n");
                txtMsg.Clear();
            }
        }

        private void SendMessage(string message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                _stream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                MessageBox.Show("Mesaj gönderilemedi.");
            }
        }

        private void ListenForMessages()
        {
            try
            {
                // StreamReader paketleri satır satır ayırır
                StreamReader reader = new StreamReader(_stream, Encoding.UTF8);
                while (_client.Connected)
                {
                    string? line = reader.ReadLine();
                    if (line == null) break;

                    this.Invoke((MethodInvoker)delegate
                    {
                        ParseIncomingData(line);
                    });
                }
            }
            catch (Exception ex)
            {
                if (_client.Connected)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("Bağlantı Hatası: " + ex.Message);
                        this.Close();
                    });
                }
            }
        }

        private void FormChat_Load(object sender, EventArgs e)
        {
            if (_isLoaded) return;
            _isLoaded = true;

            // ÖNEMLİ: \n eklendi
            SendMessage($"GENEL|{_username}|sohbete katıldı!\n");

            _receiveThread = new Thread(ListenForMessages);
            _receiveThread.IsBackground = true;
            _receiveThread.Start();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            lstMessages.Items.Clear();
        }

        private void lstUsers_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                string target = lstUsers.SelectedItem.ToString()!;
                if (target == _username) return;
                OpenDMPanel(target);
            }
        }

        private void OpenDMPanel(string targetUser)
        {
            if (!_openDMs.ContainsKey(targetUser))
            {
                FormDM dmForm = new FormDM(_stream, _username, targetUser);
                dmForm.FormClosed += (s, args) => _openDMs.Remove(targetUser);
                _openDMs.Add(targetUser, dmForm);
                dmForm.Show();
            }
            else { _openDMs[targetUser].BringToFront(); }
        }

        private void ParseIncomingData(string data)
        {
            string[] parts = data.Split('|');
            if (parts.Length < 3) return;

            string type = parts[0];     // DM veya GENEL
            string senderNick = parts[1]; // Mesajı gönderen kişi
            string msgContent = parts[2]; // Mesajın asıl içeriği

            if (type == "GENEL")
            {
                lstMessages.Items.Add($"{senderNick}: {msgContent}");
                lstMessages.TopIndex = lstMessages.Items.Count - 1;
            }
            else if (type == "DM")
            {
                // Gelen pakette parts[1] gönderen kişidir. 
                // Eğer o kişiden pencere gelmişse içeriği parts[2] olarak bas.
                if (!_openDMs.ContainsKey(senderNick))
                {
                    OpenDMPanel(senderNick);
                }
                _openDMs[senderNick].AddMessage(senderNick, msgContent);
            }
            else if (type == "USERLIST")
            {
                lstUsers.Items.Clear();
                string[] users = msgContent.Split(',');
                foreach (var user in users) if (user != _username) lstUsers.Items.Add(user);
            }
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCik_Click(object sender, EventArgs e)
        {
            _client.Close();
            this.Close();
        }
    }
}