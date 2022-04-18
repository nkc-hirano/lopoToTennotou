using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneController
{
    public struct SceneStateUpdateProcessDef
    {
        private static readonly string logicSceneName = "LogicScene";
        private static readonly string titleSceneName = "TitleScene";
        private static readonly string introductionSceneName = "IntroductionScene";
        private static readonly string tutorialSceneName = "TutorialScene";
        private static readonly string gameSceneName = "GameScene";


        public void InitStateStartProcess()
        {
            SceneManager.LoadSceneAsync(logicSceneName, LoadSceneMode.Additive);
        }

        public void TitleStateStartProcess(int id = 0)
        {
            SceneManager.LoadSceneAsync(titleSceneName, LoadSceneMode.Additive);
        }

        public void IntroductionStateStartProcess()
        {
            SceneManager.LoadSceneAsync(introductionSceneName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(titleSceneName);
        }

        public void TutorialStateStartProcess()
        {
            SceneManager.LoadSceneAsync(tutorialSceneName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(introductionSceneName);
        }

        public void GameStateStartProcess(int id = 0)
        {
            SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(tutorialSceneName);
        }
    }
}
