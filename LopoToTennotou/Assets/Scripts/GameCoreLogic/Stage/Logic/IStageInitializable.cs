namespace GameCore
{
    public interface IStageInitializable
    {
        public void StageDataRegister(Direction[,] moveGimmickData, Direction[,] buttonGimmickData, SpuareOptionFlag[,] otherGimmickData);
    }
}