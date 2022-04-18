using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniHooks
{
    public class Hooks<T>
    {
        private static Dictionary<int, IObserver<T>> useEffectDic = new Dictionary<int, IObserver<T>>(256);

        private static int currentGivableId = 0;

        public static (HooksValue<T> value, IObserver<T> setValue) UseState(T initValue)
        {
            var hooksVal = new HooksValue<T>(initValue, currentGivableId);
            currentGivableId++;
            var observer = new _HooksObserver(hooksVal);
            return (hooksVal, observer);
        }

        public static void UseEffect(IObserver<T> observer, HooksValue<T> value)
        {
            if (useEffectDic.ContainsKey(value.Id)) { throw new ArgumentException(); }

            useEffectDic[value.Id] = observer;
        }

        public static void UseEffect(Action<T> action, HooksValue<T> value)
        {
            if (action == null) { throw new NullReferenceException(); }
            if (useEffectDic.ContainsKey(value.Id)) { throw new ArgumentException(); }

            useEffectDic[value.Id] = new HocksSubscriber<T>(action);
        }

        private class _HooksObserver : IObserver<T>
        {
            HooksValue<T> hooksValue = null;

            public _HooksObserver(HooksValue<T> hooksValue)
            {
                this.hooksValue = hooksValue;
            }

            public void OnCompleted()
            {
                this.hooksValue = null;
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(T value)
            {
                if (hooksValue.Value.Equals(value)) { return; }
                hooksValue.Value = value;

                if (!useEffectDic.ContainsKey(hooksValue.Id)) { return; }
                useEffectDic[hooksValue.Id].OnNext(value);
            }
        }
    }
}
