using UnityEngine;

namespace Test_Trap.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] int movePowerX = 0;        // X軸で加える力
        [SerializeField] int movePowerZ = -150;     // Z軸で加える力
        void Start()
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(movePowerX, 0, movePowerZ));
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Hit.IHitBullet>() != null) 
            {
                collision.gameObject.GetComponent<Hit.IHitBullet>().HitAction();
                Destroy(gameObject);
            }
        }
    }
}