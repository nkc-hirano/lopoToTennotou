using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SceneController
{
    public class SceneStateUpdaterBase : MonoBehaviour
    {
        [Inject]
        protected SceneStateController controller;

        public virtual void LoadNextScene()
        {

        }
    }
}
