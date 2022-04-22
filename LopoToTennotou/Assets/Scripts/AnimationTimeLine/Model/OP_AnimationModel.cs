using UnityEngine;
using System;

namespace AnimationTimeLine.Model
{
    public class OP_AnimationModel : MonoBehaviour
    {
        int progress = 0;       // アニメーションの進度
        public event Action<int> progressChangeHandler_OP;

        public void Progress_Set(int currentProgress)
        {
            // 進行度に変化あれば、イベント発火？
            if (progress != currentProgress) progressChangeHandler_OP(currentProgress);
            // 値統一
            progress = currentProgress;
        }
        public int GetProgress()
        {
            return progress;
        }
    }
}