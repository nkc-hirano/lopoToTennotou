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
        [SerializeField]
        int bulletSpeed = 2000; //

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
                yield return new WaitForSeconds(spanTime);
                shotEventHandler();
            }
        }

        void CreateBullet()
        {
            float rotX = gameObject.transform.eulerAngles.y;
            Vector2 plusPos = Vector2.zero;
            if (rotX == 270) plusPos.x = 1;
            else if (rotX == 180) plusPos.y = -1;
            else if (rotX == 90) plusPos.x = -1;
            else plusPos.y = 1;
            // ���𐶐����A�q�I�u�W�F�N�g�ɂ���

            GameObject myBullet = Instantiate(bullet);
            myBullet.transform.position = gameObject.transform.position;
            Debug.Log(gameObject.transform.eulerAngles + " " + plusPos);
            myBullet.GetComponent<Bullet.BulletController>().Shoot(new Vector3(bulletSpeed * plusPos.x, 0, bulletSpeed * plusPos.y));
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