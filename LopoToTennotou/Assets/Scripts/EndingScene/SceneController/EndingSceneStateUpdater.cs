namespace SceneController
{
    public class EndingSceneStateUpdater : SceneStateUpdaterBase
    {
        public override void LoadNextScene()
        {
            controller.SceneTypeUpdateObserver.OnNext(SceneStateType.Title);
        }
    }
}