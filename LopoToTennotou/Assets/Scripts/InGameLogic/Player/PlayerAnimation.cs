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

        // デバッグ用
        //[SerializeField]
        //KaedeTestAnimation testAnimation;

        int runFlg = Animator.StringToHash("IsRun");   // 走るアニメーションの名前
        int pushFlg = Animator.StringToHash("IsPush"); // 押すアニメーションの名前
        int fallFlg = Animator.StringToHash("IsFall"); // 落ちるアニメーションの名前
        int stopFlg = Animator.StringToHash("IsStop"); // イベント中

        // Start is called before the first frame update
        void Start()
        {
            TryGetComponent(out core);
            TryGetComponent(out animator);

            core.readOnlyCurrentState
                .Subscribe(ActionMotion);

            // デバッグ用
            //testAnimation.StateChangeEventHandler += ActionMotion;
        }

        private void ActionMotion(PlayerStateType statetype)
        {
            animator.SetBool(runFlg, false);
            animator.SetBool(pushFlg, false);

            switch (statetype)
            {
                case PlayerStateType.Stay:              // 滞在中
                    animator.SetBool(runFlg, false);
                    break;
                case PlayerStateType.Run:               // 走る
                    animator.SetBool(runFlg, true);
                    break;
                case PlayerStateType.Action:            // 押す
                    animator.SetBool(pushFlg, true);
                    break;
                case PlayerStateType.Fall:              // 落ちる
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
