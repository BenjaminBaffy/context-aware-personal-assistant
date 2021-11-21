# See this guide on how to implement these action:
# https://rasa.com/docs/rasa/custom-actions

from typing import Any, Text, Dict, List

from rasa_sdk import Action, Tracker
from rasa_sdk.executor import CollectingDispatcher
from src.wx import wx_city


class Weather(Action):

    def name(self) -> Text:
        return "action_wx"

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:

        # get the current slot values from rasa
        city = tracker.get_slot('city')

        # Default answer if better answer cannot be found
        response="Sorry, got no idea - but I hope it's going to be sunny and warm. "
            
        # get the weather information from function defined in wx.py
        open_wx_msg = wx_city(city)

        # set values to current weather variables from open_wx_msg json
        temp=round(open_wx_msg['main']['temp'])
        description=(open_wx_msg['weather'][0]["description"])

        response = "The current temperature in {} is {}C. It is {}".format(city, temp, description)

        return dispatcher.utter_message(response)
