using UnityEngine;
using UniRx;
using System;

namespace MVPModule
{
    public class MonoViewBase<T> : MonoBehaviour
    {
        // View‚ÌBaseƒNƒ‰ƒX


        public event Func<T> CurrentValueGetEventHandler;

        protected T CurrentValue => CurrentValueGetEventHandler();

        protected Subject<T> subject = new Subject<T>();

        public IObservable<T> Observable => subject;

        public virtual void OnModelValueChanged(T value) { }
    }
}