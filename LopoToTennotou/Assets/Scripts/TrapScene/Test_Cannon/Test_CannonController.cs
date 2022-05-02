using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test_Trap.Cannon
{
    public class Test_CannonController : MonoBehaviour
    {
        [SerializeField]
        GameObject bullet;      // �e�I�u�W�F�N�g
        [SerializeField]
        int spanTime;           // �������x

        bool stopFlg;           // �����~�t���O

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
            // ���𐶐����A�q�I�u�W�F�N�g�ɂ���
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