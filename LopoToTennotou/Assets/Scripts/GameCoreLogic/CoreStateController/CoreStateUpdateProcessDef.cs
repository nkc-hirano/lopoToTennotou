using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public struct CoreStateUpdateProcessDef
    {
        public void CoreGameInitStartProcess()
        {

        }

        public void GameTutorialStartProcess() { }
        public void GameStartProcess() { }
        public void GamePlayStartProcess() { }
        public void GameGoalProcess() { }
        public void CoreGameFinalStartProcess() { }
    }
}
