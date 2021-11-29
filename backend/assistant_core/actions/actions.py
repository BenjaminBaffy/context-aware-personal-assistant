# See this guide on how to implement these action:
# https://rasa.com/docs/rasa/custom-actions

from typing import Any, Text, Dict, List

from rasa_sdk import Action, Tracker
from rasa_sdk.executor import CollectingDispatcher
from src.wx import wx_city

GROCERY_LIST = []

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

class GroceryAddDisplay(Action):

    def name(self) -> Text:
        return "action_add_gr"

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        groceries = tracker.get_slot('items')
        # if groceries:
        GROCERY_LIST.extend(groceries)
    
        groceries_str = '\n'.join(GROCERY_LIST)
        response = f"Items in your grocery list: \n{groceries_str}"
        
        # else: 
        #     response = "Currently your grocery list is empty. \nAdd something"

        return dispatcher.utter_message(response)


class GroceryDisplay(Action):

    def name(self) -> Text:
        return "action_ls_gr"

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        if GROCERY_LIST:      
            groceries_str = '\n'.join(GROCERY_LIST)
            response = f"Items in your grocery list: \n{groceries_str}"
        else: 
            response = "Currently your grocery list is empty. \nAdd something..."

        return dispatcher.utter_message(response)


class GroceryRemove(Action):

    def name(self) -> Text:
        return "action_rm_gr"

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        groceries = tracker.get_slot('items')

        for element in GROCERY_LIST:
            if element in groceries:
                GROCERY_LIST.remove(element)

        if GROCERY_LIST:
            groceries_str = '\n'.join(GROCERY_LIST)
            response = f"Items in your grocery list: \n{groceries_str}"
        
        else: 
            response = "Currently your grocery list is empty. \nAdd something"

        return dispatcher.utter_message(response)
