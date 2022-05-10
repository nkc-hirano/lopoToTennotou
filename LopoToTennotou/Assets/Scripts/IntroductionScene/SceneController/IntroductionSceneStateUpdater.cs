namespace SceneController
{
    public class IntroductionSceneStateUpdater : SceneStateUpdaterBase
    {
        public override void LoadNextScene()
        {
            controller.SceneTypeUpdateObserver.OnNext(SceneStateType.Tutorial);
        }
    }
}