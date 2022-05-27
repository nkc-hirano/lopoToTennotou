using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using GameCore;
using Zenject;

namespace TimerCount
{
    public class TimerController : MonoBehaviour
    {
        public static TimerController instance;

        [Inject]
        CoreStateController coreStateController;
        [SerializeField] Text timeCounterText;                              // ���ԕ\���e�L�X�g
     
        bool stopFlg;   // ������Ƃ߂�t���O

        public delegate void TimerEverntHandler(float time);
        public event TimerEverntHandler OnTimeChanged;
        public event Action<float> stratHandler;
        float time = 0;

        private void Awake()
        {
            OnTimeChanged += TextUpdate;
        }
        private void Start()
        {
            coreStateController.CoreStateTypeUpdateObserver.OnNext(CoreStateType.GamePlay);
        }

        private void FlagChange() 
        {
            // �t���O��؂�ւ���
            stopFlg = stopFlg ? false : true;
        }
        void TextUpdate(float time)
        {
            // �����_���܂ŕ\������
            timeCounterText.text = time.ToString("F1");
        }
        IEnumerator TimerCoroutine() 
        {
            // ���Ԃ��X�V
            while (!stopFlg) 
            {
                time += 0.1f;
                OnTimeChanged(time);
                yield return new WaitForSeconds(0.1f);
            }
        }
        public void StartTimer()
        {
            // �C�x���g����
            StartCoroutine(TimerCoroutine());
        }

        public void EndTimer()
        {
            Debug.Log(time.ToString("F1"));
            FlagChange();
//            coreStateController.CoreStateTypeUpdateObserver.OnNext(CoreStateType.Goal);
        }

        private void OnDestroy()
        {
            OnTimeChanged -= TextUpdate;
        }
    }
}