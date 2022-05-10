using System;
using UnityEngine;
using SceneController;

namespace AnimationTimeLine.View
{
    public class ED_AnimationView : MonoBehaviour
    {
        public event Action<int> progressUpdateHandler_ED;
        public event Func<int> getCurrentProgressNumHandler;

        ConclusionSceneStateUpdater _conclusionSceneStateUpdater;
        // 
        TimelineProcedurePlayer _timelineProcedurePlayer;
        private void Start()
        {
            _timelineProcedurePlayer.timeLineFinishEventHandler += OnTimelineFinish;
        }
        private void OnTimelineFinish() 
        {
            int currentNum = getCurrentProgressNumHandler();
            progressUpdateHandler_ED(currentNum++);
        }
        public void ChangeInokeTimeLine(int progress)
        {
            Debug.Log(progress + "î‘ñ⁄ÇÃÉ^ÉCÉÄÉçÉOçƒê∂");
            if (_timelineProcedurePlayer.MaxProgress == progress) 
            {
                _conclusionSceneStateUpdater.LoadNextScene();
            }
        }
        private void OnDestroy()
        {
            _timelineProcedurePlayer.timeLineFinishEventHandler -= OnTimelineFinish;
        }
    }
}