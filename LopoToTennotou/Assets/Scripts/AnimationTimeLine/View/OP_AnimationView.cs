using UnityEngine;
using System;
using SceneController;

namespace AnimationTimeLine.View
{
    public class OP_AnimationView : MonoBehaviour
    {
        public event Func<int> getCurrentProgressNumHandler;
        public event Action<int> progressUpdateHandler_OP;
        TimelineProcedurePlayer _timelineProcedurePlayer;
        IntroductionSceneStateUpdater _introductionSceneStateUpdater;

        private void Start()
        {
            _timelineProcedurePlayer.timeLineFinishEventHandler += OnTimelineFinish;
        }
        private void OnTimelineFinish()
        {
            int currentNum = getCurrentProgressNumHandler();
            // 進行度を確認？
            // イベントに進行度を入れて発火？？して次のタイムログ再生？？
            progressUpdateHandler_OP(currentNum++);
        }

        // 進行度が進んだら実行
        public void ChangeInvokeTimeLine(int progress)
        {
            Debug.Log(progress + "番目のタイムログ再生");
            if (_timelineProcedurePlayer.MaxProgress == progress)
            {
                _introductionSceneStateUpdater.LoadNextScene();
            }
        }
        private void OnDestroy()
        {
            _timelineProcedurePlayer.timeLineFinishEventHandler -= OnTimelineFinish;
        }
    }
}