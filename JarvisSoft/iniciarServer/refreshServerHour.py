from getUrlwemos import getUrlNg
import sched, time
from urllib.request import urlopen
from bs4 import BeautifulSoup


print("Url esp: ", getUrlNg())
x = getUrlNg()
url1 = "https://www.jarvisservice.net/b5554efe14d4c89f59869d6c33c8e9410fe94802268b43f807/?hub.verify_token=1144&hub.challenge=OK%20MANO&urlo=" + str(x)

html1 = urlopen(url1)
soup1 = BeautifulSoup(html1, features="html.parser")

for script in soup1(["script", "style"]):
    script.extract()    # rip it out

text1 = soup1.get_text()
lines1 = (line.strip() for line in text1.splitlines())
chunks1 = (phrase.strip() for line in lines1 for phrase in line.split("  "))
text1 = '\n'.join(chunk for chunk in chunks1 if chunk)
if "https" in text1:
    print("Tudo certo pode prosseguir")

s = sched.scheduler(time.time, time.sleep)
def do_something(sc): 
    x = getUrlNg()
    url1 = "https://www.jarvisservice.net/b5554efe14d4c89f59869d6c33c8e9410fe94802268b43f807/?hub.verify_token=1144&hub.challenge=OK%20MANO&urlo=" + str(x)

    html1 = urlopen(url1)
    soup1 = BeautifulSoup(html1, features="html.parser")

    for script in soup1(["script", "style"]):
        script.extract()    # rip it out

    text1 = soup1.get_text()
    lines1 = (line.strip() for line in text1.splitlines())
    chunks1 = (phrase.strip() for line in lines1 for phrase in line.split("  "))
    text1 = '\n'.join(chunk for chunk in chunks1 if chunk)
    if "https" in text1:
        print("Tudo certo pode prosseguir")

    s.enter(2400, 1, do_something, (sc,))

s.enter(2400, 1, do_something, (s,))
s.run()