from __future__ import division
import subprocess

from pyasn1.type.univ import Null
from pesqWik import pesqWik
from pesqBing import pesqBin
from filteringText import filtNumbrs, filterText, text2int
import re, sys, pyaudio
from six.moves import queue
# imporações Google Cloud client
from google.cloud import speech
from google.oauth2 import service_account
from verifyEnt import verifyName
from pesqYout import ytSearch
from subprocess import call
import ctypes

# Parâmetros de gravação de áudio
RATE = 16000
CHUNK = int(RATE / 10)  # 100ms

class MicrophoneStream(object):
    """Abre um fluxo de gravação como um gerador que produz os pedaços de áudio."""

    def __init__(self, rate, chunk):
        self._rate = rate
        self._chunk = chunk

        # Crie um buffer thread-safe de dados de áudio
        self._buff = queue.Queue()
        self.closed = True

    def __enter__(self):
        self._audio_interface = pyaudio.PyAudio()
        self._audio_stream = self._audio_interface.open(
            format=pyaudio.paInt16,

            # A API atualmente suporta apenas áudio de 1 canal (mono)
            # https://goo.gl/z757pE
            channels=1,
            rate=self._rate,
            input=True,
            frames_per_buffer=self._chunk,
            

            # Execute o fluxo de áudio de forma assíncrona para preencher o objeto de buffer.
            # Isso é necessário para que o buffer do dispositivo de entrada não
            # transborde enquanto o thread de chamada faz solicitações de rede, etc.
            stream_callback=self._fill_buffer,
        )

        self.closed = False

        return self

    def __exit__(self, type, value, traceback):
        self._audio_stream.stop_stream()
        self._audio_stream.close()
        self.closed = True

        # Sinalize o gerador para encerrar para que o cliente
        # método streaming_recognize não bloqueará o término do processo.
        self._buff.put(None)
        self._audio_interface.terminate()

    def _fill_buffer(self, in_data, frame_count, time_info, status_flags):
        """Colete dados continuamente do fluxo de áudio para o buffer."""
        self._buff.put(in_data)
        return None, pyaudio.paContinue

    def generator(self):
        while not self.closed:
            # Use um get () de bloqueio para garantir que haja pelo menos um pedaço de
            # dados e interromper a iteração se o bloco for Nenhum, indicando o
            # fim do fluxo de áudio.
            chunk = self._buff.get()
            if chunk is None:
                return
            data = [chunk]

            # Agora consuma todos os outros dados ainda armazenados em buffer.
            while True:
                try:
                    chunk = self._buff.get(block=False)
                    if chunk is None:
                        return
                    data.append(chunk)
                except queue.Empty:
                    break

            yield b"".join(data)

#Repete as respostas do servidor e as imprime.
def listen_print_loop(responses):
    cont = 0
    num_chars_printed = 0
    p = Null
    jafalou=""
    for response in responses:
        if not response.results:
            continue        

        # A lista de `resultados` é consecutiva. Para streaming, só nos preocupamos com
        # o primeiro resultado sendo considerado, uma vez que é `is_final`, ele
        # passa a considerar o próximo enunciado.
        result = response.results[0]
        if not result.alternatives:
            continue

        # Exibe a transcrição da alternativa principal.
        transcript = result.alternatives[0].transcript

        # Exibe resultados provisórios, mas com um retorno de carro no final do
        # linha, então as linhas subsequentes irão sobrescrevê-las.
        #
        # Se o resultado anterior for maior do que este, precisamos imprimir
        # alguns espaços extras para sobrescrever o resultado anterior
        overwrite_chars = " " * (num_chars_printed - len(transcript))

        if not result.is_final:
            sys.stdout.write(transcript + overwrite_chars + "\r")
            sys.stdout.flush()
            num_chars_printed = len(transcript)
            cont += 1
            if cont >=40:
                result.is_final = True
                cont = 0
                print(transcript + overwrite_chars)
                #                                 #
                x = filterText(transcript)
                if(x == "nao e calculo"):
                    bugOi = " " + transcript
                    if " oi " in bugOi  or " olá " in bugOi or " ei " in bugOi or   " Oi " in bugOi  or " Olá " in bugOi or " Ei " in bugOi           or "  oi " in bugOi  or "  olá " in bugOi or "  ei " in bugOi or   "  Oi " in bugOi  or "  Olá " in bugOi or "  Ei " in bugOi:
                        CREATE_NO_WINDOW = 0x08000000
                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                        p = subprocess.Popen(["python", "speak.py", "Olá posso ajudar em algo?"], creationflags=subprocess.CREATE_NO_WINDOW)

                    
                    elif "para" in transcript and "falar" in transcript or "pare" in transcript and "falar" in transcript or "cala" in transcript and "boca" in transcript or "cale" in transcript and "boca" in transcript        or "Para" in transcript and "falar" in transcript or "Pare" in transcript and "falar" in transcript or "Cala" in transcript and "boca" in transcript or "Cale" in transcript and "boca" in transcript:
                        p.kill()
                        CREATE_NO_WINDOW = 0x08000000
                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                        p = subprocess.Popen(["python", "speak.py", "Tudo bem, você é quem manda."], creationflags=subprocess.CREATE_NO_WINDOW)
                    
                        
                    elif "apaga" in transcript and "luz" in transcript and "quarto" in transcript or "desliga" in transcript and "luz" in transcript and "quarto" in transcript     or "Apaga" in transcript and "luz" in transcript and "quarto" in transcript or "Desliga" in transcript and "luz" in transcript and "quarto" in transcript:
                        CREATE_NO_WINDOW = 0x08000000
                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                        p = subprocess.Popen(["python", "speak.py", "Ok mano desligando a luz do quarto"], creationflags=subprocess.CREATE_NO_WINDOW)
                    
                    elif "acende" in transcript and "luz" in transcript and "quarto" in transcript or "liga" in transcript and "luz" in transcript and "quarto" in transcript     or "Acende" in transcript and "luz" in transcript and "quarto" in transcript or "Liga" in transcript and "luz" in transcript and "quarto" in transcript:
                        CREATE_NO_WINDOW = 0x08000000
                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                        p = subprocess.Popen(["python", "speak.py", "Ok mano ligando a luz do quarto"], creationflags=subprocess.CREATE_NO_WINDOW)
                    
                    
                    elif "que" in transcript and "horas" in transcript and "são" in transcript     or "Que" in transcript and "horas" in transcript and "são" in transcript:
                        CREATE_NO_WINDOW = 0x08000000
                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                        p = subprocess.Popen(["python", "speak.py", "Não sei as horas"], creationflags=subprocess.CREATE_NO_WINDOW)
                        jafalou="sim"
                    
                    elif "como" in transcript and "está" in transcript and "tempo" in transcript or "previsão" in transcript and "tempo" in transcript      or "Como" in transcript and "está" in transcript and "tempo" in transcript or "Previsão" in transcript and "tempo" in transcript:
                        CREATE_NO_WINDOW = 0x08000000
                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                        p = subprocess.Popen(["python", "speak.py", "Não sei a previsão do tempo"], creationflags=subprocess.CREATE_NO_WINDOW)

                        
                    else:
                        if "como" in transcript and "fritar" in transcript or "como" in transcript and "fazer" in transcript or "como" in transcript and "fazer" in transcript or "como" in transcript and "construir" in transcript or "geito" in transcript and "de" in transcript and "fazer" in transcript or "como" in transcript and "cozinhar" in transcript or "como" in transcript and "consertar" in transcript         or "Como" in transcript and "fritar" in transcript or "Como" in transcript and "fazer" in transcript or "Como" in transcript and "fazer" in transcript or "Como" in transcript and "construir" in transcript or "Geito" in transcript and "de" in transcript and "fazer" in transcript or "Como" in transcript and "cozinhar" in transcript or "Como" in transcript and "consertar" in transcript:
                            CREATE_NO_WINDOW = 0x08000000
                            ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                            p = subprocess.Popen(["python", "speak.py", "Ok abrindo o melhor resultado para. " + transcript], creationflags=subprocess.CREATE_NO_WINDOW)
                            ytSearch(transcript)

                        if "quem construiu" in transcript or "quem fez" in transcript or "como foi construido" in transcript or "quem descobriu" in transcript or "quando foi descoberto" in transcript or "como descobriram" in transcript or "quem inventou" in transcript     or "Quem construiu" in transcript or "Quem fez" in transcript or "Como foi construido" in transcript or "Quem descobriu" in transcript or "Quando foi descoberto" in transcript or "Como descobriram" in transcript or "Quem inventou" in transcript:
                            fras = pesqBin(transcript)
                            CREATE_NO_WINDOW = 0x08000000
                            ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                            p = subprocess.Popen(["python", "speak.py", str(fras)], creationflags=subprocess.CREATE_NO_WINDOW)

                        else:                
                            k = str(verifyName(transcript))
                            if "PESSOA" in k:
                                rk = k.split(',')
                                rk2 = rk[1]
                                rk3 = rk2.replace(')','')
                                rk4 = rk3.replace("'","")
                                if not "Jarvis" in rk4:
                                    if "quem é" in transcript and rk4 in transcript or "quem foi" in transcript and rk4 in transcript or "o que" in transcript and rk4 and "inventou" in transcript       or "Quem é" in transcript and rk4 in transcript or "Quem foi" in transcript and rk4 in transcript or "O que" in transcript and rk4 and "inventou" in transcript:
                                        fras = pesqWik(rk4)
                                        CREATE_NO_WINDOW = 0x08000000
                                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                                        p = subprocess.Popen(["python", "speak.py", fras], creationflags=subprocess.CREATE_NO_WINDOW)
                            
                            # if re.search(r"\b(sair|fechar)\b", transcript, re.I):
                            #     print("Exiting..")
                            #     break
                else:
                    fras = "O resultado é " + str(x)
                    CREATE_NO_WINDOW = 0x08000000
                    ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                    p = subprocess.Popen(["python", "speak.py", fras], creationflags=subprocess.CREATE_NO_WINDOW)
                
                num_chars_printed = 0
                transcript = ""
                responses=""
                response=""
                result=""
                overwrite_chars=""
                
            
        else:
            if jafalou != "sim":
                print(transcript + overwrite_chars)
                #                                 #
                y = text2int(transcript)
                #print("Aqui mano: " + y)
                x =filterText(y)
                

                if(x == "nao e calculo"):
                    bugOi = " " + transcript
                    if " oi " in bugOi  or " olá " in bugOi or " ei " in bugOi or   " Oi " in bugOi  or " Olá " in bugOi or " Ei " in bugOi           or "  oi " in bugOi  or "  olá " in bugOi or "  ei " in bugOi or   "  Oi " in bugOi  or "  Olá " in bugOi or "  Ei " in bugOi:
                        CREATE_NO_WINDOW = 0x08000000
                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                        p = subprocess.Popen(["python", "speak.py", "Olá posso ajudar em algo?"], creationflags=subprocess.CREATE_NO_WINDOW)

                    
                    elif "para" in transcript and "falar" in transcript or "pare" in transcript and "falar" in transcript or "cala" in transcript and "boca" in transcript or "cale" in transcript and "boca" in transcript        or "Para" in transcript and "falar" in transcript or "Pare" in transcript and "falar" in transcript or "Cala" in transcript and "boca" in transcript or "Cale" in transcript and "boca" in transcript:
                        p.kill()
                        CREATE_NO_WINDOW = 0x08000000
                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                        p = subprocess.Popen(["python", "speak.py", "Tudo bem, você é quem manda."], creationflags=subprocess.CREATE_NO_WINDOW)
                    
                        
                    elif "apaga" in transcript and "luz" in transcript and "quarto" in transcript or "desliga" in transcript and "luz" in transcript and "quarto" in transcript     or "Apaga" in transcript and "luz" in transcript and "quarto" in transcript or "Desliga" in transcript and "luz" in transcript and "quarto" in transcript:
                        CREATE_NO_WINDOW = 0x08000000
                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                        p = subprocess.Popen(["python", "speak.py", "Ok mano desligando a luz do quarto"], creationflags=subprocess.CREATE_NO_WINDOW)
                    
                    elif "acende" in transcript and "luz" in transcript and "quarto" in transcript or "liga" in transcript and "luz" in transcript and "quarto" in transcript     or "Acende" in transcript and "luz" in transcript and "quarto" in transcript or "Liga" in transcript and "luz" in transcript and "quarto" in transcript:
                        CREATE_NO_WINDOW = 0x08000000
                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                        p = subprocess.Popen(["python", "speak.py", "Ok mano ligando a luz do quarto"], creationflags=subprocess.CREATE_NO_WINDOW)
                    
                    
                    elif "que" in transcript and "horas" in transcript and "são" in transcript     or "Que" in transcript and "horas" in transcript and "são" in transcript:
                        CREATE_NO_WINDOW = 0x08000000
                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                        p = subprocess.Popen(["python", "speak.py", "Não sei as horas"], creationflags=subprocess.CREATE_NO_WINDOW)

                    
                    elif "como" in transcript and "está" in transcript and "tempo" in transcript or "previsão" in transcript and "tempo" in transcript      or "Como" in transcript and "está" in transcript and "tempo" in transcript or "Previsão" in transcript and "tempo" in transcript:
                        CREATE_NO_WINDOW = 0x08000000
                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                        p = subprocess.Popen(["python", "speak.py", "Não sei a previsão do tempo"], creationflags=subprocess.CREATE_NO_WINDOW)

                        
                    else:
                        if "como" in transcript and "fritar" in transcript or "como" in transcript and "fazer" in transcript or "como" in transcript and "fazer" in transcript or "como" in transcript and "construir" in transcript or "geito" in transcript and "de" in transcript and "fazer" in transcript or "como" in transcript and "cozinhar" in transcript or "como" in transcript and "consertar" in transcript         or "Como" in transcript and "fritar" in transcript or "Como" in transcript and "fazer" in transcript or "Como" in transcript and "fazer" in transcript or "Como" in transcript and "construir" in transcript or "Geito" in transcript and "de" in transcript and "fazer" in transcript or "Como" in transcript and "cozinhar" in transcript or "Como" in transcript and "consertar" in transcript:
                            CREATE_NO_WINDOW = 0x08000000
                            ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                            p = subprocess.Popen(["python", "speak.py", "Ok abrindo o melhor resultado para. " + transcript], creationflags=subprocess.CREATE_NO_WINDOW)
                            ytSearch(transcript)

                        if "quem construiu" in transcript or "quem fez" in transcript or "como foi construido" in transcript or "quem descobriu" in transcript or "quando foi descoberto" in transcript or "como descobriram" in transcript or "quem inventou" in transcript     or "Quem construiu" in transcript or "Quem fez" in transcript or "Como foi construido" in transcript or "Quem descobriu" in transcript or "Quando foi descoberto" in transcript or "Como descobriram" in transcript or "Quem inventou" in transcript:
                            fras = pesqBin(transcript)
                            CREATE_NO_WINDOW = 0x08000000
                            ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                            p = subprocess.Popen(["python", "speak.py", str(fras)], creationflags=subprocess.CREATE_NO_WINDOW)

                        else:                
                            k = str(verifyName(transcript))
                            if "PESSOA" in k:
                                rk = k.split(',')
                                rk2 = rk[1]
                                rk3 = rk2.replace(')','')
                                rk4 = rk3.replace("'","")
                                if not "Jarvis" in rk4:
                                    if "quem é" in transcript and rk4 in transcript or "quem foi" in transcript and rk4 in transcript or "o que" in transcript and rk4 and "inventou" in transcript       or "Quem é" in transcript and rk4 in transcript or "Quem foi" in transcript and rk4 in transcript or "O que" in transcript and rk4 and "inventou" in transcript:
                                        fras = pesqWik(rk4)
                                        CREATE_NO_WINDOW = 0x08000000
                                        ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                                        p = subprocess.Popen(["python", "speak.py", fras], creationflags=subprocess.CREATE_NO_WINDOW)
                            
                            # if re.search(r"\b(sair|fechar)\b", transcript, re.I):
                            #     print("Exiting..")
                            #     break
                else:
                    fras = "O resultado é " + str(x)
                    CREATE_NO_WINDOW = 0x08000000
                    ctypes.windll.kernel32.SetConsoleTitleA("My New Title")
                    p = subprocess.Popen(["python", "speak.py", fras], creationflags=subprocess.CREATE_NO_WINDOW)
                num_chars_printed = 0
            else:
                jafalou = "nao"
                main()

            

#definir credenciais
credentials = service_account.Credentials.from_service_account_file("speechjarvs.json")
scoped_credentials = credentials.with_scopes(["https://www.googleapis.com/auth/cloud-platform"])

def main():
    language_code = "pt-BR"  # a BCP-47 language tag

    client = speech.SpeechClient(credentials=credentials)
    config = speech.RecognitionConfig(
        encoding=speech.RecognitionConfig.AudioEncoding.LINEAR16,
        sample_rate_hertz=RATE,
        language_code=language_code,
        enable_word_time_offsets=True,        
        max_alternatives = 10,
    )

    streaming_config = speech.StreamingRecognitionConfig(
        config=config, interim_results=True
    )

    with MicrophoneStream(RATE, CHUNK) as stream:
        audio_generator = stream.generator()
        requests = (
            speech.StreamingRecognizeRequest(audio_content=content)
            for content in audio_generator
        )

        responses = client.streaming_recognize(streaming_config, requests)

        # Now, put the transcription responses to use.
        listen_print_loop(responses)

if __name__ == "__main__":
    main()