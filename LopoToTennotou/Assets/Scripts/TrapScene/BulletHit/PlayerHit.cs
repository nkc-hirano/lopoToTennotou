using UnityEngine;
using GameCore;
using System;
using UniRx;

namespace Trap.Bullet.Hit
{
    public class PlayerHit : MonoBehaviour,IHitBullet
    {
        [SerializeField]
        PlayerCore core;
        int beforenum;
        [SerializeField]
        PlayerSus sus;

        public void BulletHitAction()
        {
            PlayerStateType type = PlayerStateType.Hit;
            core.PlayerStateUpdate(type);
            Debug.Log("プレイヤヒット");
            core.PlayerStateUpdate(PlayerStateType.Stop);
            sus.PlayerSusSubject.OnNext(Unit.Default);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<IGimmickHit>() != null) 
            {
                // インターフェイスから番号を受け取り、ポップアップを出す
                int number;
                collision.gameObject.GetComponent<IGimmickHit>().GimmickHit(out number);
                if (beforenum == number) return;
                PopUpController.instance.PopUpWindoController(number);
                beforenum = number;
            }
        }
    }
}