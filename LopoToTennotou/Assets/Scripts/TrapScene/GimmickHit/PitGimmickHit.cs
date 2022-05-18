using UnityEngine;

public class PitGimmickHit : MonoBehaviour, IGimmickHit
{
    public void GimmickHit(out int name)
    {
        name = (int)GimmickNumber.Pit;
    }
}