using UnityEngine;
using AnimationTimeLine.View;
using AnimationTimeLine.Model;

namespace AnimationTimeLine.Presenter
{
    public class OP_AnimationPresenter : MonoBehaviour
    {
        [SerializeField]
        bool IsDebug = true;
        [SerializeField]
        OP_AnimationModel model;
        [SerializeField]
        OP_AnimationView view;
        void Start()
        {
            if (IsDebug) DebugInject();
        }
        void DebugInject()
        {
            // ƒCƒxƒ“ƒg‚Ì“o˜^
            view.progressUpdateHandler_OP += model.Progress_Set;
            view.getCurrentProgressNumHandler += model.GetProgress;
            model.progressChangeHandler_OP += view.ChangeInvokeTimeLine;
        }
        private void OnDestroy()
        {
            view.progressUpdateHandler_OP -= model.Progress_Set;
            view.getCurrentProgressNumHandler -= model.GetProgress;
            model.progressChangeHandler_OP -= view.ChangeInvokeTimeLine;
        }
    }
}