using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class A : MonoBehaviour, ICannonBulletTouchable
    {
        public void TouchAction()
        {
            Debug.Log(name);
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
