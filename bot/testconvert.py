import subprocess

subprocess.call(['ffmpeg', '-i', 'webproject/mainapp/mp3/audio.mp3',
                   'webproject/mainapp/wav/mp3.wav'])

# import pydub, os
# from pydub import AudioSegment

# mp3_path = 'webproject/mainapp/mp3'
# wav_path = 'webproject/mainapp/wav'

# mp3 = str(mp3_path)
# wav = str(wav_path)
# file=[]
# for files in os.listdir(mp3):
#     with open('C:\\inetpub\\wwwroot\\webproject\\mainapp\\statu.txt', 'a') as f:
#             f.write("entrou files\n")

#     print(file)
#     file.append(files)

# for i in range(len(file)):
#     with open('C:\\inetpub\\wwwroot\\webproject\\mainapp\\statu.txt', 'a') as f:
#         f.write("entrou range> "+file[i]+"\n")

#     mp3_file=AudioSegment.from_file(mp3+'/'+file[i])
#     mp3_file.export(wav + "/" + mp3[1:].split('.')[0].split('/')[-1] + '.wav', format='wav')

#     with open('C:\\inetpub\\wwwroot\\webproject\\mainapp\\statu.txt', 'a') as f:
#         f.write("converteu range\n")

# #pydub.AudioSegment.from_file("C:\\inetpub\\wwwroot\\webproject\\mainapp\\mp3\\audio.mp3").export("C:\\inetpub\\wwwroot\\webproject\\wav\\mp3.wav", format="wav")

# # mp3_file= pydub.AudioSegment.from_file("C:\\inetpub\\wwwroot\\webproject\\mainapp\\mp3\\audio.mp3")
# # mp3_file.export('C:\\inetpub\\wwwroot\\webproject\\wav\\mp3.wav', format='wav')
# # mp3_file.close()


# # def mp3_wav(mp3_path, wav_path):
# #     mp3 = str(mp3_path)
# #     wav = str(wav_path)
# #     file=[]
# #     for files in os.listdir(mp3):
# #         print(file)
# #         file.append(files)
    
# #     for i in range(len(file)):
# #         mp3_file=AudioSegment.from_file(mp3+'/'+file[i])
# #         mp3_file.export(wav + "/" + mp3[1:].split('.')[0].split('/')[-1] + '.wav', format='wav')

# # mp3_path = 'webproject/mainapp/mp3'
# # wav_path = 'webproject/mainapp/wav'
# # mp3_wav(mp3_path, wav_path)
# # file=[]
# # for files in os.listdir(mp3_path):
# #     print(file)
# #     file.append(files)

# # for i in range(len(file)):
# #     mp3_file=AudioSegment.from_file(mp3_path+'/'+file[i])
# #     mp3_file.export(wav_path + "/" + mp3_path[1:].split('.')[0].split('/')[-1] + '.wav', format='wav')
