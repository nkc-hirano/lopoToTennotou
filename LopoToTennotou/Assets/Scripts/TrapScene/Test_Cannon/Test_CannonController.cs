using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test_Trap.Cannon
{
    public class Test_CannonController : MonoBehaviour
    {
        [SerializeField]
        GameObject bullet;      // 弾オブジェクト
        [SerializeField]
        int spanTime;           // 生成速度

        bool stopFlg;           // 動作停止フラグ

        event Action shotEventHandler; 

        void Start()
        {
            shotEventHandler += CreateBullet;
            StartCoroutine(Timeroroutine());
        }

        IEnumerator Timeroroutine() 
        {
            while (!stopFlg) 
            {
                shotEventHandler();
                yield return new WaitForSeconds(spanTime);
            }
        }

        void CreateBullet() 
        {
            // 球を生成し、子オブジェクトにする
            Instantiate(bullet, gameObject.transform);
        }

        private void OnDestroy()
        {
            shotEventHandler -= CreateBullet;
        }
        void StopCreate() 
        {
            stopFlg = true;
        }
    }
}