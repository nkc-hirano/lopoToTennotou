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
            Debug.Log("�v���C���q�b�g");
            core.PlayerStateUpdate(PlayerStateType.Stop);
            sus.PlayerSusSubject.OnNext(Unit.Default);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<IGimmickHit>() != null) 
            {
                // �C���^�[�t�F�C�X����ԍ����󂯎��A�|�b�v�A�b�v���o��
                int number;
                collision.gameObject.GetComponent<IGimmickHit>().GimmickHit(out number);
                if (beforenum == number) return;
                PopUpController.instance.PopUpWindoController(number);
                beforenum = number;
            }
        }
    }
}