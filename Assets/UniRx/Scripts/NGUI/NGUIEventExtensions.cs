using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;

namespace UniRx
{
    public static partial class NGUIEvent
    {
        public static IObservable<Unit> AsObservable(this List<EventDelegate> nguiEvent)
        {
            var dummy = 0;
            return Observable.FromEvent<EventDelegate.Callback>(h =>
            {
                dummy.GetHashCode(); // capture for AOT issue
                return new EventDelegate.Callback(h);
            }, h => EventDelegate.Add(nguiEvent, h), h => EventDelegate.Remove(nguiEvent, h));
        }

        /* Could not make to create an instance of EventDelegate<T> type
        public static IObservable<T> AsObservable<T>(this List<EventDelegate> nguiEvent)
        {
            var dummy = 0;
            return Observable.FromEvent<EventDelegate, T>(h =>
            {
                dummy.GetHashCode(); // capture for AOT issue
                //EventDelegate.Callback cb = new EventDelegate.Callback(h);
                EventDelegate evt = new EventDelegate();
                evt.parameters = T as object;
                return evt;
            }, h => EventDelegate.Add(nguiEvent, h), h => EventDelegate.Remove(nguiEvent, h));
        }
        */
    }
}