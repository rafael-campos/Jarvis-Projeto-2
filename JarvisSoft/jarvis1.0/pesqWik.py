from typing import Text
import wikipedia
import sumy
from sumy.parsers.plaintext import PlaintextParser
from sumy.nlp.tokenizers import Tokenizer
from sumy.summarizers.lex_rank import LexRankSummarizer

def pesqWik(frase):
    try:
        wikipedia.set_lang("pt")
        original_text = wikipedia.summary(frase)[0:150]
        rr = original_text
        
        if "<b>" in rr:
            t = str(rr).replace("<b>", "")
            tt = t.replace("</b>","")
            return tt
            
        return original_text
    except:
        return "Não encontrei nenhuma pesquisa relacionada"