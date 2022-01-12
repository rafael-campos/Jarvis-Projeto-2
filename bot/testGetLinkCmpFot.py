import urllib
import urllib.request
from bs4 import BeautifulSoup

# Fetch URL
url = 'https://drive.google.com/uc?id=1BYIEUxdTKInfW0DKcHpde43wxyqQaBoK'
request = urllib.request.Request(url)
request.add_header('Accept-Encoding', 'utf-8')

# Response has UTF-8 charset header,
# and HTML body which is UTF-8 encoded
response = urllib.request.urlopen(request)

# Parse with BeautifulSoup
soup = BeautifulSoup(response.read().decode('utf-8', 'ignore'))

# Print title attribute of a <div> which uses umlauts (e.g. können)
print(soup)