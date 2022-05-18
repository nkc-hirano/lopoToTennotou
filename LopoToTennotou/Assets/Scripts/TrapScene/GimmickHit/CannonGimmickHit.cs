using UnityEngine;

public class CannonGimmickHit : MonoBehaviour, IGimmickHit
{
    public void GimmickHit(out int name)
    {
        name = (int)GimmickNumber.Cannon;
    }
}