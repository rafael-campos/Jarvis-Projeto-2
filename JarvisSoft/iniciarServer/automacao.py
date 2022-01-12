from getUrlwemos import getUrlNg
from urllib.request import urlopen
from bs4 import BeautifulSoup

def apagarLuzQuarto():
    try:
        urlEsp = getUrlNg()
        tokens = ""
        url = urlEsp + "/SmFydmlzODA6MTIzNDU2"
        html1 = urlopen(url.replace(" ", ""))
        soup1 = BeautifulSoup(html1, features="html.parser")

        for script in soup1(["script", "style"]):
            script.extract()    # rip it out

        text1 = soup1.get_text()
        lines1 = (line.strip() for line in text1.splitlines())
        chunks1 = (phrase.strip() for line in lines1 for phrase in line.split("  "))
        text1 = '\n'.join(chunk for chunk in chunks1 if chunk)
        if "GPIO14/D5 - State off" in text1:
            print("JA desligueii: ")
            return "Eu ja havia desligado!"
        else:
            url1 = urlEsp.replace(" ", "") + "/14/off/SmFydmlzODA6MTIzNDU2"
            print(url1)
            html1 = urlopen(url1).read()
            soup = BeautifulSoup(html1, features="html.parser")
            return "Ok desligado"

    except:
        return "Sem conexão com servidor de automação"

def ligarLuzQuarto():
    try:
        urlEsp = getUrlNg()
        
        tokens = ""
        url = urlEsp + "/SmFydmlzODA6MTIzNDU2"
        html1 = urlopen(url.replace(" ", ""))
        soup1 = BeautifulSoup(html1, features="html.parser")

        for script in soup1(["script", "style"]):
            script.extract()    # rip it out

        text1 = soup1.get_text()
        lines1 = (line.strip() for line in text1.splitlines())
        chunks1 = (phrase.strip() for line in lines1 for phrase in line.split("  "))
        text1 = '\n'.join(chunk for chunk in chunks1 if chunk)
        if "GPIO14/D5 - State on" in text1:
            print("JA ligueii: ")
            return "Eu ja havia ligado!"
        else:
            url1 = urlEsp.replace(" ", "") + "/14/on/SmFydmlzODA6MTIzNDU2"
            print(url1)
            html1 = urlopen(url1).read()
            soup = BeautifulSoup(html1, features="html.parser")
            return "Ok ligado"

    except:
        return "Sem conexão com servidor de automação"

print(apagarLuzQuarto())