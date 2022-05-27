using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneController
{
    public struct SceneStateUpdateProcessDef
    {
        private static readonly string logicSceneName = "LogicScene";
        private static readonly string fadeSceneName = "FadeScene";
        private static readonly string titleSceneName = "TitleScene";
        private static readonly string introductionSceneName = "IntroductionScene";
        private static readonly string tutorialSceneName = "TutorialScene";
        private static readonly string gameSceneName = "GameScene";
        private static readonly string endingSceneName = "EndingScene";

        private static int gameSceneLoadNum = 0;

        public void InitStateStartProcess()
        {
            SceneManager.LoadSceneAsync(logicSceneName, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(fadeSceneName, LoadSceneMode.Additive);
        }

        public void TitleStateStartProcess(int id = 0)
        {
            SceneManager.LoadSceneAsync(titleSceneName, LoadSceneMode.Additive);
        }

        public void IntroductionStateStartProcess()
        {
            SceneManager.UnloadSceneAsync(titleSceneName);
            SceneManager.LoadSceneAsync(introductionSceneName, LoadSceneMode.Additive);
        }

        public void TutorialStateStartProcess()
        {
            SceneManager.UnloadSceneAsync(introductionSceneName);
            SceneManager.LoadSceneAsync(tutorialSceneName, LoadSceneMode.Additive);
        }

        public void GameStateStartProcess()
        {
            if (gameSceneLoadNum == 0)
            {
                SceneManager.UnloadSceneAsync(tutorialSceneName);
                gameSceneLoadNum++;
            }
            else
            {
                SceneManager.UnloadSceneAsync(gameSceneName);
            }
            SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Additive);

            
        }

        public void EndingStartScene()
        {
            SceneManager.UnloadSceneAsync(gameSceneName);
            SceneManager.LoadSceneAsync(endingSceneName);
        }
    }
}
