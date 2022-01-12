from google.cloud import speech_v1p1beta1 as speech
from google.oauth2 import service_account

#definir credenciais
credentials = service_account.Credentials.from_service_account_file("speechjarvs.json")
scoped_credentials = credentials.with_scopes(["https://www.googleapis.com/auth/cloud-platform"])

client = speech.SpeechClient(credentials=credentials)

speech_file = "resources/commercial_mono.wav"

with open(speech_file, "rb") as audio_file:
    content = audio_file.read()

audio = speech.RecognitionAudio(content=content)

config = speech.RecognitionConfig(
    encoding=speech.RecognitionConfig.AudioEncoding.LINEAR16,
    sample_rate_hertz=8000,
    language_code="en-US",
    enable_speaker_diarization=True,
    diarization_speaker_count=2,
)

print("Waiting for operation to complete...")
response = client.recognize(config=config, audio=audio)

# The transcript within each result is separate and sequential per result.
# However, the words list within an alternative includes all the words
# from all the results thus far. Thus, to get all the words with speaker
# tags, you only have to take the words list from the last result:
result = response.results[-1]

words_info = result.alternatives[0].words

# Printing out the output:
for word_info in words_info:
    print(
        u"word: '{}', speaker_tag: {}".format(word_info.word, word_info.speaker_tag)
    )