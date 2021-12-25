using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DangoMimikyu.EventManagement
{
	public class EventManager : MonoBehaviour
	{
		public static EventManager instance { private set; get; }

		private Dictionary<GameEvents, EventWrapper> m_EventnameToEvent = new Dictionary<GameEvents, EventWrapper>(); // dictionary of <eventName, ActualEventAction>

		#region Monobehaviour functions
		private void Awake()
		{
			if (!instance)
			{
				Debug.Log("created this instance of EventManager");
				instance = this;
			}
			else
			{
				Debug.LogWarning("Existing EventManager already exist but you're trying to make a new one. Will destroy the old one");
				Destroy(instance);
				instance = this;
			}
		}
		#endregion

		#region Subscription functions
		public void StartListening(GameEvents eventName, Action<EventArgumentData> action)
		{
			EventWrapper wrapper;
			if (!m_EventnameToEvent.TryGetValue(eventName, out wrapper)) // check if there is an existing wrapper of this name to make a new one if needed
			{
				wrapper = new EventWrapper();                   // make a new event of this type
				m_EventnameToEvent.Add(eventName, wrapper);     // add this new event to the dictionary
				Debug.LogWarning(action.Method.Name + "started listening to " + eventName);
			}
			wrapper.eventAction += action;
		}

		public void StopListening(GameEvents eventName, Action<EventArgumentData> action)
		{
			EventWrapper wrapper;
			if (m_EventnameToEvent.TryGetValue(eventName, out wrapper))
			{
				wrapper.eventAction -= action;
			}
		}
		#endregion

		#region Event handling functions
		public void DispatchEvent(GameEvents eventName, params object[] arguments)
		{
			EventWrapper wrapper;
			if (m_EventnameToEvent.TryGetValue(eventName, out wrapper))
			{
				EventArgumentData eventData;
				eventData = new EventArgumentData(eventName, arguments); // don't need a null check because if the invoke isn't expecting arguments, having arguments doesn't do anything

				//Debug.Log("dispatching event");
				// invoke the event
				wrapper.InvokeEvent(eventData);
			}
			else
			{
				Debug.LogWarning("trying to dispatch an event that nobody is listening to yet");
			}
		}
		#endregion
	}
}