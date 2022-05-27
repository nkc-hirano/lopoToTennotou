using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniHooks;
using UniRx;
using System;
using Zenject;
using SceneController;

namespace GameCore
{
    public class CoreStateController : MonoBehaviour
    {
        [Inject]
        SceneStateController sceneStateController;
        [Inject]
        StageInstantiater stageInstantiater;

        static Subject<Unit> playerContllerableSubject = new Subject<Unit>();
        public static IObserver<Unit> PlayerContllerableObserver => playerContllerableSubject;

        static int currentLoadStageNum = 0;
        CoreStateUpdateProcessDef processes = new CoreStateUpdateProcessDef();
        Subject<CoreStateType> coreStateTypeUpdateSubject = new Subject<CoreStateType>();

        public IObserver<CoreStateType> CoreStateTypeUpdateObserver => coreStateTypeUpdateSubject;
        public IObservable<CoreStateType> CoreStateTypeUpdateObservable => coreStateTypeUpdateSubject;

        void Start()
        {
            var (value, setValue) = Hooks<CoreStateType>.UseState(CoreStateType.None);

            coreStateTypeUpdateSubject.Subscribe(setValue);

            Hooks<CoreStateType>.UseEffect(current =>
            {
                switch (current)
                {
                    case CoreStateType.None:
                        currentLoadStageNum = 0;
                        break;
                    case CoreStateType.Init:
                        processes.CoreGameInitStartProcess(stageInstantiater, currentLoadStageNum);
                        currentLoadStageNum++;
                        break;
                    case CoreStateType.Tutorial:
                        processes.GameTutorialStartProcess(PlayerContllerableObserver);
                        break;
                    case CoreStateType.Start:
                        processes.GameStartProcess();
                        break;
                    case CoreStateType.GamePlay:
                        processes.GamePlayStartProcess();
                        break;
                    case CoreStateType.Goal:
                        processes.GameGoalProcess();
                        break;
                    case CoreStateType.Faild:
                        processes.GameFaildProcess(currentLoadStageNum, CoreStateTypeUpdateObserver);
                        break;
                    case CoreStateType.Final:
                        processes.CoreGameFinalStartProcess(currentLoadStageNum, sceneStateController);
                        break;
                    default:
                        break;
                }
            }, value);
        }
    }
}