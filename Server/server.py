import socket
import threading
import base64
import urllib.request # Dış IP'yi çekmek için gerekli

# Ayarlar
host = '0.0.0.0'
port = 5000

# --- DIŞ IP'Yİ OTOMATİK ÇEKEN FONKSİYON ---
def get_external_ip():
    try:
        # Google üzerinden gerçek internet IP'ni öğrenir
        return urllib.request.urlopen('https://ident.me').read().decode('utf8')
    except:
        return "127.0.0.1" # İnternet yoksa yerel dön

clients = {}  # nickname: socket
banned_ips = set()

def generate_room_code(ip, port):
    data = f"{ip}:{port}"
    encoded_bytes = base64.b64encode(data.encode('utf-8'))
    # Karışıklığı önlemek için sondaki '==' kısımlarını temizleyebiliriz (isteğe bağlı)
    return f"CAT-{encoded_bytes.decode('utf-8')}"

# ... (broadcast_user_list, remove_client ve handle_client fonksiyonları öncekiyle aynı) ...

def broadcast_user_list():
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

def admin_console():
    print("Admin konsolu aktif. /list, /kick <nick>, /ban <nick>, /unban <ip>")
    while True:
        cmd_input = input("")
        if not cmd_input: continue
        parts = cmd_input.split(" ")
        command = parts[0].lower()

        if command == "/list":
            print(f"Aktif Kullanıcılar: {', '.join(clients.keys())}")
        elif command == "/kick" and len(parts) > 1:
            target = parts[1]
            if target in clients:
                clients[target].send("GENEL|SİSTEM|Sunucudan atıldınız!\n".encode('utf-8'))
                clients[target].close()
                print(f"[KICK] {target} atıldı.")
        elif command == "/ban" and len(parts) > 1:
            target = parts[1]
            if target in clients:
                ip = clients[target].getpeername()[0]
                banned_ips.add(ip)
                clients[target].send("GENEL|SİSTEM|BANLANDINIZ!\n".encode('utf-8'))
                clients[target].close()
                print(f"[BAN] {target} ({ip}) yasaklandı.")

def handle_client(client_socket, nickname):
    buffer = ""
    while True:
        try:
            data = client_socket.recv(1024).decode('utf-8')
            if not data: break
            buffer += data
            while '\n' in buffer:
                line, buffer = buffer.split('\n', 1)
                line = line.strip()
                if not line: continue
                parts = line.split('|')
                if len(parts) < 3: continue
                msg_type, target, content = parts[0], parts[1], parts[2]
                if msg_type == "GENEL":
                    res = f"GENEL|{nickname}|{content}\n".encode('utf-8')
                    for c in clients.values(): c.send(res)
                elif msg_type == "DM":
                    if target in clients:
                        res = f"DM|{nickname}|{content}\n".encode('utf-8')
                        clients[target].send(res)
                elif msg_type == "DOSYA":
                    response = f"DOSYA|{nickname}|{content}\n".encode('utf-8')
                    for c in clients.values():
                        try: c.send(response)
                        except: pass

        except: break
    remove_client(nickname)
    client_socket.close()

def receive():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server.bind((host, port))
    server.listen()

    threading.Thread(target=admin_console, daemon=True).start()

    # BURASI DEĞİŞTİ: IP otomatik alınıyor
    current_ip = get_external_ip()
    room_code = generate_room_code(current_ip, port)
    
    print(f"\n" + "="*35)
    print(f" SUNUCU AKTİF!")
    print(f" DIŞ IP: {current_ip}")
    print(f" ODA KODUNUZ: {room_code}")
    print(f"="*35 + "\n")

    while True:
        try:
            client, addr = server.accept()
            if addr[0] in banned_ips:
                client.close()
                continue
            initial_data = client.recv(1024).decode('utf-8')
            if not initial_data: continue
            nickname = initial_data.split('|')[1].split('\n')[0]
            clients[nickname] = client
            print(f"[BAĞLANDI] {nickname}")
            broadcast_user_list()
            threading.Thread(target=handle_client, args=(client, nickname), daemon=True).start()
        except: pass

if __name__ == "__main__":
    receive()