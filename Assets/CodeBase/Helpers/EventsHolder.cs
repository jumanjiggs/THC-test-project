using UnityEngine;
using UnityEngine.Events;

namespace CodeBase
{
    public class EventsHolder : Singleton<EventsHolder>
    {
        public UnityEvent bucketCompleted;
    }
}