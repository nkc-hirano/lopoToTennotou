using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniHooks;
using System;

namespace SceneController
{
    public class SceneStateController : MonoBehaviour
    {
        SceneStateUpdateProcessDef updateProcessDef = new SceneStateUpdateProcessDef();
        Subject<SceneStateType> sceneTypeUpdateSubject = new Subject<SceneStateType>();

        public IObserver<SceneStateType> SceneTypeUpdateObserver => sceneTypeUpdateSubject;

        private void Start()
        {
            var (value, setValue) = Hooks<SceneStateType>.UseState(SceneStateType.None);

            sceneTypeUpdateSubject.Subscribe(setValue);

            Hooks<SceneStateType>.UseEffect(current =>
            {
                switch (current)
                {
                    case SceneStateType.None:
                        break;
                    case SceneStateType.Init:
                        updateProcessDef.InitStateStartProcess();
                        break;
                    case SceneStateType.Title:
                        updateProcessDef.TitleStateStartProcess();
                        break;
                    case SceneStateType.Introduction:
                        updateProcessDef.IntroductionStateStartProcess();
                        break;
                    case SceneStateType.Tutorial:
                        updateProcessDef.TutorialStateStartProcess();
                        break;
                    case SceneStateType.Game:
                        updateProcessDef.GameStateStartProcess();
                        break;
                    case SceneStateType.Ending:
                        updateProcessDef.EndingStartScene();
                        break;
                    default:
                        throw new ArgumentException();
                }
            }, value);

            setValue.OnNext(SceneStateType.Init);
            setValue.OnNext(SceneStateType.Title);
        }
    }
}