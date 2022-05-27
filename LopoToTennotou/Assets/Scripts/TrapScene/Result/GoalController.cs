using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] string playerName;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == playerName)
        {
            Debug.Log("ƒeƒX");
        }
    }
}
