import json, requests, random, os, re, pyttsx3, pythoncom, unicodedata
from extractOperacaoM import result, isDigit
from googletrans import Translator

def filtNumbrs(frase):
    cont = -1
    x = frase.split(' ')

    for i in x:
        cont += 1

        if "duas" in str(i):
            i.replace("duas", "2")
            x[cont] = "2"
            
        
        if "tres" in str(i):
            i.replace("tres", "3")
            x[cont] = "3"
        
        if "quatro" in str(i):
            i.replace("quatro", "4")
            x[cont] = "4"
        
        if "cinco" in str(i):
            i.replace("cinco", "5")
            x[cont] = "5"
        
        if "seis" in str(i):
            i.replace("seis", "6")
            x[cont] = "6"
        
        if "sete" in str(i):
            i.replace("sete", "7")
            x[cont] = "7"
        
        if "oito" in str(i):
            i.replace("oito", "8")
            x[cont] = "8"
        
        if "nove" in str(i):
            i.replace("nove", "9")
            x[cont] = "9"
        
        if "dez" in str(i):
            i.replace("dez", "10")
            x[cont] = "10"
        
        if "onze" in str(i):
            i.replace("onze", "11")
            x[cont] = "11"
        
        if "doze" in str(i):
            i.replace("doze", "12")
            x[cont] = "12"

        if "treze" in str(i):
            i.replace("treze", "13")
            x[cont] = "13"
        
        if "quatorze" in str(i):
            i.replace("quatorze", "14")
            x[cont] = "14"
        
        if "quinze" in str(i):
            i.replace("quinze", "15")
            x[cont] = "15"
        
        if "dezesseis" in str(i):
            i.replace("dezesseis", "16")
            x[cont] = "16"
        
        if "dezessete" in str(i):
            i.replace("dezessete", "17")
            x[cont] = "17"
        
        if "dezoito" in str(i):
            i.replace("dezoito", "18")
            x[cont] = "18"
        
        if "dezenove" in str(i):
            i.replace("dezenove", "19")
            x[cont] = "19"
        
        if "vinte" in str(i):
            i.replace("vinte", "20")
            x[cont] = "20"        
        
    return " ".join(x)


def text2int(textnum, numwords={}):
    translator = Translator()
    if "duas" in textnum:
        t = textnum.replace("duas", "dois")
        r = translator.translate(t, dest='en').text
        if "twice" in r:
            y = r.replace("twice", "2 times")
        else:
            y = translator.translate(t, dest='en').text
        #print("tem(" + y + ")")
    else:
        y = translator.translate(textnum, dest='en').text

    if not numwords:
        units = [
        "zero", "one", "two", "three", "four", "five", "six", "seven", "eight",
        "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen",
        "sixteen", "seventeen", "eighteen", "nineteen",
        ]

        tens = ["", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"]

        scales = ["hundred", "thousand", "million", "billion", "trillion"]

        numwords["and"] = (1, 0)
        for idx, word in enumerate(units):  numwords[word] = (1, idx)
        for idx, word in enumerate(tens):       numwords[word] = (1, idx * 10)
        for idx, word in enumerate(scales): numwords[word] = (10 ** (idx * 3 or 2), 0)

    ordinal_words = {'first':1, 'second':2, 'third':3, 'fifth':5, 'eighth':8, 'ninth':9, 'twelfth':12}
    ordinal_endings = [('ieth', 'y'), ('th', '')]

    textnum = y.replace('-', ' ')

    current = result = 0
    curstring = ""
    onnumber = False
    for word in textnum.split():
        if word in ordinal_words:
            scale, increment = (1, ordinal_words[word])
            current = current * scale + increment
            if scale > 100:
                result += current
                current = 0
            onnumber = True
        else:
            for ending, replacement in ordinal_endings:
                if word.endswith(ending):
                    word = "%s%s" % (word[:-len(ending)], replacement)

            if word not in numwords:
                if onnumber:
                    curstring += repr(result + current) + " "
                curstring += word + " "
                result = current = 0
                onnumber = False
            else:
                scale, increment = numwords[word]

                current = current * scale + increment
                if scale > 100:
                    result += current
                    current = 0
                onnumber = True

    if onnumber:
        curstring += repr(result + current)

    return  translator.translate(curstring, dest='pt').text

#print(text2int("quanto é duas vezes o 10"))

def strip_accents(s):
    out = re.sub(r'[^\w\s]','',s)
    return ''.join(c for c in unicodedata.normalize('NFD', out.lower())
    if unicodedata.category(c) != 'Mn')

def filterText(text):
    frase = text
    
    

    if isDigit(frase):
        resultCalc = result(frase)
        return resultCalc
    else:
       return "nao e calculo"