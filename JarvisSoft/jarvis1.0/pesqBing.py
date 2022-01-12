import requests

subscription_key = "530851bafb83443daca3a07f0d946db7"
assert subscription_key

search_url = "https://api.bing.microsoft.com/v7.0/search"


def pesqBin(search_term):
    try:
        headers = {"Ocp-Apim-Subscription-Key": subscription_key}
        params = {"q": search_term, "textDecorations": True, "textFormat": "HTML"}
        response = requests.get(search_url, headers=headers, params=params)
        response.raise_for_status()
        search_results = response.json()
        rr = search_results['webPages']['value'][0]['snippet']
        if "<b>" in rr:
            t = str(rr).replace("<b>", "")
            tt = t.replace("</b>","")
            return tt
        return rr
    except:
        return "Não encontrei nenhuma pesquiza relacionada"