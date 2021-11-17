import requests

API_KEY = "0f7822807230d53f4377f28275dda1b4"

FINAL_URL = "http://api.openweathermap.org/data/2.5/weather?q=Budapest&units=metric&appid=0f7822807230d53f4377f28275dda1b4"

wx_data = requests.get(FINAL_URL).json()

print(wx_data)
