import json
import os 

ms2 =""
ms3=""
def getUrlNg(): 
    global ms3
    global ms2
    os.system("curl  http://localhost:4040/api/tunnels > tunnels.json")

    with open('tunnels.json') as data_file:    
        datajson = json.load(data_file)

    # print("Akii: ", datajson)
    msg = ""
    for i in datajson['tunnels']:
        msg = msg + i['public_url'] +' - ' + i['config']['addr'] + ">>>"

        lk = str(msg).split('-')
        for link in lk:
            if "https" in link:
                ms2 = link
        
        mms = ms2.split('>>>')
        for m1 in mms:
            if "https" in m1:
                ms3 = m1
    return ms3
    
print(getUrlNg())