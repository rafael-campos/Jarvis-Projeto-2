import sys
import pyttsx3

def init_engine():
    engine = pyttsx3.init()
    return engine

def say(s):
    voices = engine.getProperty('voices')
    index = -1
    ind = 0

    for voice in voices:
        index += 1
        x9 = f'{index} -- {voice.name}'
        if "IVONA 2 Ricardo" in x9:
            ind = f'{index}'
            break
        else:
            ind = 0
    engine.setProperty('voice', voices[int(ind)].id)
    engine. setProperty("rate", 178)
    #engine.say(s)
    engine.save_to_file(s, 'mane.mp3')
    engine.runAndWait() #blocks

engine = init_engine()
say(str(sys.argv[1]))