using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Test_TimerCount.Clear;

namespace Test_TimerCount
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] Text timeCounterText;                              // 時間表示テキスト
        [SerializeField] Test_ClearPointController clearPointController;    // クリア状況

        bool stopFlg;   // 動作をとめるフラグ

        delegate void TimerEverntHandler(float time);
        event TimerEverntHandler OnTimeChanged;

        public int FlagChange() 
        {
            // フラグを切り替える
            stopFlg = stopFlg ? false : true;
            return 0;
        }
        void TextUpdate(float time)
        {
            // 小数点第一まで表示する
            timeCounterText.text = time.ToString("F1");
        }
        IEnumerator TimerCoroutine() 
        {
            // 時間を更新
            float time = 0;
            while (!stopFlg) 
            {
                time += 0.1f;
                OnTimeChanged(time);
                yield return new WaitForSeconds(0.1f);
            }
        }
        private void Awake()
        {
            OnTimeChanged += TextUpdate;
            clearPointController.eventSystemHandler += FlagChange;
        }
        void Start()
        {
            // イベント発火
            StartCoroutine(TimerCoroutine());
        }
        private void OnDestroy()
        {
            OnTimeChanged -= TextUpdate;
            clearPointController.eventSystemHandler -= FlagChange;
        }
    }
}