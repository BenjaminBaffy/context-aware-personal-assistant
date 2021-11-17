# See this guide on how to implement these action:
# https://rasa.com/docs/rasa/custom-actions

from typing import Any, Text, Dict, List

from rasa_sdk import Action, Tracker
from rasa_sdk.executor import CollectingDispatcher
# from rasa_sdk.events import SlotSet
from src.wx import wx_city


class Weather(Action):

    def name(self) -> Text:
        return "action_wx"

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:

        # get the current slot values from rasa
        city = tracker.get_slot('city')
        # wx_type = tracker.get_slot('wx_type')
        # forecast_period = tracker.get_slot('forecast_period')

        # set values for empty slots
        # if forecast_period is None:
        #     forecast_period  = "current"
        # if wx_type is None:
        #     wx_type = "weather"

        # set the forecast steps for OpenWeather forecast json format 
        # drow = 0
        # if forecast_period == "tomorrow":
        #    drow = 1
        
        # list possible cities     
        # possible_cities=["Budapest","Debrecen","Szeged"]

        # Default answer if better answer cannot be found
        # response="Sorry, got no idea - but I hope it's going to be sunny and warm. "

        # read & parse the information and generate response
        if True:
            
            # get the weather information from function defined in wx.py
            open_wx_msg = wx_city(city)

            # set values to current weather variables from open_wx_msg json
            temp=round(open_wx_msg['main']['temp'])
            # pressure=round(open_wx_msg['main']['pressure'])
            # humidity=round(open_wx_msg['main']['humidity'])
            description=(open_wx_msg['weather'][0]["description"])

            response = "The current temperature in {} is {}C. It is {}".format(city, temp, description)

            # # set predicted rainfall to 0 if field missing - OpenWeather API seems to not return it at all if 0
            # try: 
            #     rain_predict=round(open_wx_msg['daily'][drow]['rain'],1)
            # except:
            #     rain_predict=0


            # answers to queries about current weather    
            # if True: 
            #     # generic query
            #     if wx_type=='weather': 
            #         response = "The current temperature in {} is {}C. It is {} and the wind speed is {}m/s".format(city, temp, cond, wind)
            #     # specific information
            #     if wx_type=='wind':
            #         response = "The current wind speed in {} is {} metres per second from {} degrees. ".format(city, wind, wind_deg)
            #     if wx_type=='temperature':
            #         response = "The current temperature in {} is {}degrees Celsius. ".format(city,temp)
            #     if wx_type=='pressure':
            #         response="The current air pressure in {} is {} millibars. ".format(city,pressure)
            #     if wx_type=='humid':
            #         response="The current humidity in {} is {}%. ".format(city,humidity)
            #     if wx_type=='uvi':
            #         response="The current UV index in {} is {}. ".format(city,uvi)
            #     if wx_type=='cloud_conditions':
            #         response="The current conditions in {} is {}.".format(city,cond)


            # answers to forecasts for today and tomorrow   
            # if forecast_period == 'today' or forecast_period == 'tomorrow':
            #     # generic forecast
            #     if wx_type=='weather':
            #         response = "The forecast high for {} {} is {}C. It is expected to be {} and the wind speed is {}m/s".format(city, forecast_period, temp_max_predict, cond_predict, wind_speed_predict)
            #     # more specific forecasts
            #     if wx_type=='wind':
            #         response = "The forecasted wind speed for {} {} is {} metres per second from {} degrees. ".format(city, forecast_period, wind_speed_predict, wind_deg_predict)
            #     if wx_type=='temperature':
            #         response = "The forecasted maximum temperature for {} {} is {}C while the minimum is {}C. ".format(city, forecast_period,temp_max_predict, temp_min_predict)
            #     if wx_type=='pressure':
            #         response="The forecasted air pressure for {} {} is {} millibars. ".format(city, forecast_period,pressure_predict)
            #     if wx_type=='humid':
            #         response="The forecasted humidity for {} {} is {}%. ".format(city, forecast_period,humidity_predict)
            #     if wx_type=='uvi':
            #         response="The forecasted UV index for {} {} is {}. ".format(city, forecast_period,uvi_predict)
            #     if wx_type=='cloud_conditions':
            #         response="The predicted conditions for {} {} is {} and 24hrs rain fall is expected to be {}mm".format(city, forecast_period,cond_predict, rain_predict)
                
        # reponse for if city is not in range of supported cities
        else:
            response = "Sorry, currently I am limited to information from Budapest, Debrecen and Szeged only."


        return dispatcher.utter_message(response)

        # return []
