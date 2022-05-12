using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameCore
{
    public class StageDataLoaderBase : MonoBehaviour
    {
        [Inject]
        protected IStageInitializable initializer;
    }
}
