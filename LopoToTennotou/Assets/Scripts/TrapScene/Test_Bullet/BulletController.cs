using UnityEngine;

namespace Trap.Bullet
{
    public class BulletController : MonoBehaviour
    {
        public void Shoot(Vector3 dir)
        {
            GetComponent<Rigidbody>().AddForce(dir);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Hit.IHitBullet>() != null) 
            {
                collision.gameObject.GetComponent<Hit.IHitBullet>().BulletHitAction();
                Destroy(gameObject);
            }
        }
    }
}