import json, requests, random, os, re, pyttsx3, pythoncom, unicodedata
from extractOperacaoM import result, isDigit


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

print(filterText("Quanto é 2x20/2"))