using System;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "EventChannel/TimeSpan")]
    public class EventChannelTimeSpan : ScriptableObject
    {
        public event Action<TimeSpan> OnEvent;

        public void RaiseEvent(TimeSpan value)
        {
            OnEvent?.Invoke(value);
        }

        public int GetNumberOfLiseners()
        {
            return OnEvent.GetInvocationList().Length;
        }
    }
}