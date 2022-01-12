from calculadora import Calc
from platform import win32_ver
import re

from urllib.request import Request, urlopen
from bs4 import BeautifulSoup

def isDigit(frase):
    if any(chr.isdigit() for chr in frase):
        return True

nuub = []
def extractNumber(frase):
    return re.findall(r'\d+', frase)


oop = []
operators = ["+", "-", "*"]

text2 = ""
def getOperator(tt):
    if "vezes" in tt:
        h = str(tt).replace("vezes","*")
        ops = re.findall(r'[\+\-*/]', h)
        ops.append('<')
        return ops

    if "x" in tt:
        h = str(tt).replace("x","*")
        ops = re.findall(r'[\+\-*/]', h)
        ops.append('<')
        return ops

    ops = re.findall(r'[\+\-*/]', tt)
    ops.append('<')
    return ops

cont = 0
def extractOperation(frase):
    global cont
    if isDigit(frase):
        operation = ""
        for n, o in zip(extractNumber(frase), getOperator(frase)):
            operation += n + " " + o + " "
        return operation[:-2]

def result(frase):
    op1 = extractOperation(frase)
    return Calc.evaluate(op1)
