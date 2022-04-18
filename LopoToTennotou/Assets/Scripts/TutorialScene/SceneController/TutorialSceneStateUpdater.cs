using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneController
{
    public class TutorialSceneStateUpdater : SceneStateUpdaterBase
    {
        public override void LoadNextScene()
        {
            controller.SceneTypeUpdateObserver.OnNext(SceneStateType.Game);
        }
    }
}
