using PushNotification.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushNotification.Plugin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.LocalNotifications;

namespace LojaMobile.Helpers
{
    //Class to handle push notifications listens to events such as registration, unregistration, message arrival and errors.
    public class  CrossPushNotificationListener : IPushNotificationListener
    {

        public void OnMessage(JObject values, DeviceType deviceType)
        {
            Debug.WriteLine("Message Arrived");
			CrossLocalNotifications.Current.Show("LojaMobile", $"Nova Mensagem. {values}");
        }

        public void OnRegistered(string token, DeviceType deviceType)
        {
            Debug.WriteLine("Push Notification - Device Registered - Token : {0}", token);
        }

        public void OnUnregistered(DeviceType deviceType)
        {
            Debug.WriteLine("Push Notification - Device Unnregistered");
       
        }

        public void OnError(string message, DeviceType deviceType)
        {
            Debug.WriteLine("Push notification error - {0}", message);
        }

        public bool ShouldShowNotification()
        {
            return true;
        }
    }
}
