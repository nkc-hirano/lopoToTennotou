using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UniHooks
{
    internal class HocksSubscriber<T> : IObserver<T>
    {
        private Action<T> onNextFunc;
        private Action<Exception> onErrorFunc;
        private Action onComprateFunc;

        public HocksSubscriber(Action<T> onNextFunc)
        {
            this.onNextFunc = onNextFunc;
        }

        public HocksSubscriber(Action<T> onNextFunc, Action<Exception> onErrorFunc)
        {
            this.onNextFunc = onNextFunc;
            this.onErrorFunc = onErrorFunc;
        }

        public HocksSubscriber(Action<T> onNextFunc, Action<Exception> onErrorFunc, Action onComprateFunc)
        {
            this.onNextFunc = onNextFunc;
            this.onErrorFunc = onErrorFunc;
            this.onComprateFunc = onComprateFunc;
        }

        public void OnCompleted()
        {
            if (onComprateFunc == null) { throw new NullReferenceException(); }
            onComprateFunc();
        }

        public void OnError(Exception error)
        {
            if (onErrorFunc == null) { throw new NullReferenceException(); }
            onErrorFunc(error);
        }

        public void OnNext(T value)
        {
            if (onNextFunc == null) { throw new NullReferenceException(); }
            onNextFunc(value);
        }
    }
}
