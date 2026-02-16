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
        private string _lastNotificationSender = "";

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

            // FormChat constructor içine ekle:
            notifyIcon1.Icon = SystemIcons.Information; // Varsayılan sistem ikonu
            notifyIcon1.Visible = true;
            // FormChat constructor içine ekle:
            notifyIcon1.BalloonTipClicked += (s, e) => {
                this.Invoke((MethodInvoker)delegate {
                    // Ana formu göster
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    this.BringToFront();

                    // Eğer son bildirim bir DM'den geldiyse, o DM penceresini de aç
                    if (!string.IsNullOrEmpty(_lastNotificationSender) && _openDMs.ContainsKey(_lastNotificationSender))
                    {
                        var dmForm = _openDMs[_lastNotificationSender];
                        dmForm.Show();
                        dmForm.WindowState = FormWindowState.Normal;
                        dmForm.BringToFront();

                        // Tıklandıktan sonra temizle (aynı bildirimin tekrar tekrar açılmaması için)
                        _lastNotificationSender = "";
                    }
                });
            };
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

            string type = parts[0];
            string senderNick = parts[1];
            string msgContent = parts[2];

            if (type == "GENEL")
            {
                lstMessages.Items.Add($"{senderNick}: {msgContent}");
                lstMessages.TopIndex = lstMessages.Items.Count - 1;

                // Bildirim Göster (Sadece mesaj başkasından geldiyse)
                if (senderNick != _username)
                {
                    ShowNotification("Yeni Genel Mesaj", $"{senderNick}: {msgContent}");
                }
            }
            else if (type == "DM")
            {
                this.Invoke((MethodInvoker)delegate
                {
                    _lastNotificationSender = senderNick; // Bildirime tıklayınca bu kişiyi açacağız

                    if (!_openDMs.ContainsKey(senderNick))
                    {
                        FormDM dmForm = new FormDM(_stream, _username, senderNick);
                        dmForm.FormClosed += (s, args) => _openDMs.Remove(senderNick);
                        _openDMs.Add(senderNick, dmForm);
                    }

                    // Mesaj ekleme mantığı (Handle kontrolü ile beraber)...
                    var targetForm = _openDMs[senderNick];
                    if (targetForm.IsHandleCreated) targetForm.AddMessage(senderNick, msgContent);
                    else targetForm.HandleCreated += (s, e) => targetForm.AddMessage(senderNick, msgContent);

                    ShowNotification($"Yeni Mesaj: {senderNick}", msgContent);
                });
            }
            else if (type == "USERLIST")
            {
                lstUsers.Items.Clear();
                string[] users = msgContent.Split(',');
                foreach (var user in users) if (user != _username) lstUsers.Items.Add(user);
            }
        }

        private string messageShortcut(string msg) => msg.Length > 30 ? msg.Substring(0, 30) + "..." : msg;
        public void ShowNotification(string title, string body)
        {
            notifyIcon1.BalloonTipTitle = title;
            notifyIcon1.BalloonTipText = body;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(3000); // 3 saniye görünür
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCik_Click(object sender, EventArgs e)
        {
            _client.Close();
            this.Close();
        }

        private void FormChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // Kapanmayı durdur
                this.Hide();     // Formu gizle
                ShowNotification("Cat-Chat", "Uygulama arka planda çalışmaya devam ediyor.");
            }
        }
    }
}