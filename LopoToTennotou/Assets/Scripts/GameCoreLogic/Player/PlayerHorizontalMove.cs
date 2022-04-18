using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameCore.Player
{
    /* プレイヤーの移動クラス*/
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
            // コンポーネントの関連付け
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

        // プレイヤーの移動をveloctiyに変更
        private void RigidbodyHorizontalMove(Vector2 distance)
        {
            velocityForUpdate = distance.normalized;
        }
    }
}
