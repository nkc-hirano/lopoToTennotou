using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test_Trap.Bullet.Hit
{
    public class Test_PlayerHit : MonoBehaviour,IHitBullet
    {
        public void HitAction()
        {
            Debug.Log("キャラクターに当たりました。");
        }
    }
}