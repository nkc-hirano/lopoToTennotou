using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public void GameTutorialStartProcess() { }
        public void GameStartProcess() { }
        public void GamePlayStartProcess() { }
        public void GameGoalProcess() { }
        public void GameFaildProcess() { }
        public void CoreGameFinalStartProcess() { }
    }
}
