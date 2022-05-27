using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MVPModule
{
    public class MonoPresenterBase<T> : MonoBehaviour
    {
        // Presenter‚ÌBaseƒNƒ‰ƒX


        [SerializeField]
        protected MonoViewBase<T> view = null;

        [SerializeField]
        protected MonoModelBase<T> model = null;

        void Start()
        {
            ObservableConnect();
            EventHandlerConnect();
        }

        protected virtual void ObservableConnect()
        {
            model.ReadOnlyProperty.Subscribe(view.OnModelValueChanged).AddTo(this);
            view.Observable.Subscribe(model.PropertyValueChange).AddTo(this);
        }

        private void EventHandlerConnect()
        {
            view.CurrentValueGetEventHandler += model.GetCurrentValue;
        }

        private void OnDestroy()
        {
            view.CurrentValueGetEventHandler -= model.GetCurrentValue;
        }
    }
}
