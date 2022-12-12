using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace TigerFrogGames
{
    public class TimeEventScheduler: MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventChannelTimeSpan onTimeChange;

        [SerializeField] private List<TimedEvent> schedualedEvents;

        [Serializable]
        private class TimedEvent
        {
            public bool repeating = false;
            public int day = -1;
            public int hour;
            public int minutes;
            public UnityEvent eventActions;
            
        }
        
        
        
        #endregion

        #region Unity Methods

        private void Start()
        {
            onTimeChange.OnEvent += OnTimeChangeOnOnEvent;
        }
        
        private void OnDestroy()
        {
            onTimeChange.OnEvent -= OnTimeChangeOnOnEvent;
        }

        #endregion

        #region Methods

        private void OnTimeChangeOnOnEvent(TimeSpan newTime)
        {
            var schedule = schedualedEvents.FindAll( 
                    s => 
                        (newTime.Days == s.day || s.day == -1 ) && 
                        newTime.Hours == s.hour &&
                        newTime.Minutes == s.minutes);

            if (schedule.Count != 0)
            {
                foreach (var timedEvent in schedule)
                {
                    timedEvent?.eventActions.Invoke();
                    if (timedEvent.repeating == false)
                    {
                        schedualedEvents.Remove(timedEvent);
                    }   
                }
            }
        }

        public void AddSchedualedEvent(UnityEvent eventAction, TimeSpan eventTime, bool isRepeating = false)
        {
            var timedEventTemp = new TimedEvent
            {
                day = eventTime.Days,
                hour = eventTime.Hours,
                minutes = eventTime.Minutes,
                repeating = isRepeating,
                eventActions = eventAction
            };


            schedualedEvents.Add(timedEventTemp);
        }
        
        
        
        /// <summary>
        /// Just a method to test printing at a time
        /// </summary>
        public void printPing()
        {
            print("ping");
        }

        [ContextMenu("Test add event")]
        public void testAddEvent()
        {
            var myEvent = new UnityEvent();
            myEvent.AddListener(printPing);
            
            AddSchedualedEvent(myEvent,new TimeSpan(2,1,1,1));
        }
        
        
        #endregion
    }
    


}