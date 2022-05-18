using UnityEngine;

public class SwitchGimmickHit : MonoBehaviour, IGimmickHit
{
    public void GimmickHit(out int name)
    {
        name = (int)GimmickNumber.Swich;
    }
}