using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniHooks;
using UniRx;
using System;

namespace GameCore
{
    public class CoreStateController : MonoBehaviour
    {
        Subject<CoreStateType> coreStateTypeUpdateSubject = new Subject<CoreStateType>();

        public IObserver<CoreStateType> CoreStateTypeUpdateObserver => coreStateTypeUpdateSubject;

        void Start()
        {
            var (value, setValue) = Hooks<CoreStateType>.UseState(CoreStateType.None);

            coreStateTypeUpdateSubject.Subscribe(setValue);

            Hooks<CoreStateType>.UseEffect(current =>
            {
                switch (current)
                {
                    case CoreStateType.None:
                        break;
                    case CoreStateType.Init:
                        break;
                    case CoreStateType.Start:
                        break;
                    case CoreStateType.GamePlay:
                        break;
                    case CoreStateType.Goal:
                        break;
                    case CoreStateType.Final:
                        break;
                    default:
                        break;
                }
            },value);
        }
    }
}
