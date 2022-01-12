from getUrlwemos import getUrlNg
from urllib.request import urlopen
from bs4 import BeautifulSoup
import subprocess
# http://127.0.0.1:8000/b5554efe14d4c89f59869d6c33c8e9410fe94802268b43f807/?hub.verify_token=1144&hub.challenge=OK%20MANO&urlo=https://
url1 = "http://127.0.0.1:8000/b5554efe14d4c89f59869d6c33c8e9410fe94802268b43f807/?hub.verify_token=1144&hub.challenge=OK%20MANO&urlo=" + str(x)

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