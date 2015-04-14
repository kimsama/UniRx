using UnityEngine;
using System.Collections;

namespace UniRx.Examples
{
    public class Sample : MonoBehaviour
    {

        public UIButton button;

        void Start()
        {

            button.onClick.AsObservable().Subscribe(_ => { });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}