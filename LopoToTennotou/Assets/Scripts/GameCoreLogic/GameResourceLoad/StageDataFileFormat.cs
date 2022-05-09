using UnityEngine;

namespace GameCore
{
    [System.Serializable]
    public struct StageDataFileFormat
    {
        // �L���Ȓl�̔z��
        [SerializeField]
        public int xLength;

        [SerializeField]
        public int yLength;

        // �ʒu�ƌ���
        [SerializeField]
        public Direction[] moveGimmickDirections;

        [SerializeField]
        public Direction[] buttonGimmickDirections;

        [SerializeField]
        public SpuareOptionFlag[] spuareOptionFlags;
    }
}