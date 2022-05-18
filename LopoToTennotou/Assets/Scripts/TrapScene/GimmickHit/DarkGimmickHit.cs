using UnityEngine;

public class DarkGimmickHit : MonoBehaviour, IGimmickHit
{
    public void GimmickHit(out int name)
    {
        name = (int)GimmickNumber.Dark;
    }
}