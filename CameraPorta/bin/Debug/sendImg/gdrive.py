# from pydrive.drive import GoogleDrive
# from pydrive.auth import GoogleAuth
   
# # For using listdir()
# import os
   
  
# # Below code does the authentication
# # part of the code
# gauth = GoogleAuth()
  
# # Creates local webserver and auto
# # handles authentication.
# gauth.LocalWebserverAuth()       
# drive = GoogleDrive(gauth)

# # iterating thought all the files/folder
# # of the desired directory

# upload_file_list = ['silvio-santos-1607779302.jpg']
# for upload_file in upload_file_list:
# 	gfile = drive.CreateFile({'parents': [{'id': '1CoeqNhUW78yAvGkiRV9rK0egFOR4Gjxk'}]})
# 	# Read file and set it as the content of this instance.
# 	gfile.SetContentFile(upload_file)
# 	gfile.Upload() # Upload the file.

from __future__ import print_function
import io
import os.path
import time
from googleapiclient.discovery import build
from google_auth_oauthlib.flow import InstalledAppFlow
from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
from googleapiclient.http import MediaFileUpload, MediaIoBaseDownload
import requests
from google_drive_downloader import GoogleDriveDownloader as gdd
import win32gui, win32con

# the_program_to_hide = win32gui.GetForegroundWindow()
# win32gui.ShowWindow(the_program_to_hide , win32con.SW_HIDE)

# If modifying these scopes, delete the file token.json.
SCOPES = ['https://www.googleapis.com/auth/drive']

def credentials():
    creds = None    
    if os.path.exists('token.json'):
        creds = Credentials.from_authorized_user_file('token.json', SCOPES)
    # If there are no (valid) credentials available, let the user log in.
    if not creds or not creds.valid:
        if creds and creds.expired and creds.refresh_token:
            creds.refresh(Request())
        else:
            flow = InstalledAppFlow.from_client_secrets_file(
                'client_secrets.json', SCOPES)
            creds = flow.run_local_server(port=0)
        # Save the credentials for the next run
        with open('token.json', 'w') as token:
            token.write(creds.to_json())

    service = build('drive', 'v3', credentials=creds)
    return service

def move(service):
    results = service.files().list(
        pageSize=10, fields="nextPageToken, files(id, name)").execute()
    items = results.get('files', [])

    if not items:
       return 'Arquivo inexistente'
    else:
        for item in items:
            #https://drive.google.com/uc?id=1BYIEUxdTKInfW0DKcHpde43wxyqQaBoK
            if item['name'] == "photo.jpg":
                #print(item['id'])
                id1 = item['id']

    file_id = id1
    folder_id = '1KaN-_C36XapoC3-lslPugzDruMEAV5Hw'
    # Retrieve the existing parents to remove
    file = service.files().get(fileId=file_id,
                                    fields='parents').execute()
    previous_parents = ",".join(file.get('parents'))
    # Move the file to the new folder
    file = service.files().update(fileId=file_id,
                                        addParents=folder_id,
                                        removeParents=previous_parents,
                                        fields='id, parents').execute()
    
    return "movido"

def upldFot(service):
    
    
    file_metadata = {'name': 'photo.jpg', 'parents': [{"id":"1KaN-_C36XapoC3-lslPugzDruMEAV5Hw"}]}
    media = MediaFileUpload('imgSendGdriv/photo.png', mimetype='image/jpeg')
    file = service.files().create(body=file_metadata,
                                        media_body=media,
                                        fields='id', supportsAllDrives=True).execute()
    print ('File ID Uploaded: %s' % file.get('id'))
    return "uploaded"



def buscFot(service):
    results = service.files().list(
        pageSize=10, fields="nextPageToken, files(id, name)").execute()
    items = results.get('files', [])

    if not items:
       return 'Arquivo inexistente'
    else:
        for item in items:
            #https://drive.google.com/uc?id=1BYIEUxdTKInfW0DKcHpde43wxyqQaBoK
            if item['name'] == "photo.jpg":
                #print(item['id'])
                return "https://drive.google.com/uc?id=" + item['id']

def dowload(service):
    results = service.files().list(
        pageSize=10, fields="nextPageToken, files(id, name)").execute()
    items = results.get('files', [])

    if not items:
       return 'Arquivo inexistente'
    else:
        for item in items:
            #https://drive.google.com/uc?id=1BYIEUxdTKInfW0DKcHpde43wxyqQaBoK
            if item['name'] == "photo.jpg":
                service.permissions().create(body={"role":"reader", "type":"anyone"}, fileId=item['id']).execute()
                time.sleep(2)
                gdd.download_file_from_google_drive(file_id=item['id'],
                    dest_path='imgDow/photo.jpg')
                
                # file_id = item['id']
                # request = service.files().export_media(fileId=file_id,
                #                                             mimeType='image/jpeg')
                # fh = io.BytesIO()
                # downloader = MediaIoBaseDownload(fh, request)
                # done = False
                # while done is False:
                #     status, done = downloader.next_chunk()
                #     print ("Download %d%%." % int(status.progress() * 100))
                                    
                
                return "sucesso"

def deletFot(service):
    results = service.files().list(
        pageSize=10, fields="nextPageToken, files(id, name)").execute()
    items = results.get('files', [])

    if not items:
        return 'Arquivo inexistente'
    else:
        print('Files:')
        for item in items:
            if item['name'] == "photo.jpg":
                service.files().delete(fileId=item['id']).execute()
                print("Delete a foto: " + item['name'] + " id: " + item['id'])
                return "deletado"
            #Sprint(u'{0} ({1})'.format(item['name'], item['id']))

def send_or_dell(cmand):
    service = credentials()

    if cmand == "upload":
        return upldFot(service) #return "uploaded"

    if cmand == "delete":
        return deletFot(service) #return "deletado" or return 'Arquivo inexistente'
            

    if cmand == "link":
        return buscFot(service) #return link or return 'Arquivo inexistente'

    if cmand == "dowload":
        return dowload(service)

    if cmand == "mov":
       return move(service)

#print(send_or_dell("delete"))
#print(send_or_dell("upload"))
#print(send_or_dell("mov"))
#print(send_or_dell("link"))

