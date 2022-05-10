using UnityEngine;
using AnimationTimeLine.View;
using AnimationTimeLine.Model;

namespace AnimationTimeLine.Presenter
{
    public class ED_AnimationPresenter : MonoBehaviour
    {
        [SerializeField]
        bool IsDebug = true;
        [SerializeField]
        ED_AnimationModel model;
        [SerializeField]
        ED_AnimationView view;
        void Start()
        {
            if (IsDebug) DebugInject();
        }
        void DebugInject()
        {
            view.progressUpdateHandler_ED += model.Progres_Set;
            view.getCurrentProgressNumHandler += model.GetProgress;
            model.progressChangeHandler_ED += view.ChangeInokeTimeLine;
        }
        private void OnDestroy()
        {
            view.progressUpdateHandler_ED -= model.Progres_Set;
            view.getCurrentProgressNumHandler -= model.GetProgress;
            model.progressChangeHandler_ED -= view.ChangeInokeTimeLine;
        }
    }
}