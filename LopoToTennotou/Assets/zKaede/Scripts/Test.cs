using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Test : MonoBehaviour
{
    Subject<string> subject = new Subject<string>();

    private void Start()
    {
        subject.Subscribe(msg => Debug.Log("1" + msg));
        subject.Subscribe(msg => Debug.Log("2" + msg));
        subject.Subscribe(msg => Debug.Log("3" + msg));

        subject.OnNext("ON");
        subject.OnNext("OFF");
    }
}
