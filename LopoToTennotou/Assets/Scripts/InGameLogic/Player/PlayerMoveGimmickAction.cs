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
            //// �f�o�b�O�p
            //if (Input.GetKey(KeyCode.X))
            //{
            //    RightArmPower(0.1f);
            //}
            //if (Input.GetKeyUp(KeyCode.X))
            //{
            //    RightArmPower(0);
            //}
            //// �f�o�b�O�p
            //if (Input.GetKey(KeyCode.Z))
            //{
            //    LeftArmPower(0.1f);
            //}
            //if (Input.GetKeyUp(KeyCode.Z))
            //{
            //    LeftArmPower(0);
            //}
        }

        // �����ꂽ�Ƃ��A�������Ƃ��A�ő�p���[
        private void RightArmPower(float pushRightPower)
        {
            // �����n�� -> �܂��̃t���[����0.0f�ō��̃t���[����0.0f����Ȃ�
            // �����I��� -> �܂��̃t���[����0.0f�ł͂Ȃ����̃t���[����0.0f
            // MaxPower ->�@�����I���̑O�̃t���[���̐���

            // X�����n��
            bool isPushStart = beforeRightPower <= 0.0f && pushRightPower != 0.0f;
            // X�����I���
            bool isPushFinish = beforeRightPower != 0.0f && pushRightPower <= 0.0f;
            // ����A������A�j���[�V�����ȊO
            bool isAnimationOverride = core.readOnlyCurrentState.Value != PlayerStateType.Run && core.readOnlyCurrentState.Value != PlayerStateType.Fall;

            if (isPushStart && isAnimationOverride)
            {
                // �����Ă���A�j���[�V����
                core.PlayerStateUpdate(PlayerStateType.Action);
                movablegimmick ??= GetInterface();
            }
            else if (isPushFinish)
            {
                // �O�̍ő�̗͂�ݒ肷��
                rightChargeMaxPower = beforeRightPower;
                // null��������X�L�b�v
                movablegimmick?.AddPower(Direction.Down, 0, 0);
                core.PlayerStateUpdate(PlayerStateType.Stay);
                movablegimmick = null;
            }

            beforeRightPower = pushRightPower;
        }

        private void LeftArmPower(float pushLeftPower)
        {
            // Z�����n��
            bool isPushStart = beforeLeftPower <= 0.0f && pushLeftPower != 0.0f;
            // Z�����I���
            bool isPushFinish = beforeLeftPower != 0.0f && pushLeftPower <= 0.0f;
            // ����A������A�j���[�V�����ȊO�̂Ƃ�
            bool isAnimationOverwrite = core.readOnlyCurrentState.Value != PlayerStateType.Run && core.readOnlyCurrentState.Value != PlayerStateType.Fall;

            if (isPushStart && isAnimationOverwrite)
            {
                // �����Ă���A�j���[�V����
                core.PlayerStateUpdate(PlayerStateType.Action);
                movablegimmick ??= GetInterface();
            }
            // �������Ƃ�
            else if (isPushFinish)
            {
                // �O�̍ő�̗͂�ݒ肷��
                leftChargeMaxPower = beforeLeftPower;
                // null��������X�L�b�v
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
