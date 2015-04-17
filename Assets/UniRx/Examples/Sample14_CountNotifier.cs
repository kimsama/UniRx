using UnityEngine;
using System.Collections;

namespace UniRx.Examples
{
    /// <summary>
    /// A simple CountNotifier usage.
    /// </summary>
    public class Sample14_CountNotifier : MonoBehaviour
    {
        void Start()
        {
            CountNotifier countNotifier = new CountNotifier();
            countNotifier.Subscribe(x =>
            {
                // It will put 'Increment' if CountNotifier does increment count 
                // otherwise put 'Decrement' except the case which is the count get max.
                Debug.Log(x);
                Debug.Log("count: " + countNotifier.Count.ToString());
            });

            // whenever clicking left button, it increase a count and put it on the console.
            var counter = Observable.EveryUpdate()
                                    .Where( _ => Input.GetMouseButtonDown(0))
                                    .Subscribe( _ => countNotifier.Increment() );
                                    
        }

    }
}