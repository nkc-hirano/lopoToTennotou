using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameCore.Player
{
    /* �v���C���[�̈ړ��N���X*/
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerCore))]
    public class PlayerHorizontalMove : MonoBehaviour
    {
        static readonly Vector2[] dirVec2Table = new Vector2[]
        {
            Vector2.right,
            (Vector2.right + Vector2.up).normalized,
            Vector2.up,
            (Vector2.up + Vector2.left).normalized,
            Vector2.left,
            (Vector2.left + Vector2.down).normalized,
            Vector2.down,
            (Vector2.down + Vector2.right).normalized,
        };

        [SerializeField]
        float mulPlayerSpeed = 0.0f;

        private Vector2 velocityForUpdate = Vector2.zero;

        PlayerCore core = null;
        new Rigidbody rigidbody = null;

        void Start()
        {
            // �R���|�[�l���g�̊֘A�t��
            TryGetComponent(out core);
            TryGetComponent(out rigidbody);

            core.CurrentDistanceObservable
                .Where(val => core.readOnlyCurrentState.Value != PlayerStateType.Action)
                .Where(val => core.readOnlyCurrentState.Value != PlayerStateType.Fall)
                .Where(val => core.readOnlyCurrentState.Value != PlayerStateType.Stop)
                .Subscribe(RigidbodyHorizontalMove);
        }

        private void FixedUpdate()
        {
            // ��������
            Vector3 dir = new Vector3(velocityForUpdate.x, 0, velocityForUpdate.y);
            rigidbody.velocity = dir * mulPlayerSpeed;

            if (dir == Vector3.zero) { return; }
            // Y���𒆐S�ɂ��Č���
            var rot = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.3f);
        }

        // �v���C���[�̈ړ���veloctiy�ɕύX
        private void RigidbodyHorizontalMove(Vector2 distance)
        {
            // �����n�߂��
            float deadInputPower = 0.3f;

            // �����ĂȂ��Ƃ�
            if(distance.magnitude < deadInputPower)
            {   
                if (core.readOnlyCurrentState.Value != PlayerStateType.Action)
                core.PlayerStateUpdate(PlayerStateType.Stay);
                velocityForUpdate = Vector2.zero;
                return;
            }
            core.PlayerStateUpdate(PlayerStateType.Run);

            const float ANGLE_CUL = 22.5f; // ����p�x
            var unsignedDeg = Vector2.Angle(Vector2.right, distance.normalized);
            // ��������
            var signedDeg = distance.y < 0 ? 360.0f - unsignedDeg : unsignedDeg;
            int tableNum = (int)((signedDeg + ANGLE_CUL) / 45) % dirVec2Table.Length;
            velocityForUpdate = dirVec2Table[tableNum];
        }
    }
}
