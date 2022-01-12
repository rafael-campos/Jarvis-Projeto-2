from typing import KeysView
from selenium import webdriver
from selenium.webdriver.chrome.options import Options
import time

def send_condtion_to_send_img():
    chrome_options = webdriver.ChromeOptions()
    prefs = {"profile.default_content_setting_values.notifications" : 2}
    chrome_options.add_experimental_option("prefs",prefs)
    chrome_options.add_argument('--headless')

    driver = webdriver.Chrome(executable_path = 'chromedriver.exe',chrome_options=chrome_options)
    driver.get("https://www.facebook.com/messages/t/104748721854941/")

    email_input = driver.find_element_by_id("email")
    password_input = driver.find_element_by_id("pass")
    login_button = driver.find_element_by_id("loginbutton")

    email = "ls7926929@gmail.com"
    passwrd = "deusmaior1275"

    email_input.send_keys(email)
    password_input.send_keys(passwrd)
    login_button.click()
    time.sleep(8)

    pessoa = open("nomePessoa.txt", "r")
    qualpessoa = pessoa.read()

    message = ""
    if qualpessoa == "Familia":
        message = "m2mn"
    
    if qualpessoa == "Mae":
        message = "m2mnm"
    
    if qualpessoa == "Desconhecido":
        message = "m2mne"
        
    message_text_box = driver.find_element_by_css_selector('.notranslate')
    message_text_box.send_keys(message)
    driver.find_element_by_css_selector(".notranslate").send_keys(u'\ue007')
    time.sleep(6)
    driver.quit()
# imagePath = "C:\\inetpub\\wwwroot\\silvio-santos-1607779302.jpg"
# image_input = driver.find_element_by_xpath("//div[@aria-label='Anexar um arquivo']")
# image_input.send_keys(imagePath)
# time.sleep(10)
# driver.find_element_by_css_selector(".uiTextareaAutogrow").send_keys(u'\ue007')

#sen_button.click()