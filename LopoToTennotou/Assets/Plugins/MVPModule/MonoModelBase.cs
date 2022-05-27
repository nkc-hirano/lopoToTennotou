using UnityEngine;
using UniRx;

namespace MVPModule
{
    public class MonoModelBase<T> : MonoBehaviour
    {
        // Model‚ÌBaseƒNƒ‰ƒX

        [SerializeField]
        string contentsName = null;

        protected ReactiveProperty<T> property = new ReactiveProperty<T>(default);

        public IReadOnlyReactiveProperty<T> ReadOnlyProperty => property;

        public string ContentsName => contentsName;


        public virtual T GetCurrentValue()
        {
            return ReadOnlyProperty.Value;
        }

        public virtual void PropertyValueChange(T value) { }


        private void Start()
        {
            ModelContainer<T>.Register(this);
        }
    }
}
