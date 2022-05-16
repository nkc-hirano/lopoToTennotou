using GameCore;
using SceneController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TutorialInitializer : MonoBehaviour
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
