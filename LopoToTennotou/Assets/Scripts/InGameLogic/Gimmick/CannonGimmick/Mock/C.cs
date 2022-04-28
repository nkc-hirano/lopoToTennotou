using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class C : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.TryGetComponent(out ICannonBulletTouchable cannonBulletTouchable))
            {
                cannonBulletTouchable.TouchAction();
            }    
        }
    }
}
