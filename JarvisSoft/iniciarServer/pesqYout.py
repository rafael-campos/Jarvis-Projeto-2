# -*- coding: utf-8 -*-
import webbrowser

from googleapiclient.discovery import build
youTubeApiKei = "AIzaSyCOYl38VwiUHjxs1-gmyM4R8m96mFXmjFM"
youtube = build('youtube', 'v3', developerKey=youTubeApiKei)

def ytSearch(frase):
    try:
        res = youtube.search().list(part='snippet',q=frase ,type='playlist').execute()

        playListId = res['items'][0]['id']['playlistId']
        nextPage_token = None

        videoidd = youtube.playlistItems().list(part='snippet', playlistId=playListId, maxResults=4).execute()
        idVd = videoidd['items'][0]['snippet']['resourceId']['videoId']
        #https://www.youtube.com/watch?v= &list=PLN_30RIYFb80mITw5GZAx-XmxSBlxwWi5&index=3
        linkYtt = "https://www.youtube.com/watch?v=" + str(idVd) + "&list=" + str(playListId)

        webbrowser.open(linkYtt)

        print(linkYtt)

        return "Abrindo primeiro resultado no youtube"
    except:
        return "Não encontrei nenhuma pesquisa relacionada"

#print(ytSearch("como fazer bolo"))