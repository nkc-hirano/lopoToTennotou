using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using GameCore.Player;

namespace GameCore
{
    public class PlayerAnimation : MonoBehaviour
    {
        PlayerCore core = null;
        Animator animator = null;

        // �f�o�b�O�p
        //[SerializeField]
        //KaedeTestAnimation testAnimation;

        int runFlg = Animator.StringToHash("IsRun");   // ����A�j���[�V�����̖��O
        int pushFlg = Animator.StringToHash("IsPush"); // �����A�j���[�V�����̖��O
        int fallFlg = Animator.StringToHash("IsFall"); // ������A�j���[�V�����̖��O
        int stopFlg = Animator.StringToHash("IsStop"); // �C�x���g��

        // Start is called before the first frame update
        void Start()
        {
            TryGetComponent(out core);
            TryGetComponent(out animator);

            core.readOnlyCurrentState
                .Subscribe(ActionMotion);

            // �f�o�b�O�p
            //testAnimation.StateChangeEventHandler += ActionMotion;
        }

        private void ActionMotion(PlayerStateType statetype)
        {
            animator.SetBool(runFlg, false);
            animator.SetBool(pushFlg, false);

            switch (statetype)
            {
                case PlayerStateType.Stay:              // �؍ݒ�
                    animator.SetBool(runFlg, false);
                    break;
                case PlayerStateType.Run:               // ����
                    animator.SetBool(runFlg, true);
                    break;
                case PlayerStateType.Action:            // ����
                    animator.SetBool(pushFlg, true);
                    break;
                case PlayerStateType.Fall:              // ������
                    animator.SetTrigger(fallFlg);
                    break;
                case PlayerStateType.Stop:
                    animator.SetTrigger(stopFlg);
                    break;
                default:
                    break;
            }
        }
    }
}
