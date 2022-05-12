using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameCore
{
    public class PlayerMoveGimmickAction : MonoBehaviour
    {
        IGimmickMovable movablegimmick = null;
        PlayerCore core = null;
        float rightChargeMaxPower = 0.0f;
        float leftChargeMaxPower = 0.0f;

        float beforeRightPower = 0.0f;
        float beforeLeftPower = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            TryGetComponent(out core);
            core.readOnlyCurrentRightArmPower
                .Subscribe(RightArmPower);

            core.readOnlyCurrentLeftArmPower
                .Subscribe(LeftArmPower);
        }

        // Update is called once per frame
        void Update()
        {
            //// デバッグ用
            //if (Input.GetKey(KeyCode.X))
            //{
            //    RightArmPower(0.1f);
            //}
            //if (Input.GetKeyUp(KeyCode.X))
            //{
            //    RightArmPower(0);
            //}
            //// デバッグ用
            //if (Input.GetKey(KeyCode.Z))
            //{
            //    LeftArmPower(0.1f);
            //}
            //if (Input.GetKeyUp(KeyCode.Z))
            //{
            //    LeftArmPower(0);
            //}
        }

        // 押されたとき、離したとき、最大パワー
        private void RightArmPower(float pushRightPower)
        {
            // 押し始め -> まえのフレームで0.0fで今のフレームで0.0fじゃない
            // 押し終わり -> まえのフレームで0.0fではなく今のフレームで0.0f
            // MaxPower ->　押し終わりの前のフレームの数字

            // X押し始め
            bool isPushStart = beforeRightPower <= 0.0f && pushRightPower != 0.0f;
            // X押し終わり
            bool isPushFinish = beforeRightPower != 0.0f && pushRightPower <= 0.0f;
            // 走る、落ちるアニメーション以外
            bool isAnimationOverride = core.readOnlyCurrentState.Value != PlayerStateType.Run && core.readOnlyCurrentState.Value != PlayerStateType.Fall;

            if (isPushStart && isAnimationOverride)
            {
                // 押しているアニメーション
                core.PlayerStateUpdate(PlayerStateType.Action);
                movablegimmick ??= GetInterface();
            }
            else if (isPushFinish)
            {
                // 前の最大の力を設定する
                rightChargeMaxPower = beforeRightPower;
                // nullだったらスキップ
                movablegimmick?.AddPower(Direction.Down, 0, 0);
                core.PlayerStateUpdate(PlayerStateType.Stay);
                movablegimmick = null;
            }

            beforeRightPower = pushRightPower;
        }

        private void LeftArmPower(float pushLeftPower)
        {
            // Z押し始め
            bool isPushStart = beforeLeftPower <= 0.0f && pushLeftPower != 0.0f;
            // Z押し終わり
            bool isPushFinish = beforeLeftPower != 0.0f && pushLeftPower <= 0.0f;
            // 走る、落ちるアニメーション以外のとき
            bool isAnimationOverwrite = core.readOnlyCurrentState.Value != PlayerStateType.Run && core.readOnlyCurrentState.Value != PlayerStateType.Fall;

            if (isPushStart && isAnimationOverwrite)
            {
                // 押しているアニメーション
                core.PlayerStateUpdate(PlayerStateType.Action);
                movablegimmick ??= GetInterface();
            }
            // 離したとき
            else if (isPushFinish)
            {
                // 前の最大の力を設定する
                leftChargeMaxPower = beforeLeftPower;
                // nullだったらスキップ
                movablegimmick?.AddPower(Direction.Down, 0, 0);
                core.PlayerStateUpdate(PlayerStateType.Stay);
                movablegimmick = null;
            }

            beforeLeftPower = pushLeftPower;
        }

        private IGimmickMovable GetInterface()
        {
            const float RAY_DISTANCE = 3.0f;
            Ray ray = new Ray(transform.position, transform.forward * RAY_DISTANCE);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out IGimmickMovable gimmickMovable))
                {
                    return gimmickMovable;
                }
            }
            return null;
        }
    }
}
