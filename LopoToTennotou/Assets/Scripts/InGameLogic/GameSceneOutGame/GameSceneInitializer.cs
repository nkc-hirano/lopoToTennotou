using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameCore
{
    public class GameSceneInitializer : MonoBehaviour
    {
        [SerializeField]
        FadeFaçade façade;
        [Inject]
        CoreStateController coreStateController;

        void Start()
        {

            coreStateController.CoreStateTypeUpdateObserver.OnNext(CoreStateType.Init);
            façade.FadeIn();
        }
    }
}
