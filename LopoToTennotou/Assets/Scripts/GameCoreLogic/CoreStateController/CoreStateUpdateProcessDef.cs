using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using TimerCount;
using SceneController;

namespace GameCore
{
    public struct CoreStateUpdateProcessDef
    {
        const string TUTORIAL_SCENE_NAME = "TutorialScene";
        const string GAME_SCENE_NAME = "GameScene";
        const int MAX_STAGE_NUM = 2;

        public void CoreGameInitStartProcess(StageInstantiater stageInstantiater, int StageNum)
        {
            stageInstantiater.StageCreate(StageNum);
            Scene scene = StageNum == 0 ? SceneManager.GetSceneByName(TUTORIAL_SCENE_NAME) : SceneManager.GetSceneByName(GAME_SCENE_NAME);
            Debug.Log(scene);
            SceneManager.MoveGameObjectToScene(stageInstantiater.RootObject, scene);
        }

        public void GameTutorialStartProcess(IObserver<Unit> PlayerContllerableObserver)
        {

        }

        public void GameStartProcess() { }
        public void GamePlayStartProcess()
        {
            // �^�C�}�[�J�n
            Debug.Log("�^�C�}�[�J�n");
            //TimerController.instance.StartTimer();
        }
        public void GameGoalProcess()
        {
            // �^�C�}�[�������Ă��邱�Ƃ��m�F
            // �^�C�}�[��~
            //Debug.Log("�^�C�}�[��~");
            //TimerController.instance.EndTimer();
        }
        public void GameFaildProcess(int StageNum, IObserver<CoreStateType> coreStateTypeUpdateObserver)
        {

        }

        public void CoreGameFinalStartProcess(int StageNum, SceneStateController sceneStateController)
        {
            Scene scene = StageNum == 0 ? SceneManager.GetSceneByName(TUTORIAL_SCENE_NAME) : SceneManager.GetSceneByName(GAME_SCENE_NAME);
            //SceneManager.UnloadSceneAsync(scene);
            if (StageNum == MAX_STAGE_NUM)
            {
                sceneStateController.SceneTypeUpdateObserver.OnNext( SceneStateType.Ending);
                return;
            }
            sceneStateController.SceneTypeUpdateObserver.OnNext(SceneStateType.None);
            sceneStateController.SceneTypeUpdateObserver.OnNext(SceneStateType.Game);
        }
    }
}
