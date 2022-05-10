using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Test_TimerCount.Clear;

namespace Test_TimerCount
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] Text timeCounterText;                              // ���ԕ\���e�L�X�g
        [SerializeField] Test_ClearPointController clearPointController;    // �N���A��

        bool stopFlg;   // ������Ƃ߂�t���O

        delegate void TimerEverntHandler(float time);
        event TimerEverntHandler OnTimeChanged;

        public int FlagChange() 
        {
            // �t���O��؂�ւ���
            stopFlg = stopFlg ? false : true;
            return 0;
        }
        void TextUpdate(float time)
        {
            // �����_���܂ŕ\������
            timeCounterText.text = time.ToString("F1");
        }
        IEnumerator TimerCoroutine() 
        {
            // ���Ԃ��X�V
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
            // �C�x���g����
            StartCoroutine(TimerCoroutine());
        }
        private void OnDestroy()
        {
            OnTimeChanged -= TextUpdate;
            clearPointController.eventSystemHandler -= FlagChange;
        }
    }
}