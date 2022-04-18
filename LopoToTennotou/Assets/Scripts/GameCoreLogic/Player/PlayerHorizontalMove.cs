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
        [SerializeField]
        float mulPlayerSpeed = 0.0f;

        private Vector2 velocityForUpdate = Vector2.zero;

        private new Rigidbody rigidbody = null;
        private PlayerCore core = null;

        void Start()
        {
            // �R���|�[�l���g�̊֘A�t��
            TryGetComponent(out rigidbody);
            TryGetComponent(out core);

            core.CurrentDistanceObservable
                .Subscribe(RigidbodyHorizontalMove);
        }

        private void FixedUpdate()
        {
            var velocity = new Vector3(velocityForUpdate.x,0, velocityForUpdate.y) * mulPlayerSpeed;
            rigidbody.velocity = velocity;
        }

        // �v���C���[�̈ړ���veloctiy�ɕύX
        private void RigidbodyHorizontalMove(Vector2 distance)
        {
            velocityForUpdate = distance.normalized;
        }
    }
}
