using UnityEngine;
using System.Collections.Generic;

namespace UniRx
{
    public static partial class NGUIComponentExtensions
    {
        public static IObservable<Unit> OnClickAsObservable(this UIButton button)
        {
            return button.onClick.AsObservable();
        }

        public static IObservable<Unit> OnValueChangedAsObservable(this UIProgressBar slider)
        {
            return Observable.Create<Unit>(observer =>
            {
                //HACK: 
                //observer.OnNext(slider.value);
                return slider.onChange.AsObservable().Subscribe(observer);
            });
        }
    }
}