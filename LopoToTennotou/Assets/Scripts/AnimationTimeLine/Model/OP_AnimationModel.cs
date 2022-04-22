using UnityEngine;
using System;

namespace AnimationTimeLine.Model
{
    public class OP_AnimationModel : MonoBehaviour
    {
        int progress = 0;       // �A�j���[�V�����̐i�x
        public event Action<int> progressChangeHandler_OP;

        public void Progress_Set(int currentProgress)
        {
            // �i�s�x�ɕω�����΁A�C�x���g���΁H
            if (progress != currentProgress) progressChangeHandler_OP(currentProgress);
            // �l����
            progress = currentProgress;
        }
        public int GetProgress()
        {
            return progress;
        }
    }
}