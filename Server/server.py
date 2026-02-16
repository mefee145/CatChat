import socket
import threading

# Ayarlar
host = '0.0.0.0'
port = 5000

clients = {}  # nickname: socket
banned_ips = set() # Banlanan IP listesi

def broadcast_user_list():
    """Aktif kullanıcı listesini tüm istemcilere gönderir."""
    user_list_str = ",".join(clients.keys())
    packet = f"USERLIST|SERVER|{user_list_str}\n".encode('utf-8')
    for c in clients.values():
        try: c.send(packet)
        except: pass

def remove_client(nickname):
    if nickname in clients:
        print(f"[AYRILDI] {nickname}")
        del clients[nickname]
        broadcast_user_list()

# --- ADMIN KONSOLU ---
def admin_console():
    print("Admin Konsolu Aktif. Komutlar: /list, /kick <nick>, /ban <nick>, /unban <ip>")
    while True:
        cmd = input("").strip()
        if not cmd: continue
        parts = cmd.split(" ")
        action = parts[0].lower()

        if action == "/list":
            print(f"Aktif Kullanıcılar: {', '.join(clients.keys())}")
        elif action == "/kick" and len(parts) > 1:
            target = parts[1]
            if target in clients:
                clients[target].close()
                print(f"[KICK] {target} sunucudan atıldı.")
        elif action == "/ban" and len(parts) > 1:
            target = parts[1]
            if target in clients:
                ip = clients[target].getpeername()[0]
                banned_ips.add(ip)
                clients[target].close()
                print(f"[BAN] {target} ({ip}) yasaklandı.")
        elif action == "/unban" and len(parts) > 1:
            ip = parts[1]
            if ip in banned_ips:
                banned_ips.remove(ip)
                print(f"[UNBAN] {ip} engeli kaldırıldı.")

def handle_client(client_socket, nickname):
    buffer = ""
    while True:
        try:
            # Ses verisi için buffer 4096 yapıldı
            data = client_socket.recv(4096).decode('utf-8')
            if not data: break
            
            buffer += data
            while '\n' in buffer:
                line, buffer = buffer.split('\n', 1)
                line = line.strip()
                if not line: continue
                
                parts = line.split('|')
                if len(parts) < 3: continue
                
                msg_type, target, content = parts[0], parts[1], parts[2]

                # SES YÖNLENDİRME
                if msg_type == "SES":
                    if target in clients:
                        response = f"SES|{nickname}|{content}\n".encode('utf-8')
                        clients[target].send(response)
                
                # GENEL MESAJ
                elif msg_type == "GENEL":
                    response = f"GENEL|{nickname}|{content}\n".encode('utf-8')
                    for c in clients.values():
                        try: c.send(response)
                        except: pass
                
                # ÖZEL MESAJ (DM)
                elif msg_type == "DM":
                    if target in clients:
                        response = f"DM|{nickname}|{content}\n".encode('utf-8')
                        clients[target].send(response)
        except:
            break
    
    remove_client(nickname)
    client_socket.close()

def receive():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server.bind((host, port))
    server.listen()

    threading.Thread(target=admin_console, daemon=True).start()

    print(f"\n" + "="*30)
    print(f" CAT-CHAT SERVER AKTİF")
    print(f" PORT: {port}")
    print(f" IP: {socket.gethostbyname(socket.gethostname())} (Yerel)")
    print(f"="*30 + "\n")

    while True:
        try:
            client, addr = server.accept()
            if addr[0] in banned_ips:
                client.close()
                continue
                
            initial_data = client.recv(1024).decode('utf-8')
            if not initial_data: continue
            
            # İlk paket formatı: GENEL|Nickname|mesaj
            parts = initial_data.split('|')
            if len(parts) >= 2:
                nickname = parts[1]
                clients[nickname] = client
                print(f"[BAĞLANDI] {nickname}")
                broadcast_user_list()
                threading.Thread(target=handle_client, args=(client, nickname), daemon=True).start()
        except:
            pass

if __name__ == "__main__":
    receive()