using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
