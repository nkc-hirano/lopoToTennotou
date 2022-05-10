using System;
using UnityEngine;

namespace AnimationTimeLine.Model
{
    public class ED_AnimationModel : MonoBehaviour
    {
        int progress;
        public event Action<int> progressChangeHandler_ED;
        public void Progres_Set(int currentProgress)
        {
            if (progress != currentProgress) progressChangeHandler_ED(currentProgress);
            progress = currentProgress;
        }
        public int GetProgress()
        {
            return progress;
        }
    }
}