using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class B : MonoBehaviour,ICannonBulletTouchable
    {
        public void TouchAction()
        {
            Debug.Log(name);
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
