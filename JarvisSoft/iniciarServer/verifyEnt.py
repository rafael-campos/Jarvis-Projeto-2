# Imports the Google Cloud client library
from google.cloud import language_v1

from google.oauth2 import service_account

credentials = service_account.Credentials.from_service_account_file("nltkJarvs.json")
scoped_credentials = credentials.with_scopes(["https://www.googleapis.com/auth/cloud-platform"])



def verifyName(text_content):
    client = language_v1.LanguageServiceClient(credentials=credentials)
    type_ = language_v1.Document.Type.PLAIN_TEXT
    language = "pt-BR"
    document = {"content": text_content, "type_": type_, "language": language}
    # Available values: NONE, UTF8, UTF16, UTF32
    encoding_type = language_v1.EncodingType.UTF8

    response = client.analyze_entities(request = {'document': document, 'encoding_type': encoding_type})

    # Loop through entitites returned from the API
    for entity in response.entities:
        tipe = u"Entity type: {}".format(language_v1.Entity.Type(entity.type_).name)
        if "PERSON" in tipe:
            return "PESSOA: ", u"{}".format(entity.name)
        else:
            return "NADA"