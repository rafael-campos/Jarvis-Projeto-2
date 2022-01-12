import time
from sendMsFace import send_condtion_to_send_img
from urllib.request import urlopen

from bs4 import BeautifulSoup
from gdrive import send_or_dell


while True:
    duration = ""
    #sendImg\send_img.txt
    sendImg = open("send_img.txt", "r")
    imgSend = sendImg.read()
    
    if imgSend == "sim":
        deleted = send_or_dell("delete")
        time.sleep(3)
        uploaded = send_or_dell("upload")
        time.sleep(2)

        if deleted == "deletado":
            if uploaded == "uploaded":
                move = send_or_dell("mov")
                if move == "movido":
                    link = send_or_dell("link")
                    print("Akiii: " + link)
                    #"https://ebc4-52-67-118-77.ngrok.io/b5554efe14d4c89f59869d6c33c8e9410fe94802268b43f807/?hub.verify_token=1144&hub.challenge=OK%20MANO&urlo=nao&img=" + link

                    url1 = "https://ebc4-52-67-118-77.ngrok.io/b5554efe14d4c89f59869d6c33c8e9410fe94802268b43f807/?hub.verify_token=1144&hub.challenge=OK%20MANO&urlo=nao&img=" + link

                    html1 = urlopen(url1)
                    soup1 = BeautifulSoup(html1, features="html.parser")

                    for script in soup1(["script", "style"]):
                        script.extract()    # rip it out

                    text1 = soup1.get_text()

                    time.sleep(3)

                    send_condtion_to_send_img()

                    with open('send_img.txt', 'w') as f:
                        f.write("nao")