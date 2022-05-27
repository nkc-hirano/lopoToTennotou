using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace MVPModule
{
    public static class ModelContainer<T>
    {
        static readonly int listCapacity = 64;

        static Dictionary<string, List<MonoModelBase<T>>> modelDic = new Dictionary<string, List<MonoModelBase<T>>>();
        static Dictionary<string, List<IDisposable>> disposableDic = new Dictionary<string, List<IDisposable>>();

        /// <summary>
        /// モデル用
        /// </summary>
        /// <param name="modelInstance"></param>
        public static void Register(MonoModelBase<T> modelInstance)
        {
            if (!modelDic.ContainsKey(modelInstance.ContentsName))
            {
                disposableDic[modelInstance.ContentsName] = new List<IDisposable>();
                modelDic[modelInstance.ContentsName] = new List<MonoModelBase<T>>(listCapacity);
            }

            foreach (MonoModelBase<T> model in modelDic[modelInstance.ContentsName])
            {
                var disposable1 = model.ReadOnlyProperty.Subscribe(modelInstance.PropertyValueChange);
                disposableDic[modelInstance.ContentsName].Add(disposable1);

                var disposable2 = modelInstance.ReadOnlyProperty.Subscribe(model.PropertyValueChange);
                disposableDic[modelInstance.ContentsName].Add(disposable2);
            }

            modelDic[modelInstance.ContentsName].Add(modelInstance);
        }

        /// <summary>
        /// モデル用
        /// </summary>
        /// <param name="name"></param>
        public static void Clear(string name)
        {
            foreach (IDisposable disposable in disposableDic[name])
            {
                disposable.Dispose();
            }
            modelDic[name].Clear();
            disposableDic[name].Clear();
        }

        /// <summary>
        /// メインロジック用
        /// </summary>
        /// <param name="contentsName"></param>
        /// <returns></returns>
        public static IReadOnlyReactiveProperty<T> GetModelValueChangeObsevable(string contentsName)
        {
            if (!modelDic.ContainsKey(contentsName)) { return null; }

            return modelDic[contentsName][0].ReadOnlyProperty;
        }

        /// <summary>
        /// メインロジック用
        /// </summary>
        /// <param name="contentsName"></param>
        /// <param name="value"></param>
        public static void SetModelPropertyValue(string contentsName, T value)
        {
            if (!modelDic.ContainsKey(contentsName)) { return; }

            modelDic[contentsName][0].PropertyValueChange(value);
        }
    }
}
