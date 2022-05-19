using UnityEngine;

public class MoveWallGimmickHit : MonoBehaviour, IGimmickHit
{
    public void GimmickHit(out int name)
    {
        name = (int)GimmickNumber.MoveWall;
    }
}