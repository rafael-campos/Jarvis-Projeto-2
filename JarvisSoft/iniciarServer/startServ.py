import time
from getUrlwemos import getUrlNg
from urllib.request import urlopen
from bs4 import BeautifulSoup
import subprocess

# import subprocess
# subprocess.call("ngrok http 192.168.1.199:8888", shell=True)

print("Url esp: ", getUrlNg())

while True:
    # ?urlo=https://www
    x = getUrlNg()
    #https://e095-3-139-61-96.ngrok.io/b5554efe14d4c89f59869d6c33c8e9410fe94802268b43f807/?hub.verify_token=1144&hub.challenge=OK%20MANO&img=nao&urlo=https://teste.teste
    url1 = "https://e095-3-139-61-96.ngrok.io/b5554efe14d4c89f59869d6c33c8e9410fe94802268b43f807/?hub.verify_token=1144&hub.challenge=OK%20MANO&img=nao&urlo=" + str(x)

    html1 = urlopen(url1)
    soup1 = BeautifulSoup(html1, features="html.parser")

    for script in soup1(["script", "style"]):
        script.extract()    # rip it out

    text1 = soup1.get_text()
    lines1 = (line.strip() for line in text1.splitlines())
    chunks1 = (phrase.strip() for line in lines1 for phrase in line.split("  "))
    text1 = '\n'.join(chunk for chunk in chunks1 if chunk)
    print("Aquiii> ", text1)
    if 'https' in text1:
        print("Tudo certo pode prosseguir> ", text1)
        with open('iniciadoServer.txt', 'w') as f:
            f.write('iniciado')
        print("Server iniciado...")

        time.sleep(2)
        break

#subprocess.call("python refreshServer.py", shell=True)