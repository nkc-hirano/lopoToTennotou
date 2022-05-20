using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using TimerCount;

namespace GameCore
{
    public struct CoreStateUpdateProcessDef
    {
        
        public void CoreGameInitStartProcess(StageInstantiater stageInstantiater, int StageNum)
        {
            const string TUTORIAL_SCENE_NAME = "TutorialScene";
            const string GAME_SCENE_NAME = "GameScene";
            stageInstantiater.StageCreate(StageNum);
            Scene scene = StageNum == 0 ? SceneManager.GetSceneByName(TUTORIAL_SCENE_NAME) : SceneManager.GetSceneByName(GAME_SCENE_NAME);
            SceneManager.MoveGameObjectToScene(stageInstantiater.RootObject, scene);
        }

        public void GameTutorialStartProcess(IObserver<Unit> PlayerContllerableObserver) 
        {

        }

        public void GameStartProcess() { }
        public void GamePlayStartProcess() 
        {
            // タイマー開始
            Debug.Log("タイマー開始");
            TimerController.instance.StartTimer();
        }
        public void GameGoalProcess()
        {
            // タイマーが動いていることを確認
            // タイマー停止
            Debug.Log("タイマー停止");
            TimerController.instance.EndTimer();
        }
        public void GameFaildProcess() { }
        public void CoreGameFinalStartProcess() { }
    }
}
