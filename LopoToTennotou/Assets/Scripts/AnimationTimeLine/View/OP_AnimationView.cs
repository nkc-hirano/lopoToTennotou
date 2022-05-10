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
            // �i�s�x���m�F�H
            // �C�x���g�ɐi�s�x�����Ĕ��΁H�H���Ď��̃^�C�����O�Đ��H�H
            progressUpdateHandler_OP(currentNum++);
        }

        // �i�s�x���i�񂾂���s
        public void ChangeInvokeTimeLine(int progress)
        {
            Debug.Log(progress + "�Ԗڂ̃^�C�����O�Đ�");
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