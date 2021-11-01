// define properties that all event subscribers should have
using System;
namespace DangoMimikyu.EventManagement
{
    public class EventWrapper
    {
        public Action<EventArgumentData> eventAction;

        public void InvokeEvent(EventArgumentData eventArgumentData)
        {
            if (eventAction != null)
            {
                eventAction.Invoke(eventArgumentData);
            }
        }

        public void DestroyEvent()
        {
            eventAction = null;
        }
    }
}