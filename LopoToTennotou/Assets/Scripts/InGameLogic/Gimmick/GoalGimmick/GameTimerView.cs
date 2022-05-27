using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MVPModule;
using TimerCount;

namespace GameCore
{
    public class GameTimerView : MonoViewBase<float>
    {
        private void Start()
        {
            //TimerController.instance.OnTimeChanged += _ => { subject.OnNext(_); };
            TimerController.instance.StartTimer();
        }

        private void Update()
        {

        }

        public override void OnModelValueChanged(float value)
        {

        }
    }
}
