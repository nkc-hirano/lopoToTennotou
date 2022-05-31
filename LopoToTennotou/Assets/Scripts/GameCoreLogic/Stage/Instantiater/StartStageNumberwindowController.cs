using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using UnityEngine.UI;

public class StartStageNumberwindowController : MonoBehaviour
{
    StageInstantiater stageInstantiater;
    int stageNum;
    Text text;
    void Start()
    {
        stageInstantiater = GameObject.Find("StageLogic").GetComponent<StageInstantiater>();
        stageNum = stageInstantiater.stageNumber + 1;
        TryGetComponent(out text);
        text.text = "ステージ" + stageNum;
    }
}
