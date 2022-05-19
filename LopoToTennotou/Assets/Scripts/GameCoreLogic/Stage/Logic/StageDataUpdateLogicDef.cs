using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public struct StageDataUpdateLogicDef
    {
        public void StageDataCalculation(ref Direction[,] moveGimmickDataData,
            ref Direction[,] buttonGimmickData,
            ref SpuareOptionFlag[,] otherGimmickData,
            in StageDataUpdateData inUpdateData,
            out StageDataUpdateResultData resultData)
        {
            resultData = StageDataUpdateResultData.Enpty;
            bool isMove = inUpdateData.rightPower != 0 || inUpdateData.leftPower != 0;
            if (!isMove) { return; }

            // ‰E‚Ì—Í‚ª‹­‚©‚Á‚½‚ç³
            int powerDiff = inUpdateData.rightPower - inUpdateData.rightPower;

            var rotInfo = CulRotBehavior(inUpdateData.dir, powerDiff);
            var posInfo = CulPosBehavior(inUpdateData.dir, Mathf.Min(inUpdateData.rightPower, inUpdateData.rightPower),
                inUpdateData.x, inUpdateData.y,
                moveGimmickDataData, buttonGimmickData, otherGimmickData);

            resultData = new StageDataUpdateResultData(true,
                inUpdateData.x, inUpdateData.y,
                (byte)posInfo.resultPosX, (byte)posInfo.resultPosY,
                rotInfo.resultDir, rotInfo.rotDir);
        }

        private (Direction resultDir, Direction rotDir) CulRotBehavior(Direction beforeDir, int diff)
        {
            return (GetRotDirection(beforeDir, diff), diff > 0 ? Direction.Left : Direction.Right);
        }

        private (int resultPosX, int resultPosY) CulPosBehavior(Direction dir, int power, byte posX, byte posY,
            in Direction[,] moveGimmickDataData,
            in Direction[,] buttonGimmickData,
            in SpuareOptionFlag[,] otherGimmickData)
        {
            int searchPosX;
            int searchPosY;
            for (int i = 0; i < power; i++)
            {
                 
            }
            return (posX, posY);
        }


        // ‰E‚Ì—Í‚ª‘å‚«‚¯‚ê‚Î³
        private Direction GetRotDirection(Direction beforeDir, int rotPower)
        {
            Direction resultDir = beforeDir;

            if (rotPower == 0) { return resultDir; }

            for (int i = 0; i < rotPower; i++)
            {
                bool isPositive = rotPower > 0;
                resultDir = NextDir(resultDir, isPositive);
            }

            return resultDir;

            Direction NextDir(Direction dir, bool isPositive)
            {
                if (isPositive)
                {
                    return dir switch
                    {
                        Direction.Up => Direction.Left,
                        Direction.Left => Direction.Down,
                        Direction.Down => Direction.Right,
                        Direction.Right => Direction.Up,
                        _ => throw new InvalidOperationException()
                    };
                }
                else
                {
                    return dir switch
                    {
                        Direction.Up => Direction.Right,
                        Direction.Right => Direction.Down,
                        Direction.Down => Direction.Left,
                        Direction.Left => Direction.Up,
                        _ => throw new InvalidOperationException()
                    };
                }
            }
        }
    }
}
