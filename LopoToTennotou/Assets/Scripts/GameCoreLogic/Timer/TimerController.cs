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
        [SerializeField] Text timeCounterText;                              // 時間表示テキスト
     
        bool stopFlg;   // 動作をとめるフラグ

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
            // フラグを切り替える
            stopFlg = stopFlg ? false : true;
        }
        void TextUpdate(float time)
        {
            // 小数点第一まで表示する
            timeCounterText.text = time.ToString("F1");
        }
        IEnumerator TimerCoroutine() 
        {
            // 時間を更新
            while (!stopFlg) 
            {
                time += 0.1f;
                OnTimeChanged(time);
                yield return new WaitForSeconds(0.1f);
            }
        }
        public void StartTimer()
        {
            // イベント発火
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