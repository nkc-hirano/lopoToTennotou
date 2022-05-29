using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosController : MonoBehaviour
{
    GameObject player;
    GameObject cameraM;
    void Start()
    {
        player = GameObject.Find("Player");
        player.transform.position = gameObject.transform.position;
        cameraM = GameObject.Find("Main Camera");
        cameraM.transform.position = new Vector3(0, CameraPosY());
    }
    float CameraPosY() 
    {
        if (gameObject.name[0] == '8') return 9;
        else if (gameObject.name[0] == '1') return 12;
        else return 0;
    }
}