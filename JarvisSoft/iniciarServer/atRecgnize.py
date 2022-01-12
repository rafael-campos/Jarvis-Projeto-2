from __future__ import division
import subprocess
from pesqWik import pesqWik
from pesqBing import pesqBin
from filteringText import filterText
import re, sys, pyaudio
from six.moves import queue
# imporações Google Cloud client
from google.cloud import speech
from google.oauth2 import service_account
from verifyEnt import verifyName
from pesqYout import ytSearch
from subprocess import call
import ctypes
# import win32gui, win32con

# the_program_to_hide = win32gui.GetForegroundWindow()
# win32gui.ShowWindow(the_program_to_hide , win32con.SW_HIDE)


# Parâmetros de gravação de áudio
RATE = 16000
CHUNK = int(RATE / 10)  # 100ms






##########################################################################################################

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
    global podeFalar
    num_chars_printed = 0
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
        # Exibe a transcrição da alternativa principal.

        transcript = result.alternatives[0].transcript
        print(transcript)
        if "Jarvis" in transcript or "jarvis" in transcript or "James" in transcript or "Chaves" in transcript or "Jardim" in transcript or "Jarles" in transcript or transcript == "já fiz" or str(transcript).replace(" ", "") == "já" or "jovens" in transcript or "Jade" in transcript:
            with open('podeFalarAg.txt', 'w') as f:
                f.write('sim')

            print("OKKKKKKKKK")

        num_chars_printed = 0



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