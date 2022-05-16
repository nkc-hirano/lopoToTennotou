using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MitsukiTestUIInputTest : MonoBehaviour
{
    UIInputProvider uIInputProvider;
    IObserver<Vector2> observer;
    void Start()
    {
        uIInputProvider = gameObject.AddComponent<UIInputProvider>();
        uIInputProvider.crossMovementObservable.Subscribe(val =>
        {
            Debug.Log("‹N“®Šm”F");
        });
    }
}
